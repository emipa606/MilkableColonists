using RimWorld;
using UnityEngine;
using Verse;

namespace Milk
{
    // Token: 0x02000007 RID: 7
    public abstract class HumanCompHasGatherableBodyResource : ThingComp
    {
        // Token: 0x04000005 RID: 5
        public float BreastSize = 1f;

        // Token: 0x04000006 RID: 6
        public float BreastSizeDays = 1f;

        // Token: 0x04000007 RID: 7
        protected float fullness;

        // Token: 0x17000008 RID: 8
        // (get) Token: 0x0600000E RID: 14
        protected abstract float GatherResourcesIntervalDays { get; }

        // Token: 0x17000009 RID: 9
        // (get) Token: 0x0600000F RID: 15
        protected abstract float ResourceAmount { get; }

        // Token: 0x1700000A RID: 10
        // (get) Token: 0x06000010 RID: 16
        protected abstract ThingDef ResourceDef { get; }

        // Token: 0x1700000B RID: 11
        // (get) Token: 0x06000011 RID: 17
        protected abstract string SaveKey { get; }

        // Token: 0x1700000C RID: 12
        // (get) Token: 0x06000012 RID: 18 RVA: 0x0000240C File Offset: 0x0000060C
        public float Fullness => fullness;

        // Token: 0x1700000D RID: 13
        // (get) Token: 0x06000013 RID: 19 RVA: 0x00002414 File Offset: 0x00000614
        protected virtual bool Active => parent.Faction != null;

        // Token: 0x1700000E RID: 14
        // (get) Token: 0x06000014 RID: 20 RVA: 0x00002424 File Offset: 0x00000624
        public bool ActiveAndFull => Active && fullness >= 1f;

        // Token: 0x06000015 RID: 21 RVA: 0x00002440 File Offset: 0x00000640
        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref fullness, SaveKey);
        }

        // Token: 0x06000016 RID: 22 RVA: 0x00002460 File Offset: 0x00000660
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

        // Token: 0x06000017 RID: 23 RVA: 0x00002658 File Offset: 0x00000858
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
}