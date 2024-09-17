[System.Serializable]
public class ResourceCost
{
    public int woodCost;
    public int grainCost;
    public int stoneCost;

    public ResourceCost(int wood, int grain, int stone)
    {
        woodCost = wood;
        grainCost = grain;
        stoneCost = stone;
    }

    public int GetTotalWoodCost() => woodCost;
    public int GetTotalGrainCost() => grainCost;
    public int GetTotalStoneCost() => stoneCost;
}
