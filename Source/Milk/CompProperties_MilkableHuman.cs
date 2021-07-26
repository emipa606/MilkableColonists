using Verse;

namespace Milk
{
    // Token: 0x02000003 RID: 3
    public class CompProperties_MilkableHuman : CompProperties
    {
        public string displayString = "MilkFullness";

        // Token: 0x04000002 RID: 2
        public float milkAmount = 1f;

        // Token: 0x04000003 RID: 3
        public ThingDef milkDef;

        // Token: 0x04000004 RID: 4
        public bool milkFemaleOnly = true;

        // Token: 0x04000001 RID: 1
        public float milkIntervalDays;

        // Token: 0x06000009 RID: 9 RVA: 0x0000234D File Offset: 0x0000054D
        public CompProperties_MilkableHuman()
        {
            compClass = typeof(CompMilkableHuman);
        }
    }
}