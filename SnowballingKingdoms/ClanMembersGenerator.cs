using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using Extensions = TaleWorlds.Core.Extensions;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using static TaleWorlds.CampaignSystem.CampaignBehaviors.LordConversationsCampaignBehavior;
using TaleWorlds.Localization;

namespace SnowballingKingdoms
{
    internal class ClanMembersGenerator
    {

        public static List<Hero> GenerateClanMemeber(Kingdom kingdom, Clan clan, Settlement settlement)
        {
            // Debug 
            InformationManager.DisplayMessage(new InformationMessage(clan.Name.ToString(), TaleWorlds.Library.Color.ConvertStringToColor("#FF0042FF")));

            List<Hero> members = new List<Hero>();

            members = GetParentsWith2SonsAnd1Daughter(kingdom, clan, settlement);

            return members;

        }

        private static List<Hero> ThreeMen(Kingdom kingdom, Clan clan, Settlement settlement)
        {
            List<Hero> members = new List<Hero>();
            MBReadOnlyList<CharacterObject> lordTemplates = kingdom.Culture.LordTemplates;

            for (int i = 0; i < 3; i++)
            {
                CharacterObject randomTemplate = Extensions.GetRandomElement<CharacterObject>(lordTemplates);
                int randomAge = MBRandom.RandomInt(Campaign.Current.Models.AgeModel.HeroComesOfAge, 50);

                Hero member = HeroCreator.CreateSpecialHero(randomTemplate, settlement, clan, null, randomAge);

                member.Gold = 20000;
                member.StayingInSettlement = settlement;

                members.Add(member);
            }

            return members;
        }

        private static List<Hero> GetParentsWith2SonsAnd1Daughter(Kingdom kingdom, Clan clan, Settlement settlement)
        {
            List<Hero> members = new List<Hero>();
            MBReadOnlyList<CharacterObject> lordTemplates = kingdom.Culture.LordTemplates;


            int fatherAge = MBRandom.RandomInt(45, 55);
            CharacterObject memberTemplate = Extensions.GetRandomElement<CharacterObject>(lordTemplates);
            Hero father = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, fatherAge);
            father.Gold = 20000;
            father.StayingInSettlement = settlement;

            
            int motherAge = MBRandom.RandomInt(fatherAge - 3, fatherAge + 3);
            memberTemplate = Extensions.GetRandomElement<CharacterObject>(lordTemplates);
            memberTemplate.IsFemale = true;
            Hero mother = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, motherAge);
            mother.Gold = 20000;
            mother.StayingInSettlement = settlement;


            int son1Age = MBRandom.RandomInt(motherAge-25, motherAge - 15);
            memberTemplate = Extensions.GetRandomElement<CharacterObject>(lordTemplates);
            Hero son1 = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, son1Age);
            son1.Gold = 10000;
            son1.StayingInSettlement = settlement;


            int son2Age = MBRandom.RandomInt(son1Age - 5, son1Age - 1);
            memberTemplate = Extensions.GetRandomElement<CharacterObject>(lordTemplates);
            Hero son2 = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, son2Age);
            son2.Gold = 10000;
            son2.StayingInSettlement = settlement;


            int daughterAge = MBRandom.RandomInt(son2Age - 5, son2Age - 1);
            memberTemplate = Extensions.GetRandomElement<CharacterObject>(lordTemplates);
            memberTemplate.IsFemale = true;
            Hero daughter = HeroCreator.CreateSpecialHero(memberTemplate, settlement, clan, null, daughterAge);
            daughter.Gold = 10000;
            daughter.StayingInSettlement = settlement;


            father.Spouse = mother;
            mother.Spouse = father;

            son1.Father = father;
            son1.Mother = mother;

            son2.Father = father;
            son2.Mother = mother;

            daughter.Father = father;
            daughter.Mother = mother;


            members.Add(father);
            members.Add(mother);
            members.Add(son1);
            members.Add(son2);
            members.Add(daughter);


            return members;
        }

    }

}
