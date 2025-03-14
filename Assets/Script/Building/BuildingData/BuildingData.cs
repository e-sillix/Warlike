using UnityEngine;

[CreateAssetMenu(fileName = "NewBuildingData", menuName = "Building/Building Data")]
public class BuildingData : ScriptableObject
{
    public string buildingName;

    // Costs for upgrading from level 1 to 30
    public int[] woodUpgradeCost = new int[30];  // Upgrade costs for wood
    public int[] grainUpgradeCost = new int[30]; // Upgrade costs for grain
    public int[] stoneUpgradeCost = new int[30]; // Upgrade costs for stone
    public int[] timeCost = new int[30];

    public GameObject buildingPrefab,BuildingBlueprint,UnderConstructionPrefab;
   
}
