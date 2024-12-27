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

        public override void Deserialize(MBObjectManager objectManager, XmlNode node)
        {
            base.Deserialize(objectManager, node);
            this.Id = node.Attributes.GetNamedItem("id").Value.ToString();
            this.Name = new TextObject(node.Attributes.GetNamedItem("name").Value, null);
            this.Banner = node.Attributes.GetNamedItem("banner").Value.ToString();
        }

        public static MBReadOnlyList<Snowball> All
        {
            get
            {
                return MBObjectManager.Instance.GetObjectTypeList<Snowball>();
            }
        }




    }
}
