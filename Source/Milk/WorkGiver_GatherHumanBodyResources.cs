using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;
using Verse.AI;

namespace Milk;

public abstract class WorkGiver_GatherHumanBodyResources : WorkGiver_Scanner
{
    protected abstract JobDef JobDef { get; }

    public override PathEndMode PathEndMode => PathEndMode.Touch;

    protected abstract HumanCompHasGatherableBodyResource GetComp(Pawn animal);
    protected abstract IEnumerable<HumanCompHasGatherableBodyResource> GetComps(Pawn pawn);

    public override IEnumerable<Thing> PotentialWorkThingsGlobal(Pawn pawn)
    {
        foreach (var pawn2 in pawn.Map.mapPawns.FreeColonistsAndPrisonersSpawned)
        {
            yield return pawn2;
        }
    }

    public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
    {
        if (!(t is Pawn pawn2) || !pawn2.RaceProps.Humanlike || pawn2.Drafted || pawn2.InAggroMentalState ||
            pawn2.IsFormingCaravan())
        {
            return false;
        }

        if (pawn2 == pawn)
        {
            return false;
        }

        var harvest = false;

        foreach (var comp in GetComps(pawn2))
        {
            if (comp.ActiveAndFull)
            {
                harvest = true;
            }
        }

        if (!harvest)
        {
            return false;
        }

        LocalTargetInfo target = pawn2;
        return pawn.CanReserve(target, 1, -1, null, forced);
    }

    public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
    {
        return new Job(JobDef, t);
    }
}