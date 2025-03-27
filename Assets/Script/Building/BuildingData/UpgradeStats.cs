using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStats : MonoBehaviour
{
    //this is manager responsible for returning data.
    [SerializeField] private BuildingStatSO grainFarmData,stoneFarmData,woodFarmData,barrackData,
    baseData,laboratoryData; // Drag the BuildingData ScriptableObject here

    public UpgradeCostPayload GetBuildingUpgradeStats(string buildingName, int levelNumber){
        //level number will be 5 for level up to 6.
        Debug.Log("details of upgrading"+buildingName+levelNumber);
         // Ensure the levelNumber is valid (between 1 and 30)
    if (levelNumber < 1 || levelNumber > 30-1)
    {
        Debug.Log("Level must be between 1 and 30");
        return null;
    }

    BuildingStatSO buildingData = null;

    // Find the correct building based on its name
    if (buildingName == "Grain")
    {
        buildingData = grainFarmData; // Reference to the ScriptableObject containing Wood Farm data
    }
    else if(buildingName == "Stone"){
        buildingData = stoneFarmData; 
    }
    else if(buildingName == "Wood"){
        buildingData = woodFarmData; 
    }
    else if(buildingName =="Infantry" ||buildingName =="Archer"
    ||buildingName =="Cavalry"||buildingName =="Mage"||buildingName=="Barrack"){
        buildingData = barrackData; 
    }
    else if(buildingName == "Base") {
        buildingData = baseData;            
    }
        else if(buildingName == "Laboratory") {            
            buildingData = laboratoryData;            
    }
     else
    {
        Debug.LogError("Building not found //fill the loop and so: " + buildingName);
        return null;
    }

    // Adjust level number to index (array starts at 0, levels start at 1)
    int levelIndex = levelNumber;//because it need one level up and array start at 0.

    // Return the resource costs for the specified level
    return new UpgradeCostPayload(  //this one is in resource Spawner
        buildingData.Capacity[levelIndex],
        buildingData.Rate[levelIndex]
    );
    }

    public void SetData(GameObject building){
        //called by building instance.
        if(building.GetComponent<TheBarrack>()){
            int l=building.GetComponent<TheBarrack>().level-1;
            building.GetComponent<TheBarrack>().SetStats(barrackData.Capacity[l],
            barrackData.Rate[l]);
        }else if(building.GetComponent<Farm>()){
            int l=building.GetComponent<Farm>().level-1;
            if(building.GetComponent<Farm>().resourceType==ResourceType.Wood){
            building.GetComponent<Farm>().SetStats(woodFarmData.Capacity[l],woodFarmData.Rate[l]);
            }
            else if(building.GetComponent<Farm>().resourceType==ResourceType.Grain){
            building.GetComponent<Farm>().SetStats(grainFarmData.Capacity[l],grainFarmData.Rate[l]);
            }
            else if(building.GetComponent<Farm>().resourceType==ResourceType.Stone){
            building.GetComponent<Farm>().SetStats(stoneFarmData.Capacity[l],stoneFarmData.Rate[l]);
            }
            }
            else if(building.GetComponent<Laboratory>()){
            int l=building.GetComponent<Laboratory>().level-1;
            // building.GetComponent<Laboratory>().SetStats(
            //     laboratoryData.Capacity[l],laboratoryData.Rate[l]);
            Debug.Log("laboratory upgrade stats not implemented yet");
            }
            else if(building.GetComponent<Base>()){
            int l=building.GetComponent<Base>().level-1;
            building.GetComponent<Base>().SetStats(baseData.Capacity[l]);
            }
    }
}
