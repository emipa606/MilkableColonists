using RimWorld;
using UnityEngine;
using Verse;

namespace Milk;

public abstract class HumanCompHasGatherableBodyResource : ThingComp
{
    public float BreastSize = 1f;

    public float BreastSizeDays = 1f;

    protected float fullness;

    protected abstract float GatherResourcesIntervalDays { get; }

    protected abstract float ResourceAmount { get; }

    protected abstract ThingDef ResourceDef { get; }

    protected abstract string SaveKey { get; }

    public float Fullness => fullness;

    protected virtual bool Active => parent.Faction != null;

    public bool ActiveAndFull => Active && fullness >= 1f;

    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Values.Look(ref fullness, SaveKey);
    }

    public override void CompTick()
    {
        if (!Active)
        {
            return;
        }

        var pawn = parent as Pawn;
        if (pawn != null &&
            (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("HugeBreasts")) ||
             pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("BionicBreasts")) ||
             pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("SlimeBreasts")) ||
             pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("GR_MuffaloMammaries"))))
        {
            BreastSizeDays = 3f;
        }
        else if (pawn != null &&
                 (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("Breasts")) ||
                  pawn.health.hediffSet.HasHediff(
                      DefDatabase<HediffDef>.GetNamedSilentFail("HydraulicBreasts")) ||
                  pawn.gender == Gender.Female && DefDatabase<HediffDef>.GetNamedSilentFail("Breasts") == null))
        {
            BreastSizeDays = 1.2f;
        }
        else if (pawn != null &&
                 pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("SmallBreasts")))
        {
            BreastSizeDays = 1f;
        }
        else if (pawn != null &&
                 (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("LargeBreasts")) ||
                  pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("ArchotechBreasts"))))
        {
            BreastSizeDays = 1.5f;
        }
        else if (pawn != null &&
                 (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("FlatBreasts")) ||
                  pawn.gender == Gender.Male))
        {
            BreastSizeDays = 0.85f;
        }

        var num = 1f / (GatherResourcesIntervalDays / BreastSizeDays * 60000f);
        if (pawn != null)
        {
            num *= PawnUtility.BodyResourceGrowthSpeed(pawn);
        }

        fullness += num;
        if (fullness > 1f)
        {
            fullness = 1f;
        }
    }

    public void Gathered(Pawn doer)
    {
        if (!Active)
        {
            Log.Error(doer + " gathered body resources while not Active: " + parent);
        }

        if (!Rand.Chance(doer.GetStatValue(StatDefOf.AnimalGatherYield)))
        {
            MoteMaker.ThrowText((doer.DrawPos + parent.DrawPos) / 2f, parent.Map,
                "TextMote_ProductWasted".Translate(), 3.65f);
        }
        else
        {
            var pawn = parent as Pawn;
            if (pawn != null &&
                (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("HugeBreasts")) ||
                 pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("BionicBreasts")) ||
                 pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("SlimeBreasts")) ||
                 pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("GR_MuffaloMammaries"))))
            {
                BreastSize = 1.5f;
            }
            else if (pawn != null &&
                     (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("Breasts")) ||
                      pawn.health.hediffSet.HasHediff(
                          DefDatabase<HediffDef>.GetNamedSilentFail("HydraulicBreasts")) ||
                      pawn.gender == Gender.Female && DefDatabase<HediffDef>.GetNamedSilentFail("Breasts") == null))
            {
                BreastSize = 1f;
            }
            else if (pawn != null &&
                     pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("SmallBreasts")))
            {
                BreastSize = 0.75f;
            }
            else if (pawn != null &&
                     (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("LargeBreasts")) ||
                      pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("ArchotechBreasts"))
                     ))
            {
                BreastSize = 1.25f;
            }
            else if (pawn != null &&
                     (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("FlatBreasts")) ||
                      pawn.gender == Gender.Male))
            {
                BreastSize = 0.5f;
            }

            var i = GenMath.RoundRandom(ResourceAmount * BreastSize * fullness);
            while (i > 0)
            {
                var num = Mathf.Clamp(i, 1, ResourceDef.stackLimit);
                i -= num;
                var thing = ThingMaker.MakeThing(ResourceDef);
                thing.stackCount = num;
                GenPlace.TryPlaceThing(thing, doer.Position, doer.Map, ThingPlaceMode.Near);
            }
        }

        fullness = 0f;
    }
}