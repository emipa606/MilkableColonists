using System.Collections.Generic;
using Verse;

namespace Milk;

public class WorkGiver_MilkHuman : WorkGiver_GatherHumanBodyResources
{
    protected override JobDef JobDef => JobDefOfZ.MilkHuman;

    protected override HumanCompHasGatherableBodyResource GetComp(Pawn animal)
    {
        return animal.TryGetComp<CompMilkableHuman>();
    }

    protected override IEnumerable<HumanCompHasGatherableBodyResource> GetComps(Pawn pawn)
    {
        var comps = pawn.AllComps;
        foreach (var comp in comps)
        {
            if (comp is CompMilkableHuman human)
            {
                yield return human;
            }
        }
    }
}