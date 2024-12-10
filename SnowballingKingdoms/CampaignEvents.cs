using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Library;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem.Settlements;
using System.Net.NetworkInformation;
using TaleWorlds.Localization;

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

                Settlement settlement = get_kingdom_settlement(kingdom);

                InformationManager.DisplayMessage(new InformationMessage(settlement.Name.ToString(), TaleWorlds.Library.Color.ConvertStringToColor("#FF1342FF")));

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


        private void CreateNewClan()
        {
            //HeroCreator.CreateSpecialHero


            //Kingdom.ge

            //Settlement settlement = this.GetValidSettlement();





        }

    }
}
