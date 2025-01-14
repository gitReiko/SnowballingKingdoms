using System;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace SnowballingKingdoms
{
    internal class SnowConfig : MBObjectBase 
    {

        public static float ClanCreationFactor { get; private set; }
        public static ushort CreateEveryDays { get; private set; }

        private float ClanCreationFactor_;

        private ushort CreateEveryDays_;

        const byte CREATE_EVERY_DAYS_MIN = 1;
        const float CLAN_CREATION_FACTOR_DEFAULT = 1.7f;

        public override void Deserialize(MBObjectManager objectManager, XmlNode node)
        {
            base.Deserialize(objectManager, node);

            init_clan_creation_factor(node);
            init_create_every_days(node);
        }

        private void init_clan_creation_factor(XmlNode node)
        {
            float param = Convert.ToSingle(node.Attributes.GetNamedItem("clan_creation_factor").Value.ToString());

            if (param == 0)
            {
                param = SnowConfig.CLAN_CREATION_FACTOR_DEFAULT;
            }

            this.ClanCreationFactor_ = param;
        }

        private void init_create_every_days(XmlNode node)
        {
            ushort param = Convert.ToUInt16(node.Attributes.GetNamedItem("create_every_days").Value.ToString());

            if(param == 0)
            {
                param = SnowConfig.CREATE_EVERY_DAYS_MIN;
            }

            this.CreateEveryDays_ = param;
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
            CreateEveryDays = SnowConfig.All[0].CreateEveryDays_;
        }

    }

}
