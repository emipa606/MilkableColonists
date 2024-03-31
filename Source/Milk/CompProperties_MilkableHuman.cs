using Verse;

namespace Milk;

public class CompProperties_MilkableHuman : CompProperties
{
    public readonly string displayString = "MilkFullness";

    public readonly float milkAmount = 1f;

    public readonly bool milkFemaleOnly = true;

    public ThingDef milkDef;

    public float milkIntervalDays;

    public CompProperties_MilkableHuman()
    {
        compClass = typeof(CompMilkableHuman);
    }
}