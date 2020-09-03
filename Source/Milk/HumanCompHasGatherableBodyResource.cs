using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace Milk
{
	// Token: 0x02000007 RID: 7
	public abstract class HumanCompHasGatherableBodyResource : ThingComp
	{
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
		public float Fullness
		{
			get
			{
				return this.fullness;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002414 File Offset: 0x00000614
		protected virtual bool Active
		{
			get
			{
				return this.parent.Faction != null;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002424 File Offset: 0x00000624
		public bool ActiveAndFull
		{
			get
			{
				return this.Active && this.fullness >= 1f;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002440 File Offset: 0x00000640
		public override void PostExposeData()
		{
			base.PostExposeData();
			Scribe_Values.Look<float>(ref this.fullness, this.SaveKey, 0f, false);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002460 File Offset: 0x00000660
		public override void CompTick()
		{
			if (this.Active)
			{
				Pawn pawn = this.parent as Pawn;
				if (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("HugeBreasts"), false) || pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("BionicBreasts"), false) || pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("SlimeBreasts"), false) || pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("GR_MuffaloMammaries"), false))
				{
					this.BreastSizeDays = 3f;
				}
				else if (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("Breasts"), false) || pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("HydraulicBreasts"), false) || (pawn.gender == Gender.Female && DefDatabase<HediffDef>.GetNamedSilentFail("Breasts") == null))
				{
					this.BreastSizeDays = 1.2f;
				}
				else if (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("SmallBreasts"), false))
				{
					this.BreastSizeDays = 1f;
				}
				else if (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("LargeBreasts"), false) || pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("ArchotechBreasts"), false))
				{
					this.BreastSizeDays = 1.5f;
				}
				else if (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("FlatBreasts"), false) || pawn.gender == Gender.Male)
				{
					this.BreastSizeDays = 0.85f;
				}
				float num = 1f / (this.GatherResourcesIntervalDays / this.BreastSizeDays * 60000f);
				if (pawn != null)
				{
					num *= PawnUtility.BodyResourceGrowthSpeed(pawn);
				}
				this.fullness += num;
				if (this.fullness > 1f)
				{
					this.fullness = 1f;
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002658 File Offset: 0x00000858
		public void Gathered(Pawn doer)
		{
			if (!this.Active)
			{
				Log.Error(doer + " gathered body resources while not Active: " + this.parent, false);
			}
			if (!Rand.Chance(doer.GetStatValue(StatDefOf.AnimalGatherYield, true)))
			{
				MoteMaker.ThrowText((doer.DrawPos + this.parent.DrawPos) / 2f, this.parent.Map, Translator.Translate("TextMote_ProductWasted"), 3.65f);
			}
			else
			{
				Pawn pawn = this.parent as Pawn;
				if (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("HugeBreasts"), false) || pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("BionicBreasts"), false) || pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("SlimeBreasts"), false) || pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("GR_MuffaloMammaries"), false))
				{
					this.BreastSize = 1.5f;
				}
				else if (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("Breasts"), false) || pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("HydraulicBreasts"), false) || (pawn.gender == Gender.Female && DefDatabase<HediffDef>.GetNamedSilentFail("Breasts") == null))
				{
					this.BreastSize = 1f;
				}
				else if (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("SmallBreasts"), false))
				{
					this.BreastSize = 0.75f;
				}
				else if (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("LargeBreasts"), false) || pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("ArchotechBreasts"), false))
				{
					this.BreastSize = 1.25f;
				}
				else if (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("FlatBreasts"), false) || pawn.gender == Gender.Male)
				{
					this.BreastSize = 0.5f;
				}
				int i = GenMath.RoundRandom(this.ResourceAmount * this.BreastSize * this.fullness);
				while (i > 0)
				{
					int num = Mathf.Clamp(i, 1, this.ResourceDef.stackLimit);
					i -= num;
					Thing thing = ThingMaker.MakeThing(this.ResourceDef, null);
					thing.stackCount = num;
					GenPlace.TryPlaceThing(thing, doer.Position, doer.Map, ThingPlaceMode.Near, null, null);
				}
			}
			this.fullness = 0f;
		}

		// Token: 0x04000005 RID: 5
		public float BreastSize = 1f;

		// Token: 0x04000006 RID: 6
		public float BreastSizeDays = 1f;

		// Token: 0x04000007 RID: 7
		protected float fullness;
	}
}
