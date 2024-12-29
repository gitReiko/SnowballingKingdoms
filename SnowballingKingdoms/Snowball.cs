using System;
using System.Xml;
using TaleWorlds.CampaignSystem;
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

        public override void Deserialize(MBObjectManager objectManager, XmlNode node)
        {
            base.Deserialize(objectManager, node);
            this.Id = node.Attributes.GetNamedItem("id").Value.ToString();
            this.Name = new TextObject(node.Attributes.GetNamedItem("name").Value, null);
            this.Kingdom = node.Attributes.GetNamedItem("kingdom").Value.ToString();
            this.Culture = node.Attributes.GetNamedItem("culture").Value.ToString();
            this.Banner = node.Attributes.GetNamedItem("banner").Value.ToString();
        }

        public static MBReadOnlyList<Snowball> get_all_unused_for_kingdom(string kingdomId)
        {
            MBReadOnlyList<Snowball> unused = new MBReadOnlyList<Snowball>();

            foreach (Snowball snowball in Snowball.All)
            {
                if ( (kingdomId == snowball.Kingdom) && is_clan_unused(snowball))
                {
                    unused.Add(snowball);
                }
            }

            return unused;
        }

        public static MBReadOnlyList<Snowball> get_all_unused_for_culture(string cultureId)
        {
            MBReadOnlyList<Snowball> unused = new MBReadOnlyList<Snowball>();

            foreach (Snowball snowball in Snowball.All)
            {
                if ((cultureId == snowball.Culture) && is_clan_unused(snowball))
                {
                    unused.Add(snowball);
                }
            }

            return unused;
        }

        public static MBReadOnlyList<Snowball> get_all_unused()
        {
            MBReadOnlyList<Snowball> unused = new MBReadOnlyList<Snowball>();

            foreach (Snowball snowball in Snowball.All)
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




    }
}
