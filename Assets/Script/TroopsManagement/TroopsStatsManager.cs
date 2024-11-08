using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsStatsManager : MonoBehaviour
{
    [SerializeField] private TroopsData InfantryData; // Drag the troopsData ScriptableObject here
    [SerializeField] private TroopsData CavalryData; // Drag the troopsData ScriptableObject here

    [SerializeField] private TroopsData ArcherData; 
    [SerializeField] private TroopsData MageData;    
    
    //Almost all data related single level at a time
    public TroopsDataPayload GetTroopsData(string troopsType, int level){
        if (level < 1 || level > 5)
        {
            Debug.LogError("Level must be between 1 and 5");
            return null;
        }

        TroopsData troopsData=null;
         // Find the correct troops based on its name
    if (troopsType == "Infantry")
    {
        troopsData = InfantryData; // Reference to the ScriptableObject containing Wood Farm data
    }
    else if (troopsType == "Cavalry")
    {
        troopsData = CavalryData; // Reference to the ScriptableObject containing Barracks data
    }
    else if (troopsType == "Archer")
    {
        troopsData = ArcherData; // Reference to the ScriptableObject containing Barracks data
    }
    else if (troopsType == "Mage")
    {
        troopsData = MageData; // Reference to the ScriptableObject containing Barracks data
    }
    else
    {
        Debug.LogError("Building not found: " + troopsType);
        return null;
    }
    // Adjust level number to index (array starts at 0, levels start at 1)
    int levelIndex = level - 1;

    // Return the resource costs for the specified level
    return new TroopsDataPayload(  //this one is in resource Spawner
        troopsData.WoodCost[levelIndex], 
        troopsData.GrainCost[levelIndex], 
        troopsData.StoneCost[levelIndex],
        troopsData.TrainingTime[levelIndex],
        troopsData.AttackRange,
        troopsData.Damage[levelIndex],
        troopsData.Health[levelIndex],     
        troopsData.MarchSpeed[levelIndex],     
        troopsData.Armor[levelIndex]     
    );
    }
    
    
    //Load 
    
    public LoadDataPayload GetTroopsLoadData(string troopsType){
        TroopsData troopsData=null;
         // Find the correct troops based on its name
    if (troopsType == "Infantry")
    {
        troopsData = InfantryData; // Reference to the ScriptableObject containing Wood Farm data
    }
    else if (troopsType == "Cavalry")
    {
        troopsData = CavalryData; // Reference to the ScriptableObject containing Barracks data
    }
    else if (troopsType == "Archer")
    {
        troopsData = ArcherData; // Reference to the ScriptableObject containing Barracks data
    }
    else if (troopsType == "Mage")
    {
        troopsData = MageData; // Reference to the ScriptableObject containing Barracks data
    }
    else
    {
        Debug.LogError("Building not found: " + troopsType);
        return null;
    }
        return new LoadDataPayload(troopsData.LoadCapacity);
    }
    

//Attack and defense related related

    public AttackStatPayload GetFightData(string troopsType){
        TroopsData troopsData=null;
         // Find the correct troops based on its name
        if (troopsType == "Infantry")
        {
            troopsData = InfantryData; // Reference to the ScriptableObject containing Wood Farm data
        }   
        else if (troopsType == "Cavalry")
        {
            troopsData = CavalryData; // Reference to the ScriptableObject containing Barracks data
        }
        else if (troopsType == "Archer")
        {
            troopsData = ArcherData; // Reference to the ScriptableObject containing Barracks data
        }
        else if (troopsType == "Mage")
        {
            troopsData = MageData; // Reference to the ScriptableObject containing Barracks data
        }
        else
        {
            Debug.LogError("Building not found: " + troopsType);
            return null;
        }
        return new AttackStatPayload(troopsData.Damage,troopsData.Health,troopsData.Armor);
    }
}
