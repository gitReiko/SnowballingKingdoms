using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace SnowballingKingdoms 
{
    internal class Snowball : MBObjectBase
    {
        public string Id { get; set; }
        public TextObject Name { get; set; }

        public string Banner {  get; set; }

        public string Kingdom { get; set; }

        public string Culture { get; set; }

        public static MBReadOnlyList<Snowball> AllUnusedSnowballs { get; private set; }

        public override void Deserialize(MBObjectManager objectManager, XmlNode node)
        {
            base.Deserialize(objectManager, node);
            this.Id = node.Attributes.GetNamedItem("id").Value.ToString();
            this.Name = new TextObject(node.Attributes.GetNamedItem("name").Value, null);
            this.Kingdom = node.Attributes.GetNamedItem("kingdom").Value.ToString();
            this.Culture = node.Attributes.GetNamedItem("culture").Value.ToString();
            this.Banner = node.Attributes.GetNamedItem("banner").Value.ToString();
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
            List<string> culturies = get_all_kingdom_culturies(kingdom);

            foreach(string culture in culturies)
            {
                foreach (Snowball snowball in Snowball.AllUnusedSnowballs)
                {
                    if ((culture == snowball.Culture) && is_clan_unused(snowball))
                    {
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
                    unused.Add(snowball);
                }
            }

            return unused;
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

        private static List<string> get_all_kingdom_culturies(Kingdom kingdom)
        {
            List<string> culturies = new List<string>();

            foreach(Settlement settlement in kingdom.Settlements)
            {
                if(!is_culture_already_exists(culturies, settlement.Culture.StringId))
                {
                    culturies.Add(settlement.Culture.StringId);
                }
            }

            return culturies;
        }

        private static bool is_culture_already_exists(List<string> culturies, string settlementCulture)
        {
            foreach(string culture in culturies)
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
