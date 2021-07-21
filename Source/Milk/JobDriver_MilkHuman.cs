using Verse;
using System.Collections.Generic;
using System.Linq;

namespace Milk
{
    // Token: 0x0200000A RID: 10
    public class JobDriver_MilkHuman : JobDriver_GatherHumanBodyResources
    {
        // Token: 0x17000011 RID: 17
        // (get) Token: 0x06000022 RID: 34 RVA: 0x00002975 File Offset: 0x00000B75
        protected override float WorkTotal => 400f;

        // Token: 0x06000023 RID: 35 RVA: 0x00002965 File Offset: 0x00000B65
        protected override HumanCompHasGatherableBodyResource GetComp(Pawn animal)
        {
            return animal.TryGetComp<CompMilkableHuman>();
        }
        protected override IEnumerable<HumanCompHasGatherableBodyResource> GetComps(Pawn animal)
        {
            List<ThingComp> comps = animal.AllComps;
            foreach (ThingComp comp in comps)
            {
                if (comp is CompMilkableHuman)
                {
                    //Log.Message("Returned Comp");
                    yield return (CompMilkableHuman)comp;
                }
            }
            //Log.Message("Done");
        }
    }
}