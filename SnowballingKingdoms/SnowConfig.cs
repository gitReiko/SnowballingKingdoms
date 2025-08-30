using System;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace SnowballingKingdoms
{
    internal class SnowConfig : MBObjectBase 
    {
        const byte CREATE_EVERY_DAYS_MIN = 1;
        const bool ADD_NEW_MEMBER_AFTER_CLAN_TIER_INCREASE = true;

        const float CLAN_CREATION_FACTOR_LESS_THAN_3_SETTLEMENTS_DEFAULT = 1.5f;
        const float CLAN_CREATION_FACTOR_LESS_THAN_6_SETTLEMENTS_DEFAULT = 2.0f;
        const float CLAN_CREATION_FACTOR_LESS_THAN_10_SETTLEMENTS_DEFAULT = 2.3f;
        const float CLAN_CREATION_FACTOR_LESS_THAN_20_SETTLEMENTS_DEFAULT = 2.5f;
        const float CLAN_CREATION_FACTOR_LESS_THAN_30_SETTLEMENTS_DEFAULT = 3.0f;
        const float CLAN_CREATION_FACTOR_LESS_THAN_40_SETTLEMENTS_DEFAULT = 3.0f;
        const float CLAN_CREATION_FACTOR_LESS_THAN_50_SETTLEMENTS_DEFAULT = 3.3f;
        const float CLAN_CREATION_FACTOR_LESS_THAN_60_SETTLEMENTS_DEFAULT = 3.3f;
        const float CLAN_CREATION_FACTOR_LESS_THAN_70_SETTLEMENTS_DEFAULT = 3.5f;
        const float CLAN_CREATION_FACTOR_LESS_THAN_80_SETTLEMENTS_DEFAULT = 3.5f;
        const float CLAN_CREATION_FACTOR_LESS_THAN_90_SETTLEMENTS_DEFAULT = 3.5f;
        const float CLAN_CREATION_FACTOR_MORE_THAN_90_SETTLEMENTS_DEFAULT = 4.0f;

        public static float ClanCreationFactorLessThan3Settlements { get; private set; }
        public static float ClanCreationFactorLessThan6Settlements { get; private set; }
        public static float ClanCreationFactorLessThan10Settlements { get; private set; }
        public static float ClanCreationFactorLessThan20Settlements { get; private set; }
        public static float ClanCreationFactorLessThan30Settlements { get; private set; }
        public static float ClanCreationFactorLessThan40Settlements { get; private set; }
        public static float ClanCreationFactorLessThan50Settlements { get; private set; }
        public static float ClanCreationFactorLessThan60Settlements { get; private set; }
        public static float ClanCreationFactorLessThan70Settlements { get; private set; }
        public static float ClanCreationFactorLessThan80Settlements { get; private set; }
        public static float ClanCreationFactorLessThan90Settlements { get; private set; }
        public static float ClanCreationFactorMoreThan90Settlements { get; private set; }

        public static ushort CreateEveryDays { get; private set; }
        public static bool OnlyPlayerExpand { get; private set; }
        public static bool OnlyAIExpand { get; private set; }
        public static bool AddNewMemberAfterClanTierIncrease { get; private set; }
        public static ushort NewMembersLimitAfterClanTierIncrease { get; private set; }

        private float ClanCreationFactorLessThan3Settlements_;
        private float ClanCreationFactorLessThan6Settlements_;
        private float ClanCreationFactorLessThan10Settlements_;
        private float ClanCreationFactorLessThan20Settlements_;
        private float ClanCreationFactorLessThan30Settlements_;
        private float ClanCreationFactorLessThan40Settlements_;
        private float ClanCreationFactorLessThan50Settlements_;
        private float ClanCreationFactorLessThan60Settlements_;
        private float ClanCreationFactorLessThan70Settlements_;
        private float ClanCreationFactorLessThan80Settlements_;
        private float ClanCreationFactorLessThan90Settlements_;
        private float ClanCreationFactorMoreThan90Settlements_;

        private ushort CreateEveryDays_;
        private bool OnlyPlayerExpand_;
        private bool OnlyAIExpand_;
        private bool AddNewMemberAfterClanTierIncrease_;
        private ushort NewMembersLimitAfterClanTierIncrease_;

        public override void Deserialize(MBObjectManager objectManager, XmlNode node)
        {
            base.Deserialize(objectManager, node);

            init_clan_creation_factor_less_than_3_settlements(node);
            init_clan_creation_factor_less_than_6_settlements(node);
            init_clan_creation_factor_less_than_10_settlements(node);
            init_clan_creation_factor_less_than_20_settlements(node);
            init_clan_creation_factor_less_than_30_settlements(node);
            init_clan_creation_factor_less_than_40_settlements(node);
            init_clan_creation_factor_less_than_50_settlements(node);
            init_clan_creation_factor_less_than_60_settlements(node);
            init_clan_creation_factor_less_than_70_settlements(node);
            init_clan_creation_factor_less_than_80_settlements(node);
            init_clan_creation_factor_less_than_90_settlements(node);
            init_clan_creation_factor_more_than_90_settlements(node);

            init_create_every_days(node);
            init_only_player_expand(node);
            init_only_ai_expand(node);
            init_add_new_member_after_clan_tier_increase(node);
            init_new_members_limit_after_clan_tier_increase(node);
        }

        private void init_clan_creation_factor_less_than_3_settlements(XmlNode node)
        {
            float param = Convert.ToSingle(node.Attributes.GetNamedItem("clan_creation_factor_less_then_3_settlements").Value.ToString());

            if (param == 0)
            {
                param = SnowConfig.CLAN_CREATION_FACTOR_LESS_THAN_3_SETTLEMENTS_DEFAULT;
            }

            this.ClanCreationFactorLessThan3Settlements_ = param;
        }

        private void init_clan_creation_factor_less_than_6_settlements(XmlNode node)
        {
            float param = Convert.ToSingle(node.Attributes.GetNamedItem("clan_creation_factor_less_then_6_settlements").Value.ToString());

            if (param == 0)
            {
                param = SnowConfig.CLAN_CREATION_FACTOR_LESS_THAN_6_SETTLEMENTS_DEFAULT;
            }

            this.ClanCreationFactorLessThan6Settlements_ = param;
        }

        private void init_clan_creation_factor_less_than_10_settlements(XmlNode node)
        {
            float param = Convert.ToSingle(node.Attributes.GetNamedItem("clan_creation_factor_less_then_10_settlements").Value.ToString());

            if (param == 0)
            {
                param = SnowConfig.CLAN_CREATION_FACTOR_LESS_THAN_10_SETTLEMENTS_DEFAULT;
            }

            this.ClanCreationFactorLessThan10Settlements_ = param;
        }

        private void init_clan_creation_factor_less_than_20_settlements(XmlNode node)
        {
            float param = Convert.ToSingle(node.Attributes.GetNamedItem("clan_creation_factor_less_then_20_settlements").Value.ToString());

            if (param == 0)
            {
                param = SnowConfig.CLAN_CREATION_FACTOR_LESS_THAN_20_SETTLEMENTS_DEFAULT;
            }

            this.ClanCreationFactorLessThan20Settlements_ = param;
        }

        private void init_clan_creation_factor_less_than_30_settlements(XmlNode node)
        {
            float param = Convert.ToSingle(node.Attributes.GetNamedItem("clan_creation_factor_less_then_30_settlements").Value.ToString());

            if (param == 0)
            {
                param = SnowConfig.CLAN_CREATION_FACTOR_LESS_THAN_30_SETTLEMENTS_DEFAULT;
            }

            this.ClanCreationFactorLessThan30Settlements_ = param;
        }

        private void init_clan_creation_factor_less_than_40_settlements(XmlNode node)
        {
            float param = Convert.ToSingle(node.Attributes.GetNamedItem("clan_creation_factor_less_then_40_settlements").Value.ToString());

            if (param == 0)
            {
                param = SnowConfig.CLAN_CREATION_FACTOR_LESS_THAN_40_SETTLEMENTS_DEFAULT;
            }

            this.ClanCreationFactorLessThan40Settlements_ = param;
        }

        private void init_clan_creation_factor_less_than_50_settlements(XmlNode node)
        {
            float param = Convert.ToSingle(node.Attributes.GetNamedItem("clan_creation_factor_less_then_50_settlements").Value.ToString());

            if (param == 0)
            {
                param = SnowConfig.CLAN_CREATION_FACTOR_LESS_THAN_50_SETTLEMENTS_DEFAULT;
            }

            this.ClanCreationFactorLessThan50Settlements_ = param;
        }

        private void init_clan_creation_factor_less_than_60_settlements(XmlNode node)
        {
            float param = Convert.ToSingle(node.Attributes.GetNamedItem("clan_creation_factor_less_then_60_settlements").Value.ToString());

            if (param == 0)
            {
                param = SnowConfig.CLAN_CREATION_FACTOR_LESS_THAN_60_SETTLEMENTS_DEFAULT;
            }

            this.ClanCreationFactorLessThan60Settlements_ = param;
        }

        private void init_clan_creation_factor_less_than_70_settlements(XmlNode node)
        {
            float param = Convert.ToSingle(node.Attributes.GetNamedItem("clan_creation_factor_less_then_70_settlements").Value.ToString());

            if (param == 0)
            {
                param = SnowConfig.CLAN_CREATION_FACTOR_LESS_THAN_70_SETTLEMENTS_DEFAULT;
            }

            this.ClanCreationFactorLessThan70Settlements_ = param;
        }

        private void init_clan_creation_factor_less_than_80_settlements(XmlNode node)
        {
            float param = Convert.ToSingle(node.Attributes.GetNamedItem("clan_creation_factor_less_then_80_settlements").Value.ToString());

            if (param == 0)
            {
                param = SnowConfig.CLAN_CREATION_FACTOR_LESS_THAN_80_SETTLEMENTS_DEFAULT;
            }

            this.ClanCreationFactorLessThan80Settlements_ = param;
        }

        private void init_clan_creation_factor_less_than_90_settlements(XmlNode node)
        {
            float param = Convert.ToSingle(node.Attributes.GetNamedItem("clan_creation_factor_less_then_90_settlements").Value.ToString());

            if (param == 0)
            {
                param = SnowConfig.CLAN_CREATION_FACTOR_LESS_THAN_90_SETTLEMENTS_DEFAULT;
            }

            this.ClanCreationFactorLessThan90Settlements_ = param;
        }

        private void init_clan_creation_factor_more_than_90_settlements(XmlNode node)
        {
            float param = Convert.ToSingle(node.Attributes.GetNamedItem("clan_creation_factor_more_then_90_settlements").Value.ToString());

            if (param == 0)
            {
                param = SnowConfig.CLAN_CREATION_FACTOR_MORE_THAN_90_SETTLEMENTS_DEFAULT;
            }

            this.ClanCreationFactorMoreThan90Settlements_ = param;
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

        private void init_only_player_expand(XmlNode node)
        {
            this.OnlyPlayerExpand_ = Convert.ToBoolean(node.Attributes.GetNamedItem("only_player_expand").Value.ToString());
        }

        private void init_only_ai_expand(XmlNode node)
        {
            this.OnlyAIExpand_ = Convert.ToBoolean(node.Attributes.GetNamedItem("only_ai_expand").Value.ToString());
        }

        private void init_add_new_member_after_clan_tier_increase(XmlNode node)
        {
            this.AddNewMemberAfterClanTierIncrease_ 
                = Convert.ToBoolean(node.Attributes.GetNamedItem("add_new_member_after_clan_tier_increase").Value.ToString());
        }

        private void init_new_members_limit_after_clan_tier_increase(XmlNode node)
        {
            this.NewMembersLimitAfterClanTierIncrease_ = Convert.ToUInt16(
                node.Attributes.GetNamedItem("new_members_limit_after_clan_tier_increase").Value.ToString()
            );
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
            ClanCreationFactorLessThan3Settlements = SnowConfig.All[0].ClanCreationFactorLessThan3Settlements_;
            ClanCreationFactorLessThan6Settlements = SnowConfig.All[0].ClanCreationFactorLessThan6Settlements_;
            ClanCreationFactorLessThan10Settlements = SnowConfig.All[0].ClanCreationFactorLessThan10Settlements_;
            ClanCreationFactorLessThan20Settlements = SnowConfig.All[0].ClanCreationFactorLessThan20Settlements_;
            ClanCreationFactorLessThan30Settlements = SnowConfig.All[0].ClanCreationFactorLessThan30Settlements_;
            ClanCreationFactorLessThan40Settlements = SnowConfig.All[0].ClanCreationFactorLessThan40Settlements_;
            ClanCreationFactorLessThan50Settlements = SnowConfig.All[0].ClanCreationFactorLessThan50Settlements_;
            ClanCreationFactorLessThan60Settlements = SnowConfig.All[0].ClanCreationFactorLessThan60Settlements_;
            ClanCreationFactorLessThan70Settlements = SnowConfig.All[0].ClanCreationFactorLessThan70Settlements_;
            ClanCreationFactorLessThan80Settlements = SnowConfig.All[0].ClanCreationFactorLessThan80Settlements_;
            ClanCreationFactorLessThan90Settlements = SnowConfig.All[0].ClanCreationFactorLessThan90Settlements_;
            ClanCreationFactorMoreThan90Settlements = SnowConfig.All[0].ClanCreationFactorMoreThan90Settlements_;

            CreateEveryDays = SnowConfig.All[0].CreateEveryDays_;
            OnlyPlayerExpand = SnowConfig.All[0].OnlyPlayerExpand_;
            OnlyAIExpand = SnowConfig.All[0].OnlyAIExpand_;
            AddNewMemberAfterClanTierIncrease = SnowConfig.All[0].AddNewMemberAfterClanTierIncrease_;
            NewMembersLimitAfterClanTierIncrease = SnowConfig.All[0].NewMembersLimitAfterClanTierIncrease_;
        }

    }

}
