using UnityEngine;

public class BuildingStatsManager : MonoBehaviour
{
    [SerializeField] private BuildingData woodFarmData; // Drag the BuildingData ScriptableObject here
    [SerializeField] private BuildingData grainFarmData; // Drag the BuildingData ScriptableObject here
    [SerializeField] private BuildingData stoneFarmData; // Drag the BuildingData ScriptableObject here
    [SerializeField] private BuildingData ArcherBarrackData,MageBarrackData,
    CavalryBarrackData,InfantryBarrackData; // Drag the BuildingData ScriptableObject here

   
    public BuildingCost GetBuildingStats(string buildingName, int levelNumber){
         // Ensure the levelNumber is valid (between 1 and 30)
    if (levelNumber < 1 || levelNumber > 30)
    {
        Debug.LogError("Level must be between 1 and 30");
        return null;
    }

    BuildingData buildingData = null;

    // Find the correct building based on its name
    if (buildingName == "WoodFarm" || buildingName=="Wood")
    {
        buildingData = woodFarmData; // Reference to the ScriptableObject containing Wood Farm data
    }
    else if (buildingName == "GrainFarm"|| buildingName=="Grain")
    {
        buildingData = grainFarmData; // Reference to the ScriptableObject containing Barracks data
    }
    else if (buildingName == "StoneFarm"|| buildingName=="Stone")
    {
        buildingData = stoneFarmData; // Reference to the ScriptableObject containing Barracks data
    }
    else if (buildingName == "MageBarracks")
    {
        buildingData = MageBarrackData; // Reference to the ScriptableObject containing Barracks data
    }
    else if (buildingName == "CavalryBarracks")
    {
        buildingData = CavalryBarrackData; // Reference to the ScriptableObject containing Barracks data
    }
    else if (buildingName == "InfantryBarracks")
    {
        buildingData = InfantryBarrackData; // Reference to the ScriptableObject containing Barracks data
    }
    else if (buildingName == "ArcherBarracks")
    {
        buildingData = ArcherBarrackData; // Reference to the ScriptableObject containing Barracks data
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