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




                InformationManager.DisplayMessage(new InformationMessage(kingdom.Name.Value, TaleWorlds.Library.Color.ConvertStringToColor("#FF0042FF")));

                Settlement settlement = get_kingdom_settlement(kingdom);

                InformationManager.DisplayMessage(new InformationMessage(settlement.Name.Value, TaleWorlds.Library.Color.ConvertStringToColor("#FF1342FF")));

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

            return null;
        }


        private void CreateNewClan()
        {
            //HeroCreator.CreateSpecialHero


            //Kingdom.ge

            //Settlement settlement = this.GetValidSettlement();





        }

    }
}
