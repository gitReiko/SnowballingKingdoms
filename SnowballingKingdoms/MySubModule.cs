using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;

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

        public override void BeginGameStart(Game game)
        {
            if (game.GameType is Campaign)
            {
                game.ObjectManager.RegisterType<Snowball>("Snowball", "Snowballs", 100U, true, false);
                MBObjectManager.Instance.LoadXML("Snowballs", false);
                Snowball.initialize_unused_snowballs();

                game.ObjectManager.RegisterType<SnowConfig>("SnowConfig", "SnowConfigs", 100U, true, false);
                MBObjectManager.Instance.LoadXML("SnowConfigs", false);
                SnowConfig.init_config();
            }
        }

    }
}
