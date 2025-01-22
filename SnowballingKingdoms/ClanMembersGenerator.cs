using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using Extensions = TaleWorlds.Core.Extensions;

namespace SnowballingKingdoms
{
    internal class ClanMembersGenerator
    {

        public static List<Hero> GenerateClanMemeber(Clan clan, Settlement settlement)
        {
            List<Hero> members = new List<Hero>();

            int clanType = MBRandom.RandomInt(1, 14);

            if (clanType > 10)
            {
                members = get_parents_with_children(clan, settlement);
            }
            else if(clanType == 10)
            {
                members = get_older_son(clan, settlement);
            }
            else if (clanType == 9)
            {
                members = get_mother_of_children(clan, settlement);
            }
            else if (clanType == 8)
            {
                members = get_female_leader(clan, settlement);
            }
            else if (clanType > 4)
            {
                members = get_family_without_children(clan, settlement);
            }
            else
            {
                members = get_members_without_family(clan, settlement);
            }

            return members;

        }

        private static List<Hero> get_members_without_family(Clan clan, Settlement settlement)
        {
            List<Hero> members = new List<Hero>();
            MBReadOnlyList<CharacterObject> lordTemplates = clan.Culture.LordTemplates;

            int membersNum = MBRandom.RandomInt(2, 5);
            for (int i = 0; i < membersNum; i++)
            {
                CharacterObject memberTemplate = Extensions.GetRandomElement<CharacterObject>(lordTemplates);
                memberTemplate.Culture = clan.Culture;
                int randomAge = MBRandom.RandomInt(Campaign.Current.Models.AgeModel.HeroComesOfAge, 50);

                int isFemale = 0;
                if (i > 0)
                {
                    isFemale = MBRandom.RandomInt(1, 10);
                }

                if (isFemale > 5)
                {
                    memberTemplate.IsFemale = true;
                }

                Hero member = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, randomAge);

                member.Gold = 20000;
                member.StayingInSettlement = settlement;

                members.Add(member);
            }

            return members;
        }

        private static List<Hero> get_parents_with_children(Clan clan, Settlement settlement)
        {
            List<Hero> members = new List<Hero>();
            MBReadOnlyList<CharacterObject> lordTemplates = clan.Culture.LordTemplates;


            int fatherAge = MBRandom.RandomInt(25, 55);
            CharacterObject memberTemplate = Extensions.GetRandomElement<CharacterObject>(lordTemplates);
            memberTemplate.Culture = clan.Culture;
            Hero father = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, fatherAge);
            father.Gold = 20000;
            father.StayingInSettlement = settlement;

            
            int motherAge = MBRandom.RandomInt(fatherAge - 3, fatherAge + 3);
            memberTemplate = Extensions.GetRandomElement<CharacterObject>(lordTemplates);
            memberTemplate.Culture = clan.Culture;
            memberTemplate.IsFemale = true;
            Hero mother = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, motherAge);
            mother.Gold = 20000;
            mother.StayingInSettlement = settlement;


            father.Spouse = mother;
            mother.Spouse = father;

            members.Add(father);
            members.Add(mother);


            int childrensNum = get_number_of_childrens(motherAge);
            int childAge = motherAge-15;

            for (int i = 0; i < childrensNum; i++)
            {
                childAge = MBRandom.RandomInt(childAge - 4, childAge - 1);
                memberTemplate = Extensions.GetRandomElement<CharacterObject>(lordTemplates);
                memberTemplate.Culture = clan.Culture;

                int isFemale = MBRandom.RandomInt(1, 10);

                if (isFemale > 5)
                {
                    memberTemplate.IsFemale = true;
                }

                Hero child = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, childAge);
                child.Gold = 10000;
                child.StayingInSettlement = settlement;

                child.Father = father;
                child.Mother = mother;

                members.Add(child);
            }

            return members;
        }


        private static int get_number_of_childrens(int motherAge)
        {
            int maxNumber = 0;

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

        private static List<Hero> get_family_without_children(Clan clan, Settlement settlement)
        {
            List<Hero> members = new List<Hero>();
            MBReadOnlyList<CharacterObject> lordTemplates = clan.Culture.LordTemplates;


            int fatherAge = MBRandom.RandomInt(25, 55);
            CharacterObject memberTemplate = Extensions.GetRandomElement<CharacterObject>(lordTemplates);
            memberTemplate.Culture = clan.Culture;
            Hero father = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, fatherAge);
            father.Gold = 20000;
            father.StayingInSettlement = settlement;


            int motherAge = MBRandom.RandomInt(fatherAge - 3, fatherAge + 3);
            memberTemplate = Extensions.GetRandomElement<CharacterObject>(lordTemplates);
            memberTemplate.Culture = clan.Culture;
            memberTemplate.IsFemale = true;
            Hero mother = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, motherAge);
            mother.Gold = 20000;
            mother.StayingInSettlement = settlement;


            father.Spouse = mother;
            mother.Spouse = father;

            members.Add(father);
            members.Add(mother);


            int membersNum = MBRandom.RandomInt(1, 3); ;

            for (int i = 0; i < membersNum; i++)
            {
                memberTemplate = Extensions.GetRandomElement<CharacterObject>(lordTemplates);
                memberTemplate.Culture = clan.Culture;
                int randomAge = MBRandom.RandomInt(Campaign.Current.Models.AgeModel.HeroComesOfAge, 50);

                int isFemale = 0;
                if (i > 0)
                {
                    isFemale = MBRandom.RandomInt(1, 10);
                }

                if (isFemale > 5)
                {
                    memberTemplate.IsFemale = true;
                }

                Hero member = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, randomAge);

                member.Gold = 20000;
                member.StayingInSettlement = settlement;

                members.Add(member);
            }

            return members;
        }

        private static List<Hero> get_mother_of_children(Clan clan, Settlement settlement)
        {
            List<Hero> members = new List<Hero>();
            MBReadOnlyList<CharacterObject> lordTemplates = clan.Culture.LordTemplates;

            int motherAge = MBRandom.RandomInt(25, 55);
            CharacterObject memberTemplate = Extensions.GetRandomElement<CharacterObject>(lordTemplates);
            memberTemplate.Culture = clan.Culture;
            memberTemplate.IsFemale = true;
            Hero mother = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, motherAge);
            mother.Gold = 20000;
            mother.StayingInSettlement = settlement;

            members.Add(mother);


            int childrensNum = get_number_of_childrens(motherAge);
            int childAge = motherAge - 15;

            for (int i = 0; i < childrensNum; i++)
            {
                childAge = MBRandom.RandomInt(childAge - 4, childAge - 1);
                memberTemplate = Extensions.GetRandomElement<CharacterObject>(lordTemplates);
                memberTemplate.Culture = clan.Culture;

                int isFemale = MBRandom.RandomInt(1, 10);

                if (isFemale > 5)
                {
                    memberTemplate.IsFemale = true;
                }

                Hero child = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, childAge);
                child.Gold = 10000;
                child.StayingInSettlement = settlement;

                child.Mother = mother;

                members.Add(child);
            }

            return members;
        }

        private static List<Hero> get_female_leader(Clan clan, Settlement settlement)
        {
            List<Hero> members = new List<Hero>();
            MBReadOnlyList<CharacterObject> lordTemplates = clan.Culture.LordTemplates;

            int membersNum = MBRandom.RandomInt(2, 5);
            for (int i = 0; i < membersNum; i++)
            {
                CharacterObject memberTemplate = Extensions.GetRandomElement<CharacterObject>(lordTemplates);
                memberTemplate.Culture = clan.Culture;
                int randomAge = MBRandom.RandomInt(Campaign.Current.Models.AgeModel.HeroComesOfAge, 50);

                int isFemale = 6;
                if (i > 0)
                {
                    isFemale = MBRandom.RandomInt(1, 10);
                }

                if (isFemale > 5)
                {
                    memberTemplate.IsFemale = true;
                }

                Hero member = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, randomAge);

                member.Gold = 20000;
                member.StayingInSettlement = settlement;

                members.Add(member);
            }

            return members;
        }


        private static List<Hero> get_older_son(Clan clan, Settlement settlement)
        {
            List<Hero> members = new List<Hero>();
            MBReadOnlyList<CharacterObject> lordTemplates = clan.Culture.LordTemplates;

            int motherAge = MBRandom.RandomInt(35, 55);
            CharacterObject memberTemplate = Extensions.GetRandomElement<CharacterObject>(lordTemplates);
            memberTemplate.Culture = clan.Culture;
            memberTemplate.IsFemale = true;
            Hero mother = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, motherAge);
            mother.Gold = 20000;
            mother.StayingInSettlement = settlement;

            int olderSonAge = motherAge - 15;
            memberTemplate = Extensions.GetRandomElement<CharacterObject>(lordTemplates);
            memberTemplate.Culture = clan.Culture;
            Hero olderSon = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, olderSonAge);
            mother.Gold = 20000;
            mother.StayingInSettlement = settlement;


            olderSon.Mother = mother;
            members.Add(olderSon);
            members.Add(mother);


            int childrensNum = get_number_of_childrens(motherAge) - 1;
            int childAge = motherAge - 15;

            for (int i = 0; i < childrensNum; i++)
            {
                childAge = MBRandom.RandomInt(childAge - 4, childAge - 1);
                memberTemplate = Extensions.GetRandomElement<CharacterObject>(lordTemplates);
                memberTemplate.Culture = clan.Culture;

                int isFemale = MBRandom.RandomInt(1, 10);

                if (isFemale > 5)
                {
                    memberTemplate.IsFemale = true;
                }

                Hero child = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, childAge);
                child.Gold = 10000;
                child.StayingInSettlement = settlement;

                child.Mother = mother;

                members.Add(child);
            }

            return members;
        }

    }

}
