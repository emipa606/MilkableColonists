using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Milk
{
    // Token: 0x02000009 RID: 9
    public class WorkGiver_MilkHuman : WorkGiver_GatherHumanBodyResources
    {
        // Token: 0x17000010 RID: 16
        // (get) Token: 0x0600001F RID: 31 RVA: 0x0000295E File Offset: 0x00000B5E
        protected override JobDef JobDef => JobDefOfZ.MilkHuman;

        // Token: 0x06000020 RID: 32 RVA: 0x00002965 File Offset: 0x00000B65
        protected override HumanCompHasGatherableBodyResource GetComp(Pawn animal)
        {
            return animal.TryGetComp<CompMilkableHuman>();
        }
        protected override IEnumerable<HumanCompHasGatherableBodyResource> GetComps(Pawn pawn)
        {
            List<ThingComp> comps = pawn.AllComps;
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