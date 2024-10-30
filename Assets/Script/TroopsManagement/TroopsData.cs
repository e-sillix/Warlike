using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTroopsData", menuName = "Troops/Troops Data")]

public class TroopsData :  ScriptableObject
{
    public string troopName;

// Training cost 
    public int[] WoodCost = new int[5];  // Upgrade costs for that troop
    public int[] GrainCost = new int[5];  // Upgrade costs for that troop
    public int[] StoneCost = new int[5];  // Upgrade costs for that troop

    public int[] LoadCapacity= new int[5];

    public int[] TrainingTime=new int[5];// ***** this will store for each level

    //upgrading will be included in future

// stats

//non-upgradable stats
    public int AttackRange;

    //upgradable stats

    public int[] Damage = new int[5];
    public int[] Health = new int[5];
    public int[] MarchSpeed = new int[5];
    public int[] Armor = new int[5];
    //+++++++ add load capacity and mining 
    

    //for future buff:
    // rate of atk,maybe some buff ,buff against other types
}
