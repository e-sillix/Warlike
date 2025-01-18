using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingOptionFunctions : MonoBehaviour
{
    private BuildingUIManager buildingUIManager;
    public MessageManager messageManager;
    public int farmlimit=4,barracklimit=1,laboratorylimit=1;
    [SerializeField]private BuildingManager buildingManager; 
    void Start(){
        buildingUIManager=GetComponent<BuildingUIManager>();
    }
    public void StoneFarmIsChosen(){
        //this will be called by UI button directly.
        //checking limit       
        if(CountFarmsWithResourceType(ResourceType.Stone)){
            return;
        }
        //
        buildingUIManager.BuildingCostInit(buildingManager.BuildingChosen("StoneFarm"));       
    }
    public void GrainFarmIsChosen(){
        //this will be called by UI button directly.
         if(CountFarmsWithResourceType(ResourceType.Grain)){
            return;
        }
        buildingUIManager.BuildingCostInit(buildingManager.BuildingChosen("GrainFarm"));       
    }
    public void WoodFarmIsChosen(){
        //this will be called by UI button directly.
         if(CountFarmsWithResourceType(ResourceType.Wood)){
            return;
        }
        buildingUIManager.BuildingCostInit(buildingManager.BuildingChosen("WoodFarm"));       
    }

bool CountFarmsWithResourceType(ResourceType type)
    {
        Farm[] allFarms = GameObject.FindObjectsOfType<Farm>();
        int count = 0;

        foreach (Farm farm in allFarms)
        {
            if (farm.resourceType == type)
            {
                count++;
            }
        }
if(count>=farmlimit){
    messageManager.MessageForBuildingLimit();
return true;
}
return false;
        
    }

    public void MageIsChosen(){
        //this will be called by UI button directly.
         if(CountBarrackWithTroopType(BarrackType.Mage)){
            return;
        }
        buildingUIManager.BuildingCostInit(buildingManager.BuildingChosen("MageBarracks"));      
    }
    public void ArcherIsChosen(){
        if(CountBarrackWithTroopType(BarrackType.Archer)){
            return;
        }
        buildingUIManager.BuildingCostInit(buildingManager.BuildingChosen("ArcherBarracks"));      
    }
    public void InfantryIsChosen(){
        if(CountBarrackWithTroopType(BarrackType.Infantry)){
            return;
        }
       buildingUIManager.BuildingCostInit(buildingManager.BuildingChosen("InfantryBarracks"));
    }
     public void CavalryIsChosen(){
        if(CountBarrackWithTroopType(BarrackType.Cavalry)){
            return;
        }
        buildingUIManager.BuildingCostInit(buildingManager.BuildingChosen("CavalryBarracks"));
    }
   bool CountBarrackWithTroopType(BarrackType type)
    {
        TheBarrack[] allBarracks = GameObject.FindObjectsOfType<TheBarrack>();
        int count = 0;

        foreach (TheBarrack barrack in allBarracks)
        {
            if (barrack.barrackType == type)
            {
                count++;
            }
        }
        if(count>=barracklimit){
    messageManager.MessageForBuildingLimit();
return true;
}
return false;
}

//other building functions

public void LaboratoryIsChosen(){
        if(CountLaboratory()){
            return;
        }
        buildingUIManager.BuildingCostInit(buildingManager.BuildingChosen("Laboratory"));
    }
bool CountLaboratory()
{                       
        Laboratory[] allLaboratories = GameObject.FindObjectsOfType<Laboratory>();
        int count = 0;

        foreach (Laboratory laboratory in allLaboratories)
        {
            count++;
        }
        if(count>=laboratorylimit){
    messageManager.MessageForBuildingLimit();
return true;
}
return false;
}
}