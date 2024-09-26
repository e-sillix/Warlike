using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private BuildingStatsManager statsManager;
    [SerializeField] private TradingManager tradingManager;
    private ConditionalManager conditionManager;


    private BuildingCost buildingCost;
    private int status,woodCost,grainCost,stoneCost;
    private GameObject buildingPrefab,buildingBlueprint;
    
    void Start(){
        conditionManager=GetComponent<ConditionalManager>();
    }
    public BuildingCost BuildingChosen(string buildingName, int level=1){
        //this will be called by uimanager 
        buildingCost=statsManager.GetBuildingStats(buildingName,level);
        AssigningCost();
        return buildingCost;
    }
    private void AssigningCost(){
        if(buildingCost==null){
            Debug.Log("null buildingcost");
            return;
        }
        woodCost=buildingCost.woodCost;
        grainCost=buildingCost.grainCost;
        stoneCost=buildingCost.stoneCost;
        buildingPrefab=buildingCost.TheOriginal;
        buildingBlueprint=buildingCost.TheBlueprint;
    }

    public bool IsEnoughCredit(){
        return conditionManager.CheckIsEnough(woodCost,grainCost,stoneCost);
    }

    public void BuildStage2(){
        //this will be triggered by build option ,when enough credit avaible
        conditionManager.SpawningBluePrint(buildingBlueprint);
    }

    public int ConfirmingBuilding(){
        //triggered by tick ui indirectly
        status=conditionManager.CheckAllTheCondition();
        if(status==0){
        CutTheBuildingCost();
        SpawnBuilding();
        NullingData();
        }
        
        else if(status==1){
            conditionManager.DestroyTheBlueprint();
            NullingData();
        }
        return status;

    }

    

    private void CutTheBuildingCost(){
        tradingManager.SpendingResources(woodCost, grainCost, stoneCost);
    }
    private void SpawnBuilding(){
        Instantiate(buildingPrefab,conditionManager.GetTheBlueprintPosition(), Quaternion.identity);
        conditionManager.DestroyTheBlueprint();
    }
    private void NullingData(){
        buildingCost=null;
        woodCost=0;
        grainCost=0;
        stoneCost=0;
        buildingPrefab=null;
        buildingBlueprint=null;   
    }
    public void CancelationOfBuilding(){
        conditionManager.DestroyTheBlueprint();
    }
    
}
