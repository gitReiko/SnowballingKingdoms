using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace SnowballingKingdoms
{
    internal class SnowConfig : MBObjectBase 
    {
        const bool DEFAULT_ADD_NEW_MEMBER_AFTER_CLAN_TIER_INCREASE = true;
        const bool DEFAULT_ONLY_AI_EXPAND = false;
        const bool DEFAULT_ONLY_PLAYER_EXPAND = false;
        const ushort DEFAULT_CLAN_MEMBERS_LIMIT = 8;
        const byte DEFAULT_CREATE_EVERY_DAYS_MIN = 1;
        const float DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_10_SETTLEMENTS = 2.3f;
        const float DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_20_SETTLEMENTS = 2.5f;
        const float DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_30_SETTLEMENTS = 3.0f;
        const float DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_3_SETTLEMENTS = 1.5f;
        const float DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_40_SETTLEMENTS = 3.0f;
        const float DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_50_SETTLEMENTS = 3.3f;
        const float DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_60_SETTLEMENTS = 3.3f;
        const float DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_6_SETTLEMENTS = 2.0f;
        const float DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_70_SETTLEMENTS = 3.5f;
        const float DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_80_SETTLEMENTS = 3.5f;
        const float DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_90_SETTLEMENTS = 3.5f;
        const float DEFAULT_CLAN_CREATION_FACTOR_MORE_THAN_90_SETTLEMENTS = 4.0f;

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
            this.ClanCreationFactorLessThan3Settlements_ = this.GetFloat(
                node, 
                "clan_creation_factor_less_then_3_settlements", 
                SnowConfig.DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_3_SETTLEMENTS
            );
        }

        private void init_clan_creation_factor_less_than_6_settlements(XmlNode node)
        {
            this.ClanCreationFactorLessThan6Settlements_ = this.GetFloat(
                node,
                "clan_creation_factor_less_then_6_settlements",
                SnowConfig.DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_6_SETTLEMENTS
            );
        }

        private void init_clan_creation_factor_less_than_10_settlements(XmlNode node)
        {
            this.ClanCreationFactorLessThan10Settlements_ = this.GetFloat(
                node,
                "clan_creation_factor_less_then_10_settlements",
                SnowConfig.DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_10_SETTLEMENTS
            );
        }

        private void init_clan_creation_factor_less_than_20_settlements(XmlNode node)
        {
            this.ClanCreationFactorLessThan20Settlements_ = this.GetFloat(
                node,
                "clan_creation_factor_less_then_20_settlements",
                SnowConfig.DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_20_SETTLEMENTS
            );
        }

        private void init_clan_creation_factor_less_than_30_settlements(XmlNode node)
        {
            this.ClanCreationFactorLessThan30Settlements_ = this.GetFloat(
                node,
                "clan_creation_factor_less_then_30_settlements",
                SnowConfig.DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_30_SETTLEMENTS
            );
        }

        private void init_clan_creation_factor_less_than_40_settlements(XmlNode node)
        {
            this.ClanCreationFactorLessThan40Settlements_ = this.GetFloat(
                node,
                "clan_creation_factor_less_then_40_settlements",
                SnowConfig.DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_40_SETTLEMENTS
            );
        }

        private void init_clan_creation_factor_less_than_50_settlements(XmlNode node)
        {
            this.ClanCreationFactorLessThan50Settlements_ = this.GetFloat(
                node,
                "clan_creation_factor_less_then_50_settlements",
                SnowConfig.DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_50_SETTLEMENTS
            );
        }

        private void init_clan_creation_factor_less_than_60_settlements(XmlNode node)
        {
            this.ClanCreationFactorLessThan60Settlements_ = this.GetFloat(
                node,
                "clan_creation_factor_less_then_60_settlements",
                SnowConfig.DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_60_SETTLEMENTS
            );
        }

        private void init_clan_creation_factor_less_than_70_settlements(XmlNode node)
        {
            this.ClanCreationFactorLessThan70Settlements_ = this.GetFloat(
                node,
                "clan_creation_factor_less_then_70_settlements",
                SnowConfig.DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_70_SETTLEMENTS
            );
        }

        private void init_clan_creation_factor_less_than_80_settlements(XmlNode node)
        {
            this.ClanCreationFactorLessThan80Settlements_ = this.GetFloat(
                node,
                "clan_creation_factor_less_then_80_settlements",
                SnowConfig.DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_80_SETTLEMENTS
            );
        }

        private void init_clan_creation_factor_less_than_90_settlements(XmlNode node)
        {
            this.ClanCreationFactorLessThan90Settlements_ = this.GetFloat(
                node,
                "clan_creation_factor_less_then_90_settlements",
                SnowConfig.DEFAULT_CLAN_CREATION_FACTOR_LESS_THAN_90_SETTLEMENTS
            );
        }

        private void init_clan_creation_factor_more_than_90_settlements(XmlNode node)
        {
            this.ClanCreationFactorMoreThan90Settlements_ = this.GetFloat(
                node,
                "clan_creation_factor_more_then_90_settlements",
                SnowConfig.DEFAULT_CLAN_CREATION_FACTOR_MORE_THAN_90_SETTLEMENTS
            );
        }

        private void init_create_every_days(XmlNode node)
        {
            this.CreateEveryDays_ = this.GetUShort(
                node,
                "create_every_days",
                SnowConfig.DEFAULT_CREATE_EVERY_DAYS_MIN
            );
        }

        private void init_only_player_expand(XmlNode node)
        {
            this.OnlyPlayerExpand_ = this.GetBool(
                node,
                "only_player_expand",
                SnowConfig.DEFAULT_ONLY_PLAYER_EXPAND
            );
        }

        private void init_only_ai_expand(XmlNode node)
        {
            this.OnlyAIExpand_ = this.GetBool(
                node,
                "only_ai_expand",
                SnowConfig.DEFAULT_ONLY_AI_EXPAND
            );
        }

        private void init_add_new_member_after_clan_tier_increase(XmlNode node)
        {
            this.AddNewMemberAfterClanTierIncrease_ = this.GetBool(
                node,
                "only_ai_expand",
                SnowConfig.DEFAULT_ADD_NEW_MEMBER_AFTER_CLAN_TIER_INCREASE
            );
        }

        private void init_new_members_limit_after_clan_tier_increase(XmlNode node)
        {
            this.NewMembersLimitAfterClanTierIncrease_ = this.GetUShort(
                node,
                "new_members_limit_after_clan_tier_increase",
                SnowConfig.DEFAULT_CLAN_MEMBERS_LIMIT
            );
        }

        private static List<SnowConfig> All
        {
            get
            {
                return MBObjectManager.Instance.GetObjectTypeList<SnowConfig>();
            }
        }

        public static void init_config()
        {
            if (All == null || All.Count == 0)
            {
                Debug.Print("[SnowballingKingdoms] SnowConfig not found!", 0, Debug.DebugColor.Red);
                return;
            }

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

        private float GetFloat(XmlNode node, string attr, float defaultValue)
        {
            if (node?.Attributes == null)
                return defaultValue;

            XmlNode a = node.Attributes.GetNamedItem(attr);
            if (a == null || string.IsNullOrWhiteSpace(a.Value))
                return defaultValue;

            if (float.TryParse(a.Value, System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture, out float result))
                return result;

            return defaultValue;
        }

        private ushort GetUShort(XmlNode node, string attr, ushort defaultValue)
        {
            if (node?.Attributes == null)
                return defaultValue;

            XmlNode a = node.Attributes.GetNamedItem(attr);
            if (a == null)
                return defaultValue;

            if (ushort.TryParse(a.Value, out ushort result))
                return result;

            return defaultValue;
        }

        private bool GetBool(XmlNode node, string attr, bool defaultValue)
        {
            if (node?.Attributes == null)
                return defaultValue;

            XmlNode a = node.Attributes.GetNamedItem(attr);
            if (a == null)
                return defaultValue;

            if (a.Value == "1") return true;
            if (a.Value == "0") return false;

            if (bool.TryParse(a.Value, out bool result))
                return result;

            return defaultValue;
        }
    }

}
