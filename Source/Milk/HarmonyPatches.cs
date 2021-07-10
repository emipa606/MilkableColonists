using HarmonyLib;
using Verse;

namespace Milk
{
    // Token: 0x02000005 RID: 5
    [StaticConstructorOnStartup]
    internal static class HarmonyPatches
    {
        // Token: 0x0600000C RID: 12 RVA: 0x00002386 File Offset: 0x00000586
        static HarmonyPatches()
        {
            new Harmony("rimworld.Ziehn.MilkableColonists").PatchAll();
        }
    }
}