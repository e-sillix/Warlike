
using UnityEngine;

public class BuildingManager : MonoBehaviour
{//responsible for spawning
    [SerializeField] private BuildingStatsManager statsManager;
    [SerializeField] private TradingManager tradingManager;
    // [SerializeField] private BuildingUpgrade buildingUpgrade;
    // [SerializeField] private BuildingInstanceUI buildingInstanceUI;
    [SerializeField] private ConstructionDependencyManager constructionDependencyManager;
    [SerializeField] private GameObject ParentObject;
    private ConditionalManager conditionManager;

    private GameObject SpawnedBuilding,ConstructionBuilding;
    private BuildingCost buildingCost;
    private int status,woodCost,grainCost,stoneCost;
    private GameObject buildingPrefab,buildingBlueprint,UnderConstructionBuilding;
    
    void Start(){
        conditionManager=GetComponent<ConditionalManager>();
    }
    public BuildingCost BuildingChosen(string buildingName, int level=1){
        //this will be called by uimanager 
        buildingCost=statsManager.GetBuildingStats(buildingName,level);
        AssigningCostBM();
        return buildingCost;
    }
    private void AssigningCostBM(){
        if(buildingCost==null){
            Debug.Log("null buildingcost");
            return;
        }
        woodCost=buildingCost.woodCost;
        grainCost=buildingCost.grainCost;
        stoneCost=buildingCost.stoneCost;
        buildingPrefab=buildingCost.TheOriginal;
        buildingBlueprint=buildingCost.TheBlueprint;
        UnderConstructionBuilding=buildingCost.UnderConstructionPrefab;
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
        GameObject blue=conditionManager.ReturnBlueprintObj();
        // SpawnedBuilding=Instantiate(buildingPrefab,blue.transform.position,Quaternion.identity );
        ConstructionBuilding=Instantiate(UnderConstructionBuilding,
        blue.transform.position,Quaternion.identity );


        // SpawnedBuilding=Instantiate(buildingPrefab,blue.transform.position,blue 
        // .transform.rotation);
        ConstructionBuilding.transform.SetParent(ParentObject.transform);
        ConstructionBuilding.transform.localRotation = Quaternion.identity;
        // ProvidingManager();
        constructionDependencyManager.ProvideDependency(ConstructionBuilding,buildingPrefab);
        conditionManager.DestroyTheBlueprint();
    }
    // void ProvidingManager(){       
    //     dependencyManager.ProvideDependency(SpawnedBuilding);
    // }
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
