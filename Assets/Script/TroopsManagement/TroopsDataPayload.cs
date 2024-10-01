using UnityEngine;

[System.Serializable]
public class TroopsDataPayload
{
    //upgrade or initial cost
    public int woodCost;
    public int grainCost;
    public int stoneCost;
    public int timeCost;    
    public int upgradeTime;

    //stats
    //non-upgradable one's
    public int attackRange;

    //upgradable one's

    public int damage;
    public int health;
    public int marchSpeed;
    public int armor;
    

    public TroopsDataPayload(int wood, int grain, int stone,int time, int Range,int TheDamage,int TheHealth,
    int Speed,int TheArmor)
    {
        woodCost = wood;
        grainCost = grain;
        stoneCost = stone;
        upgradeTime= time;
        attackRange= Range;
        damage= TheDamage; 
        health= TheHealth;
        marchSpeed= Speed;
        armor= TheArmor;
    }
}