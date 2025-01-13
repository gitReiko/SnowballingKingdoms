using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;

namespace SnowballingKingdoms 
{
    internal class Snowball : MBObjectBase
    {
        public string Id { get; set; }

        public TextObject Name { get; set; }

        public string Banner { get; set; }

        public string Kingdom { get; set; }

        public string Culture { get; set; }

        public CultureObject SettlementCulture { get; private set; }

        public static MBReadOnlyList<Snowball> AllUnusedSnowballs { get; private set; }

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

        public static void initilize_unused_snowballs()
        {
            MBReadOnlyList<Snowball> unused = new MBReadOnlyList<Snowball>();

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
            Snowball.AllUnusedSnowballs.Remove(snowball);
        }

        public static MBReadOnlyList<Snowball> get_all_unused_for_kingdom(string kingdomId)
        {
            MBReadOnlyList<Snowball> unused = new MBReadOnlyList<Snowball>();

            foreach (Snowball snowball in Snowball.AllUnusedSnowballs)
            {
                if ( (kingdomId == snowball.Kingdom) && is_clan_unused(snowball))
                {


                    unused.Add(snowball);
                }
            }

            return unused;
        }

        public static MBReadOnlyList<Snowball> get_all_unused_for_kingdom_main_culture(string cultureId)
        {
            MBReadOnlyList<Snowball> unused = new MBReadOnlyList<Snowball>();

            foreach (Snowball snowball in Snowball.AllUnusedSnowballs)
            {
                if ((cultureId == snowball.Culture) && is_clan_unused(snowball))
                {
                    unused.Add(snowball);
                }
            }

            return unused;
        }

        public static MBReadOnlyList<Snowball> get_all_unused_for_kingdom_culturies(Kingdom kingdom)
        {
            MBReadOnlyList<Snowball> unused = new MBReadOnlyList<Snowball>();
            List<CultureObject> culturies = get_all_kingdom_culturies(kingdom);

            foreach(CultureObject culture in culturies)
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

            return unused;
        }

        public static MBReadOnlyList<Snowball> get_all_unused()
        {
            MBReadOnlyList<Snowball> unused = new MBReadOnlyList<Snowball>();

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

        private static MBReadOnlyList<Snowball> All
        {
            get
            {
                return MBObjectManager.Instance.GetObjectTypeList<Snowball>();
            }
        }

        private static bool is_clan_unused(Snowball snowball)
        {
            foreach(Clan clan in Clan.All)
            {
                if(clan.Name  == snowball.Name)
                {
                    return false;
                }
            }

            return true;
        }

        private static List<CultureObject> get_all_kingdom_culturies(Kingdom kingdom)
        {
            List<CultureObject> culturies = new List<CultureObject>();

            foreach(Settlement settlement in kingdom.Settlements)
            {
                if(!is_culture_already_exists(culturies, settlement.Culture))
                {
                    culturies.Add(settlement.Culture);
                }
            }

            return culturies;
        }

        private static bool is_culture_already_exists(List<CultureObject> culturies, CultureObject settlementCulture)
        {
            foreach(CultureObject culture in culturies)
            {
                if(culture == settlementCulture)
                {
                    return true;
                }
            }

            return false;
        }




    }
}
