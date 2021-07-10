using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace Milk
{
    // Token: 0x02000008 RID: 8
    public abstract class JobDriver_GatherHumanBodyResources : JobDriver
    {
        // Token: 0x04000009 RID: 9
        protected const TargetIndex AnimalInd = TargetIndex.A;

        // Token: 0x04000008 RID: 8
        private float gatherProgress;

        // Token: 0x1700000F RID: 15
        // (get) Token: 0x06000019 RID: 25
        protected abstract float WorkTotal { get; }

        // Token: 0x0600001A RID: 26
        protected abstract HumanCompHasGatherableBodyResource GetComp(Pawn animal);

        // Token: 0x0600001B RID: 27 RVA: 0x000028F5 File Offset: 0x00000AF5
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref gatherProgress, "gatherProgress");
        }

        // Token: 0x0600001C RID: 28 RVA: 0x00002914 File Offset: 0x00000B14
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            var pawn1 = pawn;
            var target = job.GetTarget(TargetIndex.A);
            var job1 = job;
            return pawn1.Reserve(target, job1, 1, -1, null, errorOnFailed);
        }

        // Token: 0x0600001D RID: 29 RVA: 0x00002946 File Offset: 0x00000B46
        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
            var wait = new Toil();
            wait.initAction = delegate
            {
                var actor = wait.actor;
                var thing = (Pawn) job.GetTarget(TargetIndex.A).Thing;
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

                GetComp((Pawn) (Thing) job.GetTarget(TargetIndex.A)).Gathered(pawn);
                actor.jobs.EndCurrentJob(JobCondition.Succeeded);
            };
            wait.AddFinishAction(delegate
            {
                var thing = (Pawn) job.GetTarget(TargetIndex.A).Thing;
                if (thing != null && thing.CurJobDef == JobDefOf.Wait_MaintainPosture)
                {
                    thing.jobs.EndCurrentJob(JobCondition.InterruptForced);
                }
            });
            wait.FailOnDespawnedOrNull(TargetIndex.A);
            wait.FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch);
            wait.AddEndCondition(delegate
            {
                if (!GetComp((Pawn) (Thing) job.GetTarget(TargetIndex.A)).ActiveAndFull)
                {
                    return JobCondition.Incompletable;
                }

                return JobCondition.Ongoing;
            });
            wait.defaultCompleteMode = ToilCompleteMode.Never;
            wait.WithProgressBar(TargetIndex.A, () => gatherProgress / WorkTotal);
            wait.activeSkill = () => SkillDefOf.Animals;
            yield return wait;
        }
    }
}