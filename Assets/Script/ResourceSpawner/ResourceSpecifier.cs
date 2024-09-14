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
    [SerializeField] private GameObject farmPrefab;           // The actual farm prefab
    [SerializeField] private GameObject farmBluePrintPrefab;  // The blueprint prefab to show while positioning
    [SerializeField] private GameObject BarrackPrefab;           // The actual barrack prefab
    [SerializeField] private GameObject BarrackBluePrintPrefab;  // The blueprint prefab to show while positioning
 
    
   void Start(){
        resourceSpawnManager=ResourceSpawnManagerGO.GetComponent<ResourceSpawnManager>();
        resourceStatsManager=ResourceStatsManagerGO.GetComponent<ResourceStatsManager>();       
   }
   //0. option of building and setting which building is chosen
    public void BarrackIsChoosen(){
        //set current resource price
        //Get  and setprice---------------- 
        resourceSpawnManager.currentResourcePrice=resourceStatsManager.ReturnBarrackPrice();
        //set current resource prefab and blueprint
        resourceSpawnManager.chosenResource=BarrackPrefab;
        resourceSpawnManager.chosenBlueprint=BarrackBluePrintPrefab;
        //call spawnBlueprint with parameters

    }
    public void FarmIsChoosen(){
        //set current resource price
        //Get  and setprice---------------- 
        resourceSpawnManager.currentResourcePrice=resourceStatsManager.ReturnFarmPrice();
        //set current resource prefab and blueprint
        resourceSpawnManager.chosenResource=farmPrefab;
        resourceSpawnManager.chosenBlueprint=farmBluePrintPrefab;
        //call spawnBlueprint with parameters        
    }
}
