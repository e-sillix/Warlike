using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUpgrade : MonoBehaviour
{
    //this is manager for upgrading building.
    private UpgradeStats upgradeStats;
    [SerializeField] private BuildingStatsManager buildingStatsManager;
    [SerializeField] private TradingManager tradingManager;
    private GameObject Target;
    private UpgradeCostPayload upgradeCost;

    private String nameOfBuilding;
    private int level,woodCost,grainCost,stoneCost;
    void Start(){
        upgradeStats=GetComponent<UpgradeStats>();
    }
     public UpgradeCostPayload GetUpgradeDetail(string nameOfBuilding,int level){
        //called when upgrade button is clicked for info by instance.
        upgradeCost= upgradeStats.GetBuildingUpgradeStats(nameOfBuilding,level);
        return upgradeCost;
    }
    public BuildingCost GetUpgradeCost(string buildingName, int level=1){
        return buildingStatsManager.GetBuildingStats(buildingName,level);
    }
    public void UpgradeClicked(GameObject target){
        //Upgrade Confirmed is clicked.
        Target=target;

        //get the level and buildingName
        GetTargetStats();
        //get the next building Cost
        GetCost();        
        //checking
        if(tradingManager.IsEnoughResource( woodCost, grainCost, stoneCost)){
        //if enough replace the stats
            //Upgrade
            UpgradeTheBuilding();
            //cut the cost
            tradingManager.SpendingResources( woodCost,grainCost, stoneCost);
        }
        else{
            Debug.Log("not enough!!!!! resources");
        //if not ,display some message or debug.
        }

    }
    void GetTargetStats(){
         if(Target.GetComponent<Farm>()){
            Farm farm=Target.GetComponent<Farm>();
            nameOfBuilding=farm.resourceType.ToString();           
            level=farm.level;
        }
        else{
            Debug.Log("Need to add more condition about the chosen building in BuildingUpgrade");
      }
    }
    void GetCost(){
        BuildingCost upgradeCost= buildingStatsManager.GetBuildingStats(nameOfBuilding,level+1);
        woodCost = upgradeCost.woodCost ;
        grainCost = upgradeCost.grainCost ;
        stoneCost =upgradeCost.stoneCost ;
    }
    void UpgradeTheBuilding(){
         if(Target.GetComponent<Farm>()){
            Farm farm=Target.GetComponent<Farm>();                     
            farm.UpgradeStats(level+1,upgradeCost.capacity,upgradeCost.rate);
        }
        else{
            Debug.Log("Need to add more condition about the chosen building in BuildingUpgrade");
      }
    }
}
