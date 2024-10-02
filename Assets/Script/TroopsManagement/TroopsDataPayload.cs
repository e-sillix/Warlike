using UnityEngine;

[System.Serializable]
public class TroopsDataPayload
{
    //training cost
    public int woodTrainingCost;
    public int grainTrainingCost;
    public int stoneTrainingCost;
    public int timeTrainingCost;    
    public int TrainingTime;

    //upgrading cost
    // public int upgradingTime;
    // public int woodUpgradingCost;
    // public int grainUpgradingCost;
    // public int stoneUpgradingCost;
    // public int timeUpgradingCost;    

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
        woodTrainingCost = wood;
        grainTrainingCost = grain;
        stoneTrainingCost = stone;        
        TrainingTime= time;
        attackRange= Range;
        damage= TheDamage; 
        health= TheHealth;
        marchSpeed= Speed;
        armor= TheArmor;
    }
}