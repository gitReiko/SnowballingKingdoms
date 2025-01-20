using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SnowballingKingdoms
{
    internal class SnowballEvents : CampaignBehaviorBase
    {

        public override void SyncData(IDataStore dataStore) { }

        public override void RegisterEvents()
        {
            CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, ClanCreation);
        }

        private void ClanCreation()
        {
            long daysNow = Convert.ToInt64(CampaignTime.Now.ToDays);

            if ((daysNow % SnowConfig.CreateEveryDays) == 0)
            {
                foreach (Kingdom kingdom in Kingdom.All)
                {
                    if (is_clan_necessary_to_create(kingdom))
                    {
                        create_new_clan(kingdom.Culture, kingdom);
                    }
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

            print_no_settlements_error(kingdom.Name);
            return null;
        }

        private void print_no_settlements_error(TextObject kingdomName)
        {
            TextObject message = new TextObject("{=sbin.error_no_kingdom_settlements} Error: kingdom {KINGDOM} has no settlements.");
            message.SetTextVariable("KINGDOM", kingdomName);

            InformationManager.DisplayMessage(new InformationMessage(message.ToString(), TaleWorlds.Library.Color.ConvertStringToColor("#FF0042FF")));
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
            TextObject message = new TextObject("{=sbin.clan_created} The growing power of {KINGDOM} led to the creation of {SNOWBALL}.");
            message.SetTextVariable("KINGDOM", kingdom.Name);
            message.SetTextVariable("SNOWBALL", snowball.Name);

            Color color = Color.FromUint(kingdom.LabelColor);

            InformationManager.DisplayMessage(new InformationMessage(message.ToString(), color));
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
