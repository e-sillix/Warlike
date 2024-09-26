using UnityEngine;

public class BuildingStatsManager : MonoBehaviour
{
    [SerializeField] private BuildingData woodFarmData; // Drag the BuildingData ScriptableObject here
    [SerializeField] private BuildingData grainFarmData; // Drag the BuildingData ScriptableObject here
    [SerializeField] private BuildingData stoneFarmData; // Drag the BuildingData ScriptableObject here
    [SerializeField] private BuildingData barrackData; // Drag the BuildingData ScriptableObject here

    // void Start()
    // {
    //     LoadBuildingStats();
    // }

    // void LoadBuildingStats()
    // {
    //     // Log upgrade costs for the wood farm
    //     for (int level = 0; level < woodFarmData.woodUpgradeCost.Length; level++)
    //     {
    //         Debug.Log($"Wood Farm Level {level + 1} W: {woodFarmData.woodUpgradeCost[level]} G: {woodFarmData.grainUpgradeCost[level]} S: {woodFarmData.stoneUpgradeCost[level]}");
    //     }
    // }

    // public int GetUpgradeCost(int level, ResourceType resourceType)
    // {
    //     if (level < 0 || level >= 30) // Ensure level is within valid range
    //     {
    //         Debug.LogError("Level must be between 1 and 30");
    //         return 0;
    //     }

    //     switch (resourceType)
    //     {
    //         case ResourceType.Wood:
    //             return woodFarmData.woodUpgradeCost[level];
    //         case ResourceType.Grain:
    //             return woodFarmData.grainUpgradeCost[level];
    //         case ResourceType.Stone:
    //             return woodFarmData.stoneUpgradeCost[level];
    //         default:
    //             return 0;
    //     }
    // }
   
    public BuildingCost GetBuildingStats(string buildingName, int levelNumber){
         // Ensure the levelNumber is valid (between 1 and 30)
    if (levelNumber < 1 || levelNumber > 30)
    {
        Debug.LogError("Level must be between 1 and 30");
        return null;
    }

    BuildingData buildingData = null;

    // Find the correct building based on its name
    if (buildingName == "WoodFarm")
    {
        buildingData = woodFarmData; // Reference to the ScriptableObject containing Wood Farm data
    }
    else if (buildingName == "GrainFarm")
    {
        buildingData = grainFarmData; // Reference to the ScriptableObject containing Barracks data
    }
    else if (buildingName == "StoneFarm")
    {
        buildingData = stoneFarmData; // Reference to the ScriptableObject containing Barracks data
    }
    else if (buildingName == "Barracks")
    {
        buildingData = barrackData; // Reference to the ScriptableObject containing Barracks data
    }
    else
    {
        Debug.LogError("Building not found: " + buildingName);
        return null;
    }

    // Adjust level number to index (array starts at 0, levels start at 1)
    int levelIndex = levelNumber - 1;

    // Return the resource costs for the specified level
    return new BuildingCost(  //this one is in resource Spawner
        buildingData.woodUpgradeCost[levelIndex], 
        buildingData.grainUpgradeCost[levelIndex], 
        buildingData.stoneUpgradeCost[levelIndex],
        buildingData.timeCost[levelIndex],
        buildingData.BuildingBlueprint,
        buildingData.buildingPrefab
    );
    }
}