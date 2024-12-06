using System;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SnowballingKingdoms
{
    public class MySubModule : MBSubModuleBase 
    {

        protected void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
        }

        public override void OnCampaignStart(Game game, object starterObject)
        {
            this.InitializeGame(game, (IGameStarter)starterObject);
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            this.InitializeGame(game, gameStarterObject);
        }

        public void InitializeGame(Game game, IGameStarter gameStarter)
        {
            this.AddBehaviours(gameStarter as CampaignGameStarter);
        }

        private void AddBehaviours(CampaignGameStarter starter)
        {
            starter.AddBehavior(new SnowballEvents());
        }

    }
}
