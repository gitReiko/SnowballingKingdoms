using System;
using System.Runtime.CompilerServices;
using HarmonyLib;
using TaleWorlds.ObjectSystem;

namespace SnowballingKingdoms
{
    // Token: 0x02000043 RID: 67
    [HarmonyPatch(typeof(MBObjectManager), "GetMergedXmlForManaged")]
    public class MBObjectManager_GetMergedXmlForManaged_SkipValidationPatch
    {
        // Token: 0x060001E0 RID: 480 RVA: 0x00011C9C File Offset: 0x0000FE9C
        [HarmonyPrefix]
        public static bool SkipValidation(string id, ref bool skipValidation)
        {
            bool flag = id == "Snowballs";
            if (flag)
            {
                skipValidation = true;
            }
            return true;
        }
    }
}
