using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace SnowballingKingdoms
{
    internal class SnowballFixesBehavior : CampaignBehaviorBase
    {
        private bool _fixed = false;

        public override void RegisterEvents()
        {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, OnSessionLaunched);
        }

        public override void SyncData(IDataStore dataStore)
        {
            dataStore.SyncData("_clansWithBrokenSkillsKilled", ref _fixed);
        }

        private void OnSessionLaunched(CampaignGameStarter starter)
        {
            if (_fixed) 
                return;

            KillClansWithBrokenSkills();

            _fixed = true;
        }

        private void KillClansWithBrokenSkills()
        {
            InformationManager.DisplayMessage(new InformationMessage("[Snowballs] Kill broken clans."));

            foreach (Clan clan in Clan.All)
            {
                if (!clan.IsNoble || clan.IsBanditFaction || clan.IsMinorFaction || clan.IsRebelClan || clan.IsEliminated)
                    continue;

                bool noSkills = true;
                foreach (Hero hero in clan.Heroes)
                {
                    if (hero == null || hero.IsChild || hero.IsDisabled)
                        continue;

                    if (HasSkills(hero)) 
                        noSkills = false;
                }

                if (noSkills)
                {
                    foreach (Hero hero in clan.Heroes)
                    {
                        KillCharacterAction.ApplyByRemove(hero);
                    }

                    InformationManager.DisplayMessage(
                        new InformationMessage($"[Snowballs] Members of broken clan {clan.Name} are killed."));
                }

                clan.CalculateMidSettlement();
            }
        }

        private bool HasSkills(Hero hero)
        {
            foreach (SkillObject skill in Skills.All)
            {
                if (hero.GetSkillValue(skill) > 0)
                    return true;
            }
            return false;
        }
    }
}