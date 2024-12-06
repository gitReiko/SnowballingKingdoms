using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Library;

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
            InformationManager.DisplayMessage(new InformationMessage("message 123", TaleWorlds.Library.Color.ConvertStringToColor("#FF0042FF")));
        }

    }
}
