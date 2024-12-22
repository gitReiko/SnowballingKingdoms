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

            members = GetParentsWithСhildren(kingdom, clan, settlement);

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

        private static List<Hero> GetParentsWithСhildren(Kingdom kingdom, Clan clan, Settlement settlement)
        {
            List<Hero> members = new List<Hero>();
            MBReadOnlyList<CharacterObject> lordTemplates = kingdom.Culture.LordTemplates;


            int fatherAge = MBRandom.RandomInt(25, 55);
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

                int isFemale = MBRandom.RandomInt(1, 2);

                if(isFemale == 2)
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

    }

}
