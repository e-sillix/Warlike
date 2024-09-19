using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpecifier : MonoBehaviour
{
    //Prefab attachment and take ui dynamic input and assign to public variable of RSpM
    //store all the function for all buildings 
    private ResourceSpawnManager resourceSpawnManager;
    private ResourceStatsManager resourceStatsManager;
    [SerializeField] private GameObject ResourceSpawnManagerGO;   
    [SerializeField] private GameObject ResourceStatsManagerGO;  
    
    [SerializeField] private GameObject BarrackPrefab;           // The actual barrack prefab
    [SerializeField] private GameObject BarrackBluePrintPrefab;  // The blueprint prefab to show while positioning
    [SerializeField] private GameObject woodPrefab;           // The actual prefab
    [SerializeField] private GameObject woodBluePrintPrefab;
    [SerializeField] private GameObject stonePrefab;           // The actual prefab
    [SerializeField] private GameObject stoneBluePrintPrefab;
    [SerializeField] private GameObject grainPrefab;           // The actual prefab
    [SerializeField] private GameObject grainBluePrintPrefab;
    
   void Start(){
        resourceSpawnManager=ResourceSpawnManagerGO.GetComponent<ResourceSpawnManager>();
        resourceStatsManager=ResourceStatsManagerGO.GetComponent<ResourceStatsManager>();       
   }
   //0. option of building and setting which building is chosen
    public void BarrackIsChoosen(){
        //set current resource price
        //Get  and setprice---------------- 
        BuildingCost CostData=resourceStatsManager.GetBuildingCost("Barracks");
        resourceSpawnManager.woodCost=CostData.woodCost;
        resourceSpawnManager.grainCost=CostData.grainCost;
        resourceSpawnManager.stoneCost=CostData.stoneCost;
        
        //set current resource prefab and blueprint
        resourceSpawnManager.chosenResource=BarrackPrefab;
        resourceSpawnManager.chosenBlueprint=BarrackBluePrintPrefab;
        //call spawnBlueprint with parameters

    }

    public void WoodIsChoosen()
    {
//      //set current resource price
//      //Get  and setprice---------------- 
        BuildingCost CostData=resourceStatsManager.GetBuildingCost("Wood");
        resourceSpawnManager.woodCost=CostData.woodCost;
        resourceSpawnManager.grainCost=CostData.grainCost;
        resourceSpawnManager.stoneCost=CostData.stoneCost;
        //set current resource prefab and blueprint
        resourceSpawnManager.chosenResource=woodPrefab;
        resourceSpawnManager.chosenBlueprint=woodBluePrintPrefab;
        //call spawnBlueprint with parameters        
    }
    public void GrainIsChoosen()
    {
//      //set current resource price
//      //Get  and setprice---------------- 
        BuildingCost CostData=resourceStatsManager.GetBuildingCost("Grain");
        resourceSpawnManager.woodCost=CostData.woodCost;
        resourceSpawnManager.grainCost=CostData.grainCost;
        resourceSpawnManager.stoneCost=CostData.stoneCost;
        //set current resource prefab and blueprint
        resourceSpawnManager.chosenResource=grainPrefab;
        resourceSpawnManager.chosenBlueprint=grainBluePrintPrefab;
        //call spawnBlueprint with parameters        
    }
    public void StoneIsChoosen()
    {
//      //set current resource price
//      //Get  and setprice---------------- 
        BuildingCost CostData=resourceStatsManager.GetBuildingCost("Stone");
        resourceSpawnManager.woodCost=CostData.woodCost;
        resourceSpawnManager.grainCost=CostData.grainCost;
        resourceSpawnManager.stoneCost=CostData.stoneCost;
        //set current resource prefab and blueprint
        resourceSpawnManager.chosenResource=stonePrefab;
        resourceSpawnManager.chosenBlueprint=stoneBluePrintPrefab;
        //call spawnBlueprint with parameters        
    }
}
