using UnityEngine;

public class BuildingStatsManager : MonoBehaviour
{
    [SerializeField] private BuildingData woodFarmData; // Drag the BuildingData ScriptableObject here
    [SerializeField] private BuildingData grainFarmData; // Drag the BuildingData ScriptableObject here
    [SerializeField] private BuildingData stoneFarmData; // Drag the BuildingData ScriptableObject here
    [SerializeField] private BuildingData ArcherBarrackData,MageBarrackData,
    CavalryBarrackData,InfantryBarrackData; // Drag the BuildingData ScriptableObject here
    [SerializeField] private BuildingData baseData,laboratoryData; // Drag the BuildingData ScriptableObject here
   
    public BuildingCost GetBuildingStats(string buildingName, int levelNumber){
         // Ensure the levelNumber is valid (between 1 and 30)
    if (levelNumber < 1 || levelNumber > 30)
    {
        Debug.LogError("Level must be between 1 and 30");
        return null;
    }

    BuildingData buildingData = null;

   // Normalize name to remove unnecessary suffixes like "_clone1"
string normalizedBuildingName = buildingName.Replace("(Clone)", "").Trim(); 

if (normalizedBuildingName.Contains("WoodFarm") || normalizedBuildingName.Contains("Wood"))
{
    buildingData = woodFarmData;
}
else if (normalizedBuildingName.Contains("GrainFarm") || normalizedBuildingName.Contains("Grain"))
{
    buildingData = grainFarmData;
}
else if (normalizedBuildingName.Contains("StoneFarm") || normalizedBuildingName.Contains("Stone"))
{
    buildingData = stoneFarmData;
}
else if (normalizedBuildingName.Contains("MageBarracks") || normalizedBuildingName.Contains("Mage"))
{
    buildingData = MageBarrackData;
}
else if (normalizedBuildingName.Contains("CavalryBarracks") || normalizedBuildingName.Contains("Cavalry"))
{
    buildingData = CavalryBarrackData;
}
else if (normalizedBuildingName.Contains("InfantryBarracks") || normalizedBuildingName.Contains("Infantry"))
{
    buildingData = InfantryBarrackData;
}
else if (normalizedBuildingName.Contains("ArcherBarracks") || normalizedBuildingName.Contains("Archer"))
{
    buildingData = ArcherBarrackData;
}
else if (normalizedBuildingName.Contains("Base"))
{
    buildingData = baseData;
}
else if (normalizedBuildingName.Contains("Laboratory"))
{
    buildingData = laboratoryData;
}
else
{
    Debug.LogWarning("No matching building data found for: " + normalizedBuildingName);
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
        buildingData.buildingPrefab,
        buildingData.UnderConstructionPrefab
    );
    }
}