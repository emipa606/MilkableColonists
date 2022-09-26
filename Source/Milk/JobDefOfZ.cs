using RimWorld;
using Verse;

namespace Milk;

[DefOf]
public static class JobDefOfZ
{
    public static JobDef MilkHuman;

    static JobDefOfZ()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(JobDefOf));
    }
}