using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace SnowballingKingdoms
{
    internal class ClanMembersGenerator
    {

        public static List<Hero> GenerateClanMemeber(Clan clan, Settlement settlement)
        {
            List<Hero> members = new List<Hero>();

            List<CharacterObject> lordTemplates = GetLordTemplates(clan?.Culture);
            if (lordTemplates.Count == 0 || Campaign.Current?.Models?.AgeModel == null)
            {
                Debug.Print("[Snowball] Cannot generate clan members: culture, lord templates or age model unavailable.", 0);
                return members;
            }

            // Family roles require templates of both sexes. Never change shared templates.
            if (!lordTemplates.Exists(template => template.IsFemale)
                || !lordTemplates.Exists(template => !template.IsFemale))
            {
                return get_members_without_family(clan, settlement, lordTemplates);
            }

            int clanType = MBRandom.RandomInt(1, 14);

            if (clanType > 10)
            {
                members = get_parents_with_children(clan, settlement, lordTemplates);
            }
            else if(clanType == 10)
            {
                members = get_older_son(clan, settlement, lordTemplates);
            }
            else if (clanType == 9)
            {
                members = get_mother_of_children(clan, settlement, lordTemplates);
            }
            else if (clanType == 8)
            {
                members = get_female_leader(clan, settlement, lordTemplates);
            }
            else if (clanType > 4)
            {
                members = get_family_without_children(clan, settlement, lordTemplates);
            }
            else
            {
                members = get_members_without_family(clan, settlement, lordTemplates);
            }

            return members;

        }

        public static void add_member_to_clan(Clan clan)
        {
            List<CharacterObject> lordTemplates = GetLordTemplates(clan?.Culture);
            if (lordTemplates.Count == 0 || Campaign.Current?.Models?.AgeModel == null)
            {
                Debug.Print("[Snowball] Cannot add clan member: culture, lord templates or age model unavailable.", 0);
                return;
            }

            bool female = ShouldCreateFemaleMember(lordTemplates);
            CharacterObject memberTemplate = GetRandomTemplate(lordTemplates, female);
            int randomAge = GetAdultAge(20, 43);

            Hero member = HeroCreator.CreateSpecialHero(memberTemplate, null, clan, null, randomAge);
            ApplySkillsFromTemplate(member, memberTemplate);
            member.Gold = 30000;
            member.ChangeState(Hero.CharacterStates.Active);
            member.Culture = clan.Culture;

            if (clan.HomeSettlement != null)
                member.StayingInSettlement = clan.HomeSettlement;
        }

        private static List<Hero> get_members_without_family(Clan clan, Settlement settlement, List<CharacterObject> lordTemplates)
        {
            List<Hero> members = new List<Hero>();

            int membersNum = MBRandom.RandomInt(2, 5);
            for (int i = 0; i < membersNum; i++)
            {
                CharacterObject memberTemplate = GetRandomTemplate(lordTemplates, i > 0 ? isFemale() : false);
                int randomAge = GetAdultAge(Campaign.Current.Models.AgeModel.HeroComesOfAge, 50);

                Hero member = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, randomAge);
                ApplySkillsFromTemplate(member, memberTemplate);
                member.Gold = 20000;
                member.Culture = clan.Culture;

                if (settlement != null)
                {
                    member.StayingInSettlement = settlement;
                }



                members.Add(member);
            }

            return members;
        }

        private static List<Hero> get_parents_with_children(Clan clan, Settlement settlement, List<CharacterObject> lordTemplates)
        {
            List<Hero> members = new List<Hero>();


            int fatherAge = GetAdultAge(25, 55);
            CharacterObject memberTemplate = GetRandomTemplate(lordTemplates, false);
            Hero father = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, fatherAge);
            ApplySkillsFromTemplate(father, memberTemplate);
            father.Gold = 20000;
            father.Culture = clan.Culture;

            if (settlement != null)
            {
                father.StayingInSettlement = settlement;
            }

            int motherAge = GetAdultAge(fatherAge - 3, fatherAge + 3);
            memberTemplate = GetRandomTemplate(lordTemplates, true);
            
            Hero mother = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, motherAge);
            ApplySkillsFromTemplate(mother, memberTemplate);
            mother.Gold = 20000;
            mother.Culture = clan.Culture;

            if (settlement != null)
            {
                mother.StayingInSettlement = settlement;
            }


            father.Spouse = mother;
            mother.Spouse = father;

            members.Add(father);
            members.Add(mother);


            int childrensNum = get_number_of_childrens(motherAge);
            int childAge = motherAge-15;

            for (int i = 0; i < childrensNum; i++)
            {
                int memberAge = MBRandom.RandomInt(childAge - 4, childAge - 1);
                if (memberAge <= 0)
                {
                    memberAge = 1;
                }
                memberTemplate = GetRandomTemplate(lordTemplates, isFemale());

                Hero child = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, memberAge);
                ApplySkillsFromTemplate(child, memberTemplate);
                child.Gold = 10000;
                child.Culture = clan.Culture;

                if (settlement != null)
                {
                    child.StayingInSettlement = settlement;
                }

                child.Father = father;
                child.Mother = mother;

                members.Add(child);
            }

            return members;
        }


        private static int get_number_of_childrens(int motherAge)
        {
            int maxNumber = 1;

            if(motherAge > 40)
            {
                maxNumber = 4;
            }
            else if(motherAge > 30)
            {
                maxNumber = 3;
            }
            else if (motherAge > 25)
            {
                maxNumber = 2;
            }
            else
            {
                return 1;
            }

            return MBRandom.RandomInt(1, maxNumber);
        }

        private static List<Hero> get_family_without_children(Clan clan, Settlement settlement, List<CharacterObject> lordTemplates)
        {
            List<Hero> members = new List<Hero>();


            int fatherAge = GetAdultAge(25, 55);
            CharacterObject memberTemplate = GetRandomTemplate(lordTemplates, false);
            Hero father = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, fatherAge);
            ApplySkillsFromTemplate(father, memberTemplate);
            father.Gold = 20000;
            father.Culture = clan.Culture;

            if (settlement != null)
            {
                father.StayingInSettlement = settlement;
            }


            int motherAge = GetAdultAge(fatherAge - 3, fatherAge + 3);
            memberTemplate = GetRandomTemplate(lordTemplates, true);
            Hero mother = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, motherAge);
            ApplySkillsFromTemplate(mother, memberTemplate);
            mother.Gold = 20000;
            mother.Culture = clan.Culture;

            if (settlement != null)
            {
                mother.StayingInSettlement = settlement;
            }


            father.Spouse = mother;
            mother.Spouse = father;

            members.Add(father);
            members.Add(mother);


            int membersNum = MBRandom.RandomInt(1, 3); ;

            for (int i = 0; i < membersNum; i++)
            {
                memberTemplate = GetRandomTemplate(lordTemplates, i > 0 ? isFemale() : false);
                int randomAge = GetAdultAge(Campaign.Current.Models.AgeModel.HeroComesOfAge, 50);

                Hero member = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, randomAge);
                ApplySkillsFromTemplate(member, memberTemplate);

                member.Gold = 20000;
                member.Culture = clan.Culture;

                if (settlement != null)
                {
                    member.StayingInSettlement = settlement;
                }

                members.Add(member);
            }

            return members;
        }

        private static List<Hero> get_mother_of_children(Clan clan, Settlement settlement, List<CharacterObject> lordTemplates)
        {
            List<Hero> members = new List<Hero>();

            int motherAge = GetAdultAge(25, 55);
            CharacterObject memberTemplate = GetRandomTemplate(lordTemplates, true);
            Hero mother = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, motherAge);
            ApplySkillsFromTemplate(mother, memberTemplate);
            mother.Gold = 20000;
            mother.Culture = clan.Culture;

            if (settlement != null)
            {
                mother.StayingInSettlement = settlement;
            }

            members.Add(mother);


            int childrensNum = get_number_of_childrens(motherAge);
            int childAge = motherAge - 15;

            for (int i = 0; i < childrensNum; i++)
            {
                int memberAge = MBRandom.RandomInt(childAge - 4, childAge - 1);
                if (memberAge <= 0)
                {
                    memberAge = 1;
                }
                memberTemplate = GetRandomTemplate(lordTemplates, isFemale());

                Hero child = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, memberAge);
                ApplySkillsFromTemplate(child, memberTemplate);
                child.Gold = 10000;
                child.Culture = clan.Culture;

                if (settlement != null)
                {
                    child.StayingInSettlement = settlement;
                }

                child.Mother = mother;

                members.Add(child);
            }

            return members;
        }

        private static List<Hero> get_female_leader(Clan clan, Settlement settlement, List<CharacterObject> lordTemplates)
        {
            List<Hero> members = new List<Hero>();

            int membersNum = MBRandom.RandomInt(2, 5);
            for (int i = 0; i < membersNum; i++)
            {
                CharacterObject memberTemplate = GetRandomTemplate(lordTemplates, i > 0 ? isFemale() : true);
                int randomAge = GetAdultAge(Campaign.Current.Models.AgeModel.HeroComesOfAge, 50);

                Hero member = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, randomAge);
                ApplySkillsFromTemplate(member, memberTemplate);
                member.Gold = 20000;
                member.Culture = clan.Culture;

                if (settlement != null)
                {
                    member.StayingInSettlement = settlement;
                }

                members.Add(member);
            }

            return members;
        }


        private static List<Hero> get_older_son(Clan clan, Settlement settlement, List<CharacterObject> lordTemplates)
        {
            List<Hero> members = new List<Hero>();

            int motherAge = GetAdultAge(Math.Max(35, Campaign.Current.Models.AgeModel.HeroComesOfAge + 15), 55);
            CharacterObject memberTemplate = GetRandomTemplate(lordTemplates, true);

            Hero mother = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, motherAge);
            ApplySkillsFromTemplate(mother, memberTemplate);
            mother.Gold = 20000;
            mother.Culture = clan.Culture;

            if (settlement != null)
            {
                mother.StayingInSettlement = settlement;
            }

            int olderSonAge = motherAge - 15;
            memberTemplate = GetRandomTemplate(lordTemplates, false);
            Hero olderSon = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, olderSonAge);
            ApplySkillsFromTemplate(olderSon, memberTemplate);
            olderSon.Gold = 20000;
            olderSon.Mother = mother;
            olderSon.Culture = clan.Culture;

            if (settlement != null)
            {
                olderSon.StayingInSettlement = settlement;
            }

            members.Add(olderSon);
            members.Add(mother);


            int childrensNum = get_number_of_childrens(motherAge) - 1;
            int childAge = motherAge - 15;

            for (int i = 0; i < childrensNum; i++)
            {
                int memberAge = MBRandom.RandomInt(childAge - 4, childAge - 1);
                if (memberAge <= 0)
                {
                    memberAge = 1;
                }
                memberTemplate = GetRandomTemplate(lordTemplates, isFemale());

                Hero child = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, memberAge);
                ApplySkillsFromTemplate(child, memberTemplate);
                child.Gold = 10000;
                child.Culture = clan.Culture;

                if (settlement != null)
                {
                    child.StayingInSettlement = settlement;
                }

                child.Mother = mother;

                members.Add(child);
            }

            return members;
        }

        public static bool CanGenerateMembers(CultureObject culture)
        {
            return Campaign.Current?.Models?.AgeModel != null && GetLordTemplates(culture).Count > 0;
        }

        private static List<CharacterObject> GetLordTemplates(CultureObject culture)
        {
            List<CharacterObject> templates = new List<CharacterObject>();
            var source = culture?.LordTemplates;
            if (source != null)
            {
                foreach (CharacterObject template in source)
                {
                    if (template != null)
                        templates.Add(template);
                }
            }
            return templates;
        }

        private static bool ShouldCreateFemaleMember(List<CharacterObject> templates)
        {
            if (!templates.Exists(template => template.IsFemale))
                return false;

            if (!templates.Exists(template => !template.IsFemale))
                return true;

            return MBRandom.RandomInt(1, 10) > 7;
        }

        private static CharacterObject GetRandomTemplate(List<CharacterObject> templates, bool female)
        {
            List<CharacterObject> matching = templates.FindAll(template => template.IsFemale == female);
            // Cultures with templates of only one sex can still supply unrelated clan members.
            List<CharacterObject> available = matching.Count > 0 ? matching : templates;
            return available[MBRandom.RandomInt(0, available.Count)];
        }

        private static int GetAdultAge(int minimum, int maximumExclusive)
        {
            minimum = Math.Max(minimum, Campaign.Current.Models.AgeModel.HeroComesOfAge);
            return maximumExclusive > minimum ? MBRandom.RandomInt(minimum, maximumExclusive) : minimum;
        }

        private static bool isFemale()
        {
            return MBRandom.RandomInt(1, 10) > 5;
        }

        public static void ApplySkillsFromTemplate(Hero hero, CharacterObject template)
        {
            if (hero?.HeroDeveloper == null || template == null)
                return;

            foreach (SkillObject skill in Skills.All)
            {
                int value = template.GetSkillValue(skill);
                if (value > 0)
                {
                    hero.HeroDeveloper.SetInitialSkillLevel(skill, value);
                }
            }

            if (!HasSkills(hero))
            {
                foreach (SkillObject skill in Skills.All)
                {
                    int randSkill = MBRandom.RandomInt(0, 300);
                    hero.HeroDeveloper.SetInitialSkillLevel(skill, randSkill);
                }
            }
        }

        private static bool HasSkills(Hero hero)
        {
            foreach (SkillObject skill in Skills.All)
            {
                if (hero.GetSkillValue(skill) > 0)
                    return true;
            }
            return false;
        }
    }
}
