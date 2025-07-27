using Verse;

namespace Milk;

public class CompMilkableHuman : HumanCompHasGatherableBodyResource
{
    protected override float GatherResourcesIntervalDays => Props.milkIntervalDays;

    protected override float ResourceAmount => Props.milkAmount;

    protected override ThingDef ResourceDef => Props.milkDef;

    protected override string SaveKey => $"milkFullness{Props.displayString}";

    private CompProperties_MilkableHuman Props => (CompProperties_MilkableHuman)props;

    protected override bool Active
    {
        get
        {
            if (!base.Active)
            {
                return false;
            }

            var pawn = parent as Pawn;
            if (pawn == null ||
                pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("Lactating_Natural")) ||
                pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("Lactating")))
            {
                return pawn != null && (!Props.milkFemaleOnly || pawn.gender == Gender.Female) &&
                       pawn.ageTracker.CurLifeStage.reproductive &&
                       (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("Lactating_Drug")) ||
                        pawn.health.hediffSet.HasHediff(
                            DefDatabase<HediffDef>.GetNamedSilentFail("Lactating_Permanent")) ||
                        pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("Lactating")) ||
                        pawn.health.hediffSet.HasHediff(
                            DefDatabase<HediffDef>.GetNamedSilentFail("Lactating_Natural"))) &&
                       pawn.RaceProps.Humanlike;
            }

            if (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("HumanPregnancy")) &&
                pawn.health.hediffSet
                    .GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamedSilentFail("HumanPregnancy")).Visible ||
                pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("Pregnant")) &&
                pawn.health.hediffSet.GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamedSilentFail("Pregnant"))
                    .Visible ||
                pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("RJW_pregnancy")) &&
                pawn.health.hediffSet
                    .GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamedSilentFail("RJW_pregnancy")).Visible ||
                pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("RJW_pregnancy_beast")) &&
                pawn.health.hediffSet
                    .GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamedSilentFail("RJW_pregnancy_beast"))
                    .Visible ||
                pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("GR_MuffaloMammaries")))
            {
                pawn.health.AddHediff(DefDatabase<HediffDef>.GetNamedSilentFail("Lactating_Natural"));
            }


            return (!Props.milkFemaleOnly || pawn.gender == Gender.Female) &&
                   pawn.ageTracker.CurLifeStage.reproductive &&
                   (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("Lactating_Drug")) ||
                    pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("Lactating_Permanent")) ||
                    pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("Lactating")) ||
                    pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("Lactating_Natural"))) &&
                   pawn.RaceProps.Humanlike;
        }
    }

    public override string CompInspectStringExtra()
    {
        if (!Active)
        {
            return null;
        }

        return Props.displayString.Translate() + ": " + Fullness.ToStringPercent();
    }
}