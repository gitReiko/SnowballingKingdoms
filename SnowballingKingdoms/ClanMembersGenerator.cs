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

namespace SnowballingKingdoms
{
    internal class ClanMembersGenerator
    {

        public static List<Hero> GenerateClanMemeber(Kingdom kingdom, Clan clan, Settlement settlement)
        {
            // Debug 
            InformationManager.DisplayMessage(new InformationMessage(clan.Name.ToString(), TaleWorlds.Library.Color.ConvertStringToColor("#FF0042FF")));

            List<Hero> members = new List<Hero>();

            members = ThreeMen(kingdom, clan, settlement);

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

    }

}
