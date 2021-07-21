using Verse;

namespace Milk
{
    // Token: 0x02000002 RID: 2
    public class CompMilkableHuman : HumanCompHasGatherableBodyResource
    {
        // Token: 0x17000001 RID: 1
        // (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        protected override float GatherResourcesIntervalDays => Props.milkIntervalDays;

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x06000002 RID: 2 RVA: 0x0000205D File Offset: 0x0000025D
        protected override float ResourceAmount => Props.milkAmount;

        // Token: 0x17000003 RID: 3
        // (get) Token: 0x06000003 RID: 3 RVA: 0x0000206A File Offset: 0x0000026A
        protected override ThingDef ResourceDef => Props.milkDef;

        // Token: 0x17000004 RID: 4
        // (get) Token: 0x06000004 RID: 4 RVA: 0x00002077 File Offset: 0x00000277
        protected override string SaveKey => "milkFullness";

        // Token: 0x17000005 RID: 5
        // (get) Token: 0x06000005 RID: 5 RVA: 0x0000207E File Offset: 0x0000027E
        public CompProperties_MilkableHuman Props => (CompProperties_MilkableHuman) props;

        // Token: 0x17000006 RID: 6
        // (get) Token: 0x06000006 RID: 6 RVA: 0x0000208C File Offset: 0x0000028C
        protected override bool Active
        {
            get
            {
                if (!base.Active)
                {
                    return false;
                }

                var pawn = parent as Pawn;
                if (
                    pawn != null &&
                    (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("HumanPregnancy")) &&
                     pawn.health.hediffSet
                         .GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamedSilentFail("HumanPregnancy")).Visible &&
                     !pawn.health.hediffSet.HasHediff(HediffDef.Named("Lactating_Natural")) ||
                     pawn.health.hediffSet.HasHediff(HediffDef.Named("Pregnant")) &&
                     pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("Pregnant")).Visible &&
                     !pawn.health.hediffSet.HasHediff(HediffDef.Named("Lactating_Natural")) ||
                     pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("RJW_pregnancy")) &&
                     pawn.health.hediffSet
                         .GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamedSilentFail("RJW_pregnancy")).Visible &&
                     !pawn.health.hediffSet.HasHediff(HediffDef.Named("Lactating_Natural")) ||
                     pawn.health.hediffSet.HasHediff(
                         DefDatabase<HediffDef>.GetNamedSilentFail("RJW_pregnancy_beast")) &&
                     pawn.health.hediffSet
                         .GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamedSilentFail("RJW_pregnancy_beast"))
                         .Visible && !pawn.health.hediffSet.HasHediff(HediffDef.Named("Lactating_Natural")) ||
                     pawn.health.hediffSet.HasHediff(
                         DefDatabase<HediffDef>.GetNamedSilentFail("GR_MuffaloMammaries")) &&
                     !pawn.health.hediffSet.HasHediff(HediffDef.Named("Lactating_Natural"))))
                {
                    pawn.health.AddHediff(HediffDef.Named("Lactating_Natural"));
                }

                return pawn != null && (!Props.milkFemaleOnly || pawn.gender == Gender.Female) &&
                       pawn.ageTracker.CurLifeStage.reproductive &&
                       (pawn.health.hediffSet.HasHediff(HediffDef.Named("Lactating_Drug")) ||
                        pawn.health.hediffSet.HasHediff(HediffDef.Named("Lactating_Permanent")) ||
                        pawn.health.hediffSet.HasHediff(HediffDef.Named("Lactating_Natural"))) &&
                       pawn.RaceProps.Humanlike;
            }
        }

        // Token: 0x06000007 RID: 7 RVA: 0x0000231A File Offset: 0x0000051A
        public override string CompInspectStringExtra()
        {
            if (!Active)
            {
                return null;
            }

            return Props.displayString.Translate() + ": " + Fullness.ToStringPercent();
        }
    }
}