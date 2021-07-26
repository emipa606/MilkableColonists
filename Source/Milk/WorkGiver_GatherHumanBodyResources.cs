using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;
using Verse.AI;

namespace Milk
{
    // Token: 0x0200000C RID: 12
    public abstract class WorkGiver_GatherHumanBodyResources : WorkGiver_Scanner
    {
        // Token: 0x17000012 RID: 18
        // (get) Token: 0x06000026 RID: 38
        protected abstract JobDef JobDef { get; }

        // Token: 0x17000013 RID: 19
        // (get) Token: 0x06000029 RID: 41 RVA: 0x000029A5 File Offset: 0x00000BA5
        public override PathEndMode PathEndMode => PathEndMode.Touch;

        // Token: 0x06000027 RID: 39
        protected abstract HumanCompHasGatherableBodyResource GetComp(Pawn animal);
        protected abstract IEnumerable<HumanCompHasGatherableBodyResource> GetComps(Pawn pawn);

        // Token: 0x06000028 RID: 40 RVA: 0x00002995 File Offset: 0x00000B95
        public override IEnumerable<Thing> PotentialWorkThingsGlobal(Pawn pawn)
        {
            foreach (var pawn2 in pawn.Map.mapPawns.FreeColonistsAndPrisonersSpawned)
            {
                yield return pawn2;
            }
        }

        // Token: 0x0600002A RID: 42 RVA: 0x000029A8 File Offset: 0x00000BA8
        public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            if (!(t is Pawn pawn2) || !pawn2.RaceProps.Humanlike || pawn2.Drafted || pawn2.InAggroMentalState ||
                pawn2.IsFormingCaravan())
            {
                return false;
            }

            if (pawn2 == pawn)
                return false;

            bool harvest = false;

            foreach (HumanCompHasGatherableBodyResource comp in GetComps(pawn2))
                if (comp.ActiveAndFull)
                    harvest = true;
            if (!harvest)
            {
                return false;
            }

            LocalTargetInfo target = pawn2;
            if (pawn.CanReserve(target, 1, -1, null, forced))
            {
                return true;
            }

            return false;
        }

        // Token: 0x0600002B RID: 43 RVA: 0x00002A14 File Offset: 0x00000C14
        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            return new Job(JobDef, t);
        }
    }
}