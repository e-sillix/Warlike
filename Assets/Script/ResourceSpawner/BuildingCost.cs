[System.Serializable]
public class BuildingCost
{
    public int woodCost;
    public int grainCost;
    public int stoneCost;

    public BuildingCost(int wood, int grain, int stone)
    {
        woodCost = wood;
        grainCost = grain;
        stoneCost = stone;
    }
}