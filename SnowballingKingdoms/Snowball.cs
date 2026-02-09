using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace SnowballingKingdoms 
{
    internal class Snowball : MBObjectBase
    {
        public string Id { get; private set; }

        public TextObject Name { get; private set; }

        public string Banner { get; private set; }

        public string Kingdom { get; private set; }

        public string Culture { get; private set; }

        public int Priority { get; private set; }

        public CultureObject SettlementCulture { get; private set; }

        public static List<Snowball> AllUnusedSnowballs { get; private set; }

        public override void Deserialize(MBObjectManager objectManager, XmlNode node)
        {
            if (is_necessary_attributes_exists_and_valid(node))
            {
                base.Deserialize(objectManager, node);

                handle_id_attr(node);
                handle_name_attr(node);
                handle_banner_attr(node);
                handle_kingdom_attr(node);
                handle_culture_attr(node);
                handle_priority_attr(node);
                this.SettlementCulture = null;
            }
        }

        private bool is_necessary_attributes_exists_and_valid(XmlNode node)
        {
            if (
                (node.Attributes == null)
                ||
                (node.Attributes["id"] == null)
                ||
                is_name_empty(node)
                ||
                is_banner_empty(node)
            )
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool is_name_empty(XmlNode node)
        {
            if (node.Attributes["name"] == null)
            {
                return true;
            }
            else
            {
                TextObject Name = new TextObject(node.Attributes.GetNamedItem("name").Value, null);
                string NameWithoutLocalization = Name.ToString();

                if(String.IsNullOrEmpty(NameWithoutLocalization))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool is_banner_empty(XmlNode node)
        {
            if (node.Attributes["banner"] == null)
            {
                return true;
            }
            else
            {
                string banner = node.Attributes.GetNamedItem("banner").Value.ToString();

                if (String.IsNullOrEmpty(banner))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void handle_id_attr(XmlNode node)
        {
            if (node.Attributes["id"] != null)
            {
                this.Id = node.Attributes.GetNamedItem("id").Value.ToString();
            }
            else
            {
                this.Id = null;
            }
        }

        private void handle_name_attr(XmlNode node)
        {
            this.Name = new TextObject(node.Attributes.GetNamedItem("name").Value, null);

        }

        private void handle_banner_attr(XmlNode node)
        {
            this.Banner = node.Attributes.GetNamedItem("banner").Value.ToString();
        }

        private void handle_kingdom_attr(XmlNode node)
        {
            if (node.Attributes["kingdom"] != null)
            {
                this.Kingdom = node.Attributes.GetNamedItem("kingdom").Value.ToString();
            }
            else
            {
                this.Kingdom = null;
            }
        }

        private void handle_culture_attr(XmlNode node)
        {
            if (node.Attributes["culture"] != null)
            {
                this.Culture = node.Attributes.GetNamedItem("culture").Value.ToString();
            }
            else
            {
                this.Culture = null;
            }
        }

        private void handle_priority_attr(XmlNode node)
        {
            if (node.Attributes["priority"] != null &&
                int.TryParse(node.Attributes["priority"].Value, out int priority))
            {
                this.Priority = priority;
            }
            else
            {
                this.Priority = 1;
            }
        }

        public static void initialize_unused_snowballs()
        {
            List<Snowball> unused = new List<Snowball>();

            foreach (Snowball snowball in Snowball.All)
            {
                if (is_clan_unused(snowball))
                {
                    unused.Add(snowball);
                }
            }

            Snowball.AllUnusedSnowballs = unused;
        }

        public static void remove_used_snowball(Snowball snowball)
        {
            if (Snowball.AllUnusedSnowballs == null)
                Snowball.initialize_unused_snowballs();

            Snowball.AllUnusedSnowballs.Remove(snowball);
        }

        public static List<Snowball> get_all_unused_for_kingdom(string kingdomId)
        {
            if (Snowball.AllUnusedSnowballs == null)
                Snowball.initialize_unused_snowballs();

            List<Snowball> unused = new List<Snowball>();

            foreach (Snowball snowball in Snowball.AllUnusedSnowballs)
            {
                if ( (kingdomId == snowball.Kingdom) && is_clan_unused(snowball))
                {
                    unused.Add(snowball);
                }
            }

            unused = get_snowballs_with_highest_priority(unused);

            return unused;
        }

        public static List<Snowball> get_all_unused_for_kingdom_main_culture(string cultureId)
        {
            if (Snowball.AllUnusedSnowballs == null)
                Snowball.initialize_unused_snowballs();

            List<Snowball> unused = new List<Snowball>();

            foreach (Snowball snowball in Snowball.AllUnusedSnowballs)
            {
                if ((cultureId == snowball.Culture) && is_clan_unused(snowball))
                {
                    unused.Add(snowball);
                }
            }

            unused = get_snowballs_with_highest_priority(unused);

            return unused;
        }

        public static List<Snowball> get_all_unused_for_kingdom_cultures(Kingdom kingdom)
        {
            if (Snowball.AllUnusedSnowballs == null)
                Snowball.initialize_unused_snowballs();

            List<Snowball> unused = new List<Snowball>();
            List<CultureObject> cultures = get_all_kingdom_cultures(kingdom);

            foreach(CultureObject culture in cultures)
            {
                foreach (Snowball snowball in Snowball.AllUnusedSnowballs)
                {
                    if ((culture.StringId == snowball.Culture) && is_clan_unused(snowball))
                    {
                        snowball.SettlementCulture = culture;
                        unused.Add(snowball);
                    }
                }
            }

            unused = get_snowballs_with_highest_priority(unused);

            return unused;
        }

        public static List<Snowball> get_all_unused()
        {
            if (Snowball.AllUnusedSnowballs == null)
                Snowball.initialize_unused_snowballs();

            List<Snowball> unused = new List<Snowball>();

            foreach (Snowball snowball in Snowball.AllUnusedSnowballs)
            {
                if (is_clan_unused(snowball))
                {
                    // It possible that next function may create a heavy load
                    snowball.SettlementCulture = get_culture_for_random_snowball(snowball);
                    unused.Add(snowball);
                }
            }

            return unused;
        }

        private static CultureObject get_culture_for_random_snowball(Snowball snowball)
        {
            foreach(Settlement settle in Settlement.All)
            {
                if(snowball.Culture == settle.Culture.StringId)
                {
                    return settle.Culture;
                }
            }

            return null;
        }

        private static List<Snowball> All
        {
            get
            {
                return MBObjectManager.Instance.GetObjectTypeList<Snowball>();
            }
        }

        private static bool is_clan_unused(Snowball snowball)
        {
            if (Clan.All == null)
                return true;

            foreach (Clan clan in Clan.All)
            {
                if (clan.Name?.ToString() == snowball.Name?.ToString()
                    || clan.StringId == snowball.Id
                ) {
                    return false;
                }
            }

            return true;
        }

        private static List<CultureObject> get_all_kingdom_cultures(Kingdom kingdom)
        {
            List<CultureObject> cultures = new List<CultureObject>();

            foreach(Settlement settlement in kingdom.Settlements)
            {
                if(!is_culture_already_exists(cultures, settlement.Culture))
                {
                    cultures.Add(settlement.Culture);
                }
            }

            return cultures;
        }

        private static bool is_culture_already_exists(List<CultureObject> cultures, CultureObject settlementCulture)
        {
            foreach(CultureObject culture in cultures)
            {
                if(culture == settlementCulture)
                {
                    return true;
                }
            }

            return false;
        }

        private static List<Snowball> get_snowballs_with_highest_priority(List<Snowball> snowballs)
        {
            int highest_priority = get_highest_priority(snowballs);

            List<Snowball> prioritySnowballs = new List<Snowball>();
            foreach (Snowball snowball in snowballs)
            {
                if(snowball.Priority == highest_priority)
                {
                    prioritySnowballs.Add(snowball);
                }
            }

            return prioritySnowballs;
        }

        private static int get_highest_priority(List<Snowball> snowballs)
        {
            int highest_priority = int.MaxValue;

            foreach (Snowball snowball in snowballs)
            {
                if(highest_priority > snowball.Priority)
                {
                    highest_priority = snowball.Priority;
                }
            }

            return highest_priority;
        }



    }
}
