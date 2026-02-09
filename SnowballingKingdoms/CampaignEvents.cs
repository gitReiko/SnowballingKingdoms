using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.ObjectSystem;

namespace SnowballingKingdoms
{
    internal class SnowballEvents : CampaignBehaviorBase
    {

        public override void SyncData(IDataStore dataStore) { }

        public override void RegisterEvents()
        {
            CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, ClanCreation);
            CampaignEvents.ClanTierIncrease.AddNonSerializedListener(this, AddMemberToClan);
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

        private void AddMemberToClan(Clan clan, bool shouldNotify)
        {
            if (
                SnowConfig.AddNewMemberAfterClanTierIncrease
                && clan.IsClan
                && clan.IsMinorFaction == false
                && clan.IsEliminated == false
                && Clan.PlayerClan != clan
                && clan.AliveLords.Count < SnowConfig.NewMembersLimitAfterClanTierIncrease
             ) {
                ClanMembersGenerator.add_member_to_clan(clan);
            }
        }

        private bool is_clan_necessary_to_create(Kingdom kingdom)
        {
            // debug
            /*
            InformationManager.DisplayMessage(new InformationMessage("settle" + get_settlement_count(kingdom), Color.ConvertStringToColor("#FF0042FF")));
            InformationManager.DisplayMessage(new InformationMessage("clan" + get_clans_count(kingdom), Color.ConvertStringToColor("#FF0042FF")));
            InformationManager.DisplayMessage(new InformationMessage("res"+ settlementsRate, Color.ConvertStringToColor("#FF0042FF")));
            */

            if (is_settlements_enough_to_create_new_clan(kingdom))
            {
                if (SnowConfig.OnlyAIExpand && SnowConfig.OnlyPlayerExpand)
                {
                    return true;
                }
                else if (SnowConfig.OnlyAIExpand)
                {
                    return !is_player_vassal_of_kingdom(kingdom);
                }
                else if (SnowConfig.OnlyPlayerExpand)
                {
                    return is_player_vassal_of_kingdom(kingdom);
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        private bool is_settlements_enough_to_create_new_clan(Kingdom kingdom)
        {
            float settlementsCount = get_settlement_count(kingdom);
            float settlementsRate = settlementsCount / get_clans_count(kingdom);

            if (settlementsCount < 3 && settlementsRate > SnowConfig.ClanCreationFactorLessThan3Settlements )
            {
                return true;
            }

            if (settlementsCount < 6 && settlementsRate > SnowConfig.ClanCreationFactorLessThan6Settlements)
            {
                return true;
            }

            if (settlementsCount < 10 && settlementsRate > SnowConfig.ClanCreationFactorLessThan10Settlements)
            {
                return true;
            }

            if (settlementsCount < 20 && settlementsRate > SnowConfig.ClanCreationFactorLessThan20Settlements)
            {
                return true;
            }

            if (settlementsCount < 30 && settlementsRate > SnowConfig.ClanCreationFactorLessThan30Settlements)
            {
                return true;
            }

            if (settlementsCount < 40 && settlementsRate > SnowConfig.ClanCreationFactorLessThan40Settlements)
            {
                return true;
            }

            if (settlementsCount < 50 && settlementsRate > SnowConfig.ClanCreationFactorLessThan50Settlements)
            {
                return true;
            }

            if (settlementsCount < 60 && settlementsRate > SnowConfig.ClanCreationFactorLessThan60Settlements)
            {
                return true;
            }

            if (settlementsCount < 70 && settlementsRate > SnowConfig.ClanCreationFactorLessThan70Settlements)
            {
                return true;
            }

            if (settlementsCount < 80 && settlementsRate > SnowConfig.ClanCreationFactorLessThan80Settlements)
            {
                return true;
            }

            if (settlementsCount < 90 && settlementsRate > SnowConfig.ClanCreationFactorLessThan90Settlements)
            {
                return true;
            }

            return settlementsCount > 89 && settlementsRate > SnowConfig.ClanCreationFactorMoreThan90Settlements;
        }

        private bool is_player_vassal_of_kingdom(Kingdom kingdom)
        {
            MBGUID playerId = Clan.PlayerClan.Id;

            foreach (Clan clan in kingdom.Clans)
            {
                if(clan.Id == playerId)
                {
                    if(clan.IsUnderMercenaryService)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return false;
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
            List<Snowball> snowballs = Snowball.get_all_unused_for_kingdom(kingdom.StringId);

            if (snowballs.IsEmpty())
            {
                snowballs = Snowball.get_all_unused_for_kingdom_main_culture(kingdom.Culture.StringId);
            }

            if (snowballs.IsEmpty())
            {
                snowballs = Snowball.get_all_unused_for_kingdom_cultures(kingdom);
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
                
                Clan newClan = Clan.CreateClan(clanId);

                TextObject clanName = snowball.Name;
                newClan.ChangeClanName(clanName, clanName);

                newClan.Culture = clanCulture;

                Banner clanBanner = new Banner(snowball.Banner);
                newClan.Banner = clanBanner;

                Settlement kingdomSettlement = get_kingdom_settlement(kingdom);
                newClan.SetInitialHomeSettlement(kingdomSettlement);

                newClan.IsNoble = true;
                newClan.AddRenown(200f);
                newClan.Influence = 100f;

                newClan.Color = clanBanner.GetPrimaryColor();
                newClan.Color2 = clanBanner.GetSecondaryColor();

                newClan.Kingdom = kingdom;

                List<Hero> heros = ClanMembersGenerator.GenerateClanMemeber(newClan, kingdomSettlement);
                newClan.SetLeader(heros[0]);

                foreach (Hero hero in heros)
                {
                    hero.ChangeState(Hero.CharacterStates.Active);
                }

                ChangeKingdomAction.ApplyByJoinToKingdom(newClan, kingdom);

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

            Color color = Color.FromUint(kingdom.PrimaryBannerColor);

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