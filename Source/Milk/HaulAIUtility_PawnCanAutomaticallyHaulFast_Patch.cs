using System;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace Milk
{
	// Token: 0x02000006 RID: 6
	[HarmonyPatch(typeof(HaulAIUtility), "PawnCanAutomaticallyHaulFast")]
	public static class HaulAIUtility_PawnCanAutomaticallyHaulFast_Patch
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000239C File Offset: 0x0000059C
		[HarmonyPostfix]
		public static void isMilk(ref Pawn p, ref Thing t, ref bool forced, ref bool __result)
		{
			LocalTargetInfo target = t;
			if (p.CanReserve(target, 1, -1, null, forced) && p.CanReach(t, PathEndMode.ClosestTouch, p.NormalMaxDanger(), false, TraverseMode.ByPawn) && !t.IsBurning() && (t.def == ThingDef.Named("Milk") || t.def == ThingDef.Named("EggChickenUnfertilized")))
			{
				__result = true;
			}
		}
	}
}
