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

                create_new_clan(kingdom.Culture, kingdomSettlement);

                


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

        private void create_new_clan(CultureObject clanCulture, Settlement kingdomSettlement)
        {
            string clanId = "clan_12345_xfh";
            TextObject clanName = new TextObject("Asen", null);
            string bannerCode = "11.118.19.1836.1836.768.788.1.0.-30.510.47.38.1800.400.764.764.0.1.90.510.47.38.1800.400.764.764.0.1.0.503.47.38.220.220.599.564.0.1.0.503.118.38.170.170.599.564.0.1.0.510.47.38.204.170.599.564.0.1.0.510.47.38.204.170.599.564.0.1.90.503.47.38.220.220.931.564.0.1.0.503.118.38.170.170.931.564.0.1.0.510.47.38.204.170.931.564.0.1.0.510.47.38.204.170.931.564.0.1.90.503.47.38.220.220.931.966.0.1.0.503.118.38.170.170.931.966.0.1.0.510.47.38.204.170.931.966.0.1.0.510.47.38.204.170.931.966.0.1.90.503.47.38.220.220.599.966.0.1.0.503.118.38.170.170.599.966.0.1.0.510.47.38.204.170.599.966.0.1.0.510.47.38.204.170.599.966.0.1.90";
            Banner clanBanner = new Banner(bannerCode);



            Clan newClan = Clan.CreateClan(clanId);

            newClan.InitializeClan(clanName, clanName, clanCulture, clanBanner, kingdomSettlement.GatePosition);
            newClan.UpdateHomeSettlement(kingdomSettlement);
            newClan.AddRenown(1000f);





        }

    }
}
