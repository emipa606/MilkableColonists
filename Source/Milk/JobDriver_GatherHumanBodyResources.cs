using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace Milk;

public abstract class JobDriver_GatherHumanBodyResources : JobDriver
{
    private const TargetIndex AnimalInd = TargetIndex.A;

    private float gatherProgress;

    protected abstract float WorkTotal { get; }

    protected abstract HumanCompHasGatherableBodyResource GetComp(Pawn animal);
    protected abstract IEnumerable<HumanCompHasGatherableBodyResource> GetComps(Pawn animal);

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref gatherProgress, "gatherProgress");
    }

    public override bool TryMakePreToilReservations(bool errorOnFailed)
    {
        var pawn1 = pawn;
        var target = job.GetTarget(AnimalInd);
        var job1 = job;
        return pawn1.Reserve(target, job1, 1, -1, null, errorOnFailed);
    }

    protected override IEnumerable<Toil> MakeNewToils()
    {
        this.FailOnDespawnedNullOrForbidden(AnimalInd);
        yield return Toils_Goto.GotoThing(AnimalInd, PathEndMode.Touch);
        var wait = new Toil();
        wait.initAction = delegate
        {
            var actor = wait.actor;
            var thing = (Pawn)job.GetTarget(AnimalInd).Thing;
            actor.pather.StopDead();
            PawnUtility.ForceWait(thing, 15000, null, true);
        };
        wait.tickAction = delegate
        {
            var actor = wait.actor;
            actor.skills.Learn(SkillDefOf.Animals, 0.13f);
            gatherProgress += actor.GetStatValue(StatDefOf.AnimalGatherSpeed);
            if (!(gatherProgress >= WorkTotal))
            {
                return;
            }

            var comps = GetComps((Pawn)(Thing)job.GetTarget(AnimalInd));
            foreach (var comp in comps)
            {
                comp.Gathered(pawn);
            }

            actor.jobs.EndCurrentJob(JobCondition.Succeeded);
        };
        wait.AddFinishAction(delegate
        {
            var thing = (Pawn)job.GetTarget(AnimalInd).Thing;
            if (thing != null && thing.CurJobDef == JobDefOf.Wait_MaintainPosture)
            {
                thing.jobs.EndCurrentJob(JobCondition.InterruptForced);
            }
        });
        wait.FailOnDespawnedOrNull(AnimalInd);
        wait.FailOnCannotTouch(AnimalInd, PathEndMode.Touch);
        wait.AddEndCondition(delegate
        {
            var comps = GetComps((Pawn)(Thing)job.GetTarget(AnimalInd));

            foreach (var comp in comps)
            {
                if (comp.ActiveAndFull)
                {
                    return JobCondition.Ongoing;
                }
            }

            return JobCondition.Incompletable;
        });
        wait.defaultCompleteMode = ToilCompleteMode.Never;
        wait.WithProgressBar(AnimalInd, () => gatherProgress / WorkTotal);
        wait.activeSkill = () => SkillDefOf.Animals;
        yield return wait;
    }
}