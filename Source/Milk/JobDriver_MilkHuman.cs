using System;
using Verse;

namespace Milk
{
	// Token: 0x0200000A RID: 10
	public class JobDriver_MilkHuman : JobDriver_GatherHumanBodyResources
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002975 File Offset: 0x00000B75
		protected override float WorkTotal
		{
			get
			{
				return 400f;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002965 File Offset: 0x00000B65
		protected override HumanCompHasGatherableBodyResource GetComp(Pawn animal)
		{
			return animal.TryGetComp<CompMilkableHuman>();
		}
	}
}
