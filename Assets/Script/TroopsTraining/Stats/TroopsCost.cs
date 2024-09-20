[System.Serializable]
public class TroopsCost
{
    public int woodCostTr;
    public int grainCostTr;
    public int stoneCostTr;

    public TroopsCost(int wood, int grain, int stone)
    {
        woodCostTr = wood;
        grainCostTr = grain;
        stoneCostTr = stone;
    }
}