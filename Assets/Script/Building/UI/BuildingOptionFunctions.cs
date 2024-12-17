using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingOptionFunctions : MonoBehaviour
{
    private BuildingUIManager buildingUIManager;
    [SerializeField]private BuildingManager buildingManager; 
    void Start(){
        buildingUIManager=GetComponent<BuildingUIManager>();
    }
    public void StoneFarmIsChosen(){
        //this will be called by UI button directly.
        buildingUIManager.BuildingCostInit(buildingManager.BuildingChosen("StoneFarm"));       
    }
    public void GrainFarmIsChosen(){
        //this will be called by UI button directly.
        buildingUIManager.BuildingCostInit(buildingManager.BuildingChosen("GrainFarm"));       
    }
    public void WoodFarmIsChosen(){
        //this will be called by UI button directly.
        buildingUIManager.BuildingCostInit(buildingManager.BuildingChosen("WoodFarm"));       
    }
    public void MageIsChosen(){
        //this will be called by UI button directly.
        buildingUIManager.BuildingCostInit(buildingManager.BuildingChosen("MageBarracks"));      
    }
    public void ArcherIsChosen(){
        buildingUIManager.BuildingCostInit(buildingManager.BuildingChosen("ArcherBarracks"));      
    }
    public void InfantryIsChosen(){
       buildingUIManager.BuildingCostInit(buildingManager.BuildingChosen("InfantryBarracks"));
    }
     public void CavalryIsChosen(){
        buildingUIManager.BuildingCostInit(buildingManager.BuildingChosen("CavalryBarracks"));
    }
   
}
