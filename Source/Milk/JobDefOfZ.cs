using RimWorld;
using Verse;

namespace Milk
{
    // Token: 0x0200000B RID: 11
    [DefOf]
    public static class JobDefOfZ
    {
        // Token: 0x0400000A RID: 10
        public static JobDef MilkHuman;

        // Token: 0x06000025 RID: 37 RVA: 0x00002984 File Offset: 0x00000B84
        static JobDefOfZ()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(JobDefOf));
        }
    }
}