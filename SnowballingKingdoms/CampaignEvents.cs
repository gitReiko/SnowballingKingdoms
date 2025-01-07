using System;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using System.Collections.Generic;

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

                create_new_clan(kingdom.Culture, kingdom);


                return;
            }
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

        private void create_new_clan(CultureObject clanCulture, Kingdom kingdom)
        {
            MBReadOnlyList<Snowball> snowballs = Snowball.get_all_unused_for_kingdom(kingdom.StringId);

            if (snowballs.IsEmpty())
            {
                snowballs = Snowball.get_all_unused_for_kingdom_main_culture(kingdom.Culture.StringId);
            }

            if (snowballs.IsEmpty())
            {
                snowballs = Snowball.get_all_unused_for_kingdom_culturies(kingdom);
            }

            if (snowballs.IsEmpty())
            {
                snowballs = Snowball.get_all_unused();
            }

            if (!snowballs.IsEmpty())
            {
                Snowball snowball = snowballs[MBRandom.RandomInt(0, (snowballs.Count-1))];

                string clanId = get_clan_id(snowball);
                TextObject clanName = snowball.Name;
                Banner clanBanner = new Banner(snowball.Banner);



                Clan newClan = Clan.CreateClan(clanId);

                Settlement kingdomSettlement = get_kingdom_settlement(kingdom);

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

                Snowball.remove_used_snowball(snowball);
            }
            else
            {
                // print_no_snowballs_left() -- no, becouse it's spam
            }
        }

        private string get_clan_id(Snowball snowball)
        {
            if (snowball.Id == null)
            {
                return "sb_clan_" + Clan.All.Count;
            }
            else
            {
                return snowball.Id;
            }
        }




    }
}
