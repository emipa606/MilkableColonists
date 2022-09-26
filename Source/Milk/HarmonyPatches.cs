using System.Reflection;
using HarmonyLib;
using Verse;

namespace Milk;

[StaticConstructorOnStartup]
internal static class HarmonyPatches
{
    static HarmonyPatches()
    {
        new Harmony("rimworld.Ziehn.MilkableColonists").PatchAll(Assembly.GetExecutingAssembly());
    }
}