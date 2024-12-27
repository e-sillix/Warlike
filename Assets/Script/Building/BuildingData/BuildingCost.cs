using UnityEngine;

[System.Serializable]
public class BuildingCost
{
    public int woodCost,grainCost, stoneCost, timeCost;
    public GameObject TheBlueprint,TheOriginal;

    public BuildingCost(int wood, int grain, int stone,int time, GameObject Blueprint,GameObject Original)
    {
        woodCost = wood;
        grainCost = grain;
        stoneCost = stone;
        timeCost= time;
        TheBlueprint=Blueprint;
        TheOriginal=Original;
    }
}