using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStatsManager : MonoBehaviour
{//Returns Stats of Resource for spawning.
    // [SerializeField]private int totalWood = 0;
    // [SerializeField]private int totalGrain = 0;
    // [SerializeField]private int totalStone = 0;


    // [SerializeField] private int farmPrice;
    // [SerializeField] private int barrackPrice;
    // Dictionary to store resource costs for each building type
    private Dictionary<string, BuildingCost> buildingCosts;

    void Start()
    {
        buildingCosts = new Dictionary<string, BuildingCost>();

       // Add costs for different buildings (name of building as key)
        buildingCosts.Add("Wood", new BuildingCost(wood: 10, grain: 15, stone: 10));
        buildingCosts.Add("Grain", new BuildingCost(wood: 10, grain: 5, stone: 10));
        buildingCosts.Add("Stone", new BuildingCost(wood: 15, grain: 5, stone: 20));
        buildingCosts.Add("Barracks", new BuildingCost(wood: 20, grain: 20, stone: 20));
        // Add more buildings here as needed
    }
    
    public BuildingCost ReturnFarmPrice(){//this name will be changed----
        string buildingName="Wood";
        return buildingCosts[buildingName];       
    }
    public BuildingCost ReturnBarrackPrice(){
        // string buildingName="Barracks";        
            return buildingCosts["Barracks"];
    }
    public BuildingCost GetBuildingCost(string buildingName)
    {
        if (buildingCosts.TryGetValue(buildingName, out BuildingCost cost))
        {
            return cost;
        }
        else
        {
            Debug.LogError("Building name not found in dictionary: " + buildingName);
            return null;
        }
    }

    
}



