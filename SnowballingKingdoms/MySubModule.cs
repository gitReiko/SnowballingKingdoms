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

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            if (game.GameType is Campaign campaign)
            {
                var starter = gameStarterObject as CampaignGameStarter;
                if (starter == null)
                    return;

                starter.AddBehavior(new SnowballEvents());
                starter.AddBehavior(new SnowballFixesBehavior());
            }
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
