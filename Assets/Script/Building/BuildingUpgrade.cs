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
    [SerializeField] private MessageManager messageManager;
    private GameObject Target;
    private UpgradeCostPayload upgradeCost;

    private String nameOfBuilding;
    private int level,woodCost,grainCost,stoneCost,timeCost;
    private int[] upgradeData;
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
            if(UpgradeTheBuilding()){
                //cut the cost
                tradingManager.SpendingResources( woodCost,grainCost, stoneCost);
            }
        }
        else{
            Debug.Log("not enough!!!!! resources");
            messageManager.MessageForNotEnoughCredit();
        //if not ,display some message or debug.
        }

    }
    void GetTargetStats(){
         if(Target.GetComponent<Farm>()){
            Farm farm=Target.GetComponent<Farm>();
            nameOfBuilding=farm.resourceType.ToString();           
            level=farm.level;
        }
        else if(Target.GetComponent<TheBarrack>()){
            TheBarrack barrack=Target.GetComponent<TheBarrack>();
            nameOfBuilding=barrack.barrackType.ToString();  
            level=barrack.level;
        }
        else if(Target.GetComponent<Base>()){
            Base TheBase=Target.GetComponent<Base>();
            nameOfBuilding=TheBase.buildingName.ToString();
            level=TheBase.level;
        }else if(Target.GetComponent<Laboratory>()){
            Laboratory laboratory=Target.GetComponent<Laboratory>();
            nameOfBuilding=laboratory.buildingName.ToString();
            level=laboratory.level;
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
        timeCost = upgradeCost.timeCost ;
    }
    bool UpgradeTheBuilding(){
        if(!CheckBuildingUpgradeLadder()){
            return false;
        }
        if(Target.GetComponent<Farm>()){
            // Farm farm=Target.GetComponent<Farm>();                     
            // farm.UpgradeStats(level+1,upgradeCost.capacity,upgradeCost.rate);
            upgradeData=new int[] { level + 1, upgradeCost.capacity, upgradeCost.rate };
        }
         else if(Target.GetComponent<TheBarrack>()){
            // TheBarrack barrack=Target.GetComponent<TheBarrack>();
            // barrack.UpgradeStats(level+1,upgradeCost.capacity,upgradeCost.rate);
            upgradeData=new int[] { level + 1, upgradeCost.capacity, upgradeCost.rate };
        }
        else if(Target.GetComponent<Base>()){
            // Base TheBase=Target.GetComponent<Base>();
            // TheBase.UpgradeStats(level+1);
            upgradeData=new int[] { level + 1};
        }
        else if(Target.GetComponent<Laboratory>()){
            // Laboratory laboratory=Target.GetComponent<Laboratory>();
            // laboratory.UpgradeStats(level+1,upgradeCost.rate);
            upgradeData=new int[] { level + 1,upgradeCost.rate };
        }
        else{
            Debug.Log("Need to add more condition about the chosen building in BuildingUpgrade");
      }
    //   Target.GetComponent<BuildingInstance>().SetBuildingStatus(true);
      Target.GetComponent<BuildingInstance>().UpgradeIsOrdered(upgradeData,timeCost);
      return true;
    }
    bool CheckBuildingUpgradeLadder(){
        //check wat building 
         if(Target.GetComponent<Base>()){
            // Laboratory laboratory=GameObject.FindObjectOfType<Laboratory>();
            // if(laboratory){
            // if(laboratory.level==level){
                Debug.Log("Base Upgrade Is Allowed.");
                return true;
            // }
            // else{
            //     Debug.Log("Upgrade Laboratory first.");
            //     messageManager.UpgradeLaboratoryMessage();
            //     return false;
            // }
            // }
            // else{
            //     Debug.Log("No Laboratory Found.");
            //     messageManager.BuildLaboratory();
            //     return false;
            // }
           
         }
        //if target is not base,compare it to base level 
        else{
            //get base level
        Base TheBase = GameObject.FindObjectOfType<Base>();
        int baselevel=TheBase.level;
            if(level<baselevel){
                Debug.Log("Upgrade is Allowed.");
                                return true;
            }else{
                    Debug.Log("Upgrade is not Allowed.");
                    messageManager.UpgradeNotAllowed();
                    return false;
                }
            
        }
        //return true if target level is less than base.

        //if target is base ,check lab if it is same level as base ,return true 

        
    }


    public int[] DirectBuildingUpgrade(GameObject building)
{//return upgrade details
    string buildingNameInstance = "";
    int level = 0; // Initialize level variable

    if (building.TryGetComponent<Farm>(out Farm farm))
    {
        buildingNameInstance = farm.resourceType.ToString(); // "Wood", "Grain", or "Stone"
        level = farm.level;
    }
    else if (building.TryGetComponent<TheBarrack>(out TheBarrack barrack))
    {
        buildingNameInstance = "Barrack";
        level = barrack.level;
        Debug.Log("Barrack vurrent level: " + level);
    }
    else if (building.TryGetComponent<Base>(out Base baseBuilding))
    {
        buildingNameInstance = "Base";
        Debug.Log("Base current level: " + baseBuilding.level);
        level = baseBuilding.level;
    }
    else if (building.TryGetComponent<Laboratory>(out Laboratory lab))
    {
        buildingNameInstance = "Laboratory";
        level = lab.level;
    }
    else
    {
        return new int[0]; // Return an empty array instead of null
    }

    // Fetch Upgrade Details
    UpgradeCostPayload updata = GetUpgradeDetail(buildingNameInstance, level);

    // Return upgrade details based on building type
    if (buildingNameInstance == "Base")
        return new int[] { level + 1 }; // Base only has level

    if (buildingNameInstance == "Laboratory")
        return new int[] { level + 1, updata.rate }; // Laboratory has rate

    return new int[] { level + 1, updata.capacity, updata.rate }; // Default case
}

}
