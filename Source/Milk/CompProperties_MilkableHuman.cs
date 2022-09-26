using Verse;

namespace Milk;

public class CompProperties_MilkableHuman : CompProperties
{
    public string displayString = "MilkFullness";

    public float milkAmount = 1f;

    public ThingDef milkDef;

    public bool milkFemaleOnly = true;

    public float milkIntervalDays;

    public CompProperties_MilkableHuman()
    {
        compClass = typeof(CompMilkableHuman);
    }
}