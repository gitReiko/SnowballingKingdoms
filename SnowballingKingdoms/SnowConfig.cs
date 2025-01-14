using System;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace SnowballingKingdoms
{
    internal class SnowConfig : MBObjectBase 
    {

        public static float ClanCreationFactor { get; private set; }

        private float ClanCreationFactor_;

        public override void Deserialize(MBObjectManager objectManager, XmlNode node)
        {
            base.Deserialize(objectManager, node);

            this.ClanCreationFactor_ = Convert.ToSingle(node.Attributes.GetNamedItem("clan_creation_factor").Value.ToString());
        }

        private static MBReadOnlyList<SnowConfig> All
        {
            get
            {
                return MBObjectManager.Instance.GetObjectTypeList<SnowConfig>();
            }
        }

        public static void init_config()
        {
            ClanCreationFactor = SnowConfig.All[0].ClanCreationFactor_;
        }

    }

}
