using System;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using Extensions = TaleWorlds.Core.Extensions;

namespace SnowballingKingdoms
{
    internal class SnowballEvents : CampaignBehaviorBase
    {

        public override void SyncData(IDataStore dataStore) { }

        public override void RegisterEvents()
        {
            CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, DailyTickEvent);
        }

        private void DailyTickEvent()
        {
            foreach(Kingdom kingdom in Kingdom.All)
            {




                InformationManager.DisplayMessage(new InformationMessage(kingdom.Name.ToString(), TaleWorlds.Library.Color.ConvertStringToColor("#FF0042FF")));

                Settlement kingdomSettlement = get_kingdom_settlement(kingdom);

                InformationManager.DisplayMessage(new InformationMessage(kingdomSettlement.Name.ToString(), TaleWorlds.Library.Color.ConvertStringToColor("#FF1342FF")));

                create_new_clan(kingdom.Culture, kingdom, kingdomSettlement);


                    break;
            }
            
            
            //InformationManager.DisplayMessage(new InformationMessage("message 123", TaleWorlds.Library.Color.ConvertStringToColor("#FF0042FF")));
        }

        private Settlement get_kingdom_settlement(Kingdom kingdom)
        {
            foreach (Settlement settlement in kingdom.Settlements)
            {
                if (settlement.IsCastle || settlement.IsTown)
                {
                    return settlement;
                }
            }

            print_no_settlements_error(kingdom.Name.Value.ToString());
            return null;
        }

        private void print_no_settlements_error(string kingdomName)
        {
            string error = "{=SNBK.error1}Error: kingdom " + kingdomName + " has no settlements.";
            TextObject errorTxt = new TextObject(error, null);
            InformationManager.DisplayMessage(new InformationMessage(errorTxt.ToString(), TaleWorlds.Library.Color.ConvertStringToColor("#FF0042FF")));
        }

        private void create_new_clan(CultureObject clanCulture, Kingdom kingdom, Settlement kingdomSettlement)
        {
            Snowball snowball = get_random_snowball();

            string clanId = snowball.Id;
            TextObject clanName = snowball.Name;
            Banner clanBanner = new Banner(snowball.Banner);



            Clan newClan = Clan.CreateClan(clanId);

            newClan.InitializeClan(clanName, clanName, clanCulture, clanBanner, kingdomSettlement.GatePosition);
            newClan.UpdateHomeSettlement(kingdomSettlement);
            newClan.AddRenown(500f);

            newClan.Color = clanBanner.GetPrimaryColor();
            newClan.Color2 = clanBanner.GetSecondaryColor();
            newClan.AlternativeColor = clanBanner.GetPrimaryColor();
            newClan.AlternativeColor2 = clanBanner.GetSecondaryColor();

            newClan.Kingdom = kingdom;

            List<Hero> heros = ClanMembersGenerator.GenerateClanMemeber(kingdom, newClan, kingdomSettlement);
            newClan.SetLeader(heros[0]);

            newClan.CreateNewMobileParty(heros[0]);

        }

        private Snowball get_random_snowball()
        {
            return Snowball.All[MBRandom.RandomInt(0, (Snowball.All.Count - 1))];
        }


    }
}
