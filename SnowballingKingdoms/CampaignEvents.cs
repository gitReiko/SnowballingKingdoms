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
            // For debug 
            //CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, WeeklyClanCreate);

            CampaignEvents.WeeklyTickEvent.AddNonSerializedListener(this, WeeklyClanCreate);
        }

        private void WeeklyClanCreate()
        {
            foreach(Kingdom kingdom in Kingdom.All)
            {
                if(is_clan_necessary_to_create(kingdom))
                {
                    create_new_clan(kingdom.Culture, kingdom);
                }
            }
        }

        private bool is_clan_necessary_to_create(Kingdom kingdom)
        {
            float settlementsRate = get_settlement_count(kingdom) / get_clans_count(kingdom);

            // debug
            /*
            InformationManager.DisplayMessage(new InformationMessage("settle" + get_settlement_count(kingdom), Color.ConvertStringToColor("#FF0042FF")));
            InformationManager.DisplayMessage(new InformationMessage("clan" + get_clans_count(kingdom), Color.ConvertStringToColor("#FF0042FF")));
            InformationManager.DisplayMessage(new InformationMessage("res"+ settlementsRate, Color.ConvertStringToColor("#FF0042FF")));
            */

            if (settlementsRate > SnowConfig.ClanCreationFactor)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private float get_settlement_count(Kingdom kingdom)
        {
            int count = 0;

            foreach (Settlement settlement in kingdom.Settlements)
            {
                if (settlement.IsCastle || settlement.IsTown)
                {
                    count++;
                }
            }

            return (float) count;
        }

        private float get_clans_count(Kingdom kingdom)
        {
            int count = 0;

            foreach (Clan clan in kingdom.Clans)
            {
                if (clan.IsMinorFaction == false)
                {
                    count++;
                }
            }

            return (float) count;
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

                if(snowball.SettlementCulture != null)
                {
                    clanCulture = snowball.SettlementCulture;
                }

                string clanId = get_clan_id(snowball);
                TextObject clanName = snowball.Name;
                Banner clanBanner = new Banner(snowball.Banner);

                Clan newClan = Clan.CreateClan(clanId);

                Settlement kingdomSettlement = get_kingdom_settlement(kingdom);

                newClan.InitializeClan(clanName, clanName, clanCulture, clanBanner, kingdomSettlement.GatePosition);
                newClan.IsNoble = true;
                newClan.UpdateHomeSettlement(kingdomSettlement);
                newClan.AddRenown(350f);
                newClan.Influence = 100f;

                newClan.Color = clanBanner.GetPrimaryColor();
                newClan.Color2 = clanBanner.GetSecondaryColor();
                newClan.AlternativeColor = clanBanner.GetPrimaryColor();
                newClan.AlternativeColor2 = clanBanner.GetSecondaryColor();

                newClan.Kingdom = kingdom;

                List<Hero> heros = ClanMembersGenerator.GenerateClanMemeber(newClan, kingdomSettlement);
                newClan.SetLeader(heros[0]);

                //newClan.CreateNewMobileParty(heros[0]);

                Snowball.remove_used_snowball(snowball);

                print_clan_created(kingdom, snowball);
            }
            else
            {
                // print_no_snowballs_left() -- no, becouse it's spam
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

        private void print_clan_created(Kingdom kingdom, Snowball snowball)
        {
            TextObject str1 = new TextObject("{=snow.clan_created1}The growing power of", null);
            TextObject str2 = new TextObject("{=snow.clan_created2}led to the creation of", null);

            string message = str1.ToString() + " " + kingdom.Name.ToString() + " " + str2.ToString() + " " + snowball.Name.ToString() + ".";

            Color color = Color.FromUint(kingdom.LabelColor);

            InformationManager.DisplayMessage(new InformationMessage(message, color));
        }

        /*
        private void log_start_of_snowball_creation(Snowball snowball)
        {

            // crash game 

            Logger snow = new Logger("snowballs", true, true, false);
            snow.Print("snowballs_event");
        }
        */




    }
}
