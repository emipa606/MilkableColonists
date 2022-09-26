using System.Collections.Generic;
using Verse;

namespace Milk;

public class JobDriver_MilkHuman : JobDriver_GatherHumanBodyResources
{
    protected override float WorkTotal => 400f;

    protected override HumanCompHasGatherableBodyResource GetComp(Pawn animal)
    {
        return animal.TryGetComp<CompMilkableHuman>();
    }

    protected override IEnumerable<HumanCompHasGatherableBodyResource> GetComps(Pawn animal)
    {
        var comps = animal.AllComps;
        foreach (var comp in comps)
        {
            if (comp is CompMilkableHuman human)
            {
                yield return human;
            }
        }
    }
}