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
    public float attackRange;

    //upgradable one's

    public int damage;
    public int health;
    public float marchSpeed;
    public float armor;
    

    public TroopsDataPayload(int wood, int grain, int stone,int time, float Range,
    int TheDamage,int TheHealth,float Speed,float TheArmor)
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