using System;
using Verse;

namespace Milk
{
	// Token: 0x02000002 RID: 2
	public class CompMilkableHuman : HumanCompHasGatherableBodyResource
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		protected override float GatherResourcesIntervalDays
		{
			get
			{
				return this.Props.milkIntervalDays;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000205D File Offset: 0x0000025D
		protected override float ResourceAmount
		{
			get
			{
				return this.Props.milkAmount;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000206A File Offset: 0x0000026A
		protected override ThingDef ResourceDef
		{
			get
			{
				return this.Props.milkDef;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002077 File Offset: 0x00000277
		protected override string SaveKey
		{
			get
			{
				return "milkFullness";
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000207E File Offset: 0x0000027E
		public CompProperties_MilkableHuman Props
		{
			get
			{
				return (CompProperties_MilkableHuman)this.props;
			}
		}

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
				Pawn pawn = this.parent as Pawn;
				if ((pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("HumanPregnancy"), false) && pawn.health.hediffSet.GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamedSilentFail("HumanPregnancy"), false).Visible && !pawn.health.hediffSet.HasHediff(HediffDef.Named("Lactating_Natural"), false)) || (pawn.health.hediffSet.HasHediff(HediffDef.Named("Pregnant"), false) && pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("Pregnant"), false).Visible && !pawn.health.hediffSet.HasHediff(HediffDef.Named("Lactating_Natural"), false)) || (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("RJW_pregnancy"), false) && pawn.health.hediffSet.GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamedSilentFail("RJW_pregnancy"), false).Visible && !pawn.health.hediffSet.HasHediff(HediffDef.Named("Lactating_Natural"), false)) || (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("RJW_pregnancy_beast"), false) && pawn.health.hediffSet.GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamedSilentFail("RJW_pregnancy_beast"), false).Visible && !pawn.health.hediffSet.HasHediff(HediffDef.Named("Lactating_Natural"), false)) || (pawn.health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamedSilentFail("GR_MuffaloMammaries"), false) && !pawn.health.hediffSet.HasHediff(HediffDef.Named("Lactating_Natural"), false)))
				{
					pawn.health.AddHediff(HediffDef.Named("Lactating_Natural"), null, null, null);
				}
				return (!this.Props.milkFemaleOnly || pawn == null || pawn.gender == Gender.Female) && (pawn == null || pawn.ageTracker.CurLifeStage.reproductive) && (pawn.health.hediffSet.HasHediff(HediffDef.Named("Lactating_Drug"), false) || pawn.health.hediffSet.HasHediff(HediffDef.Named("Lactating_Permanent"), false) || pawn.health.hediffSet.HasHediff(HediffDef.Named("Lactating_Natural"), false)) && (pawn == null || pawn.RaceProps.Humanlike);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000231A File Offset: 0x0000051A
		public override string CompInspectStringExtra()
		{
			if (!this.Active)
			{
				return null;
			}
			return Translator.Translate("MilkFullness") + ": " + base.Fullness.ToStringPercent();
		}
	}
}
