using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingInstanceUI : MonoBehaviour
{//this is a manager attached to BuildingUIManager ,for showing data
    [SerializeField] private GameObject BuildingInfoUIPanel,BuildingUpgradeUIPanel;
    private GameObject Target;

    private string nameOfBuilding;
    int capacity,level,currentResourceAmount,rateOfProduction,newCapacity,newRate;
    [SerializeField]private TextMeshProUGUI BuildingName,Stats; 
    [SerializeField] private TextMeshProUGUI UpgradeBuildingName,UpgradeData,UpgradeCostText;

    private BuildingCost upgradeBuildingData;
    [SerializeField] private BuildingUpgrade buildingUpgrade;

    //Info related ui
    public void InfoIsClicked(GameObject target){
        //called by buildingInstance linked function info
        Target=target;
        BuildingInfoUIPanel.SetActive(true);
        GetAllTheStats();
        DisplayBuildingInfoUI();
    }
    void GetAllTheStats(){
        //this is called for info and upgrade
        if(Target.GetComponent<Farm>()){
            Farm farm=Target.GetComponent<Farm>();
            nameOfBuilding=farm.resourceType.ToString();
            capacity=farm.capacity;
            currentResourceAmount=farm.resourceAmount;
            level=farm.level;
            rateOfProduction=farm.rateOfProduction;
        }
        else{
            Debug.Log("Need to add more condition about the chosen building in BuildingInstanceUI");
      }
    }
    void DisplayBuildingInfoUI(){
        BuildingName.text = nameOfBuilding ;  
        Stats.text= "Level: "+ level +", Capacity: "+capacity + ", Rate Of Production: "+rateOfProduction
         +"Resource, : "+currentResourceAmount+"/"+capacity;
    }

    
    //Building Upgrade related
    public void UpgradeIsClicked(GameObject target){
        //indirectly by buildingInstance
        Target=target;
        BuildingUpgradeUIPanel.SetActive(true);

        GetAllTheStats();//getting current info
        GetAllUpgradeStats();
        DisplayBuildingUpgradeInfo();

        GetUpgradeCost();
    } 
    void GetAllUpgradeStats(){
        UpgradeCostPayload upgradeDetails=buildingUpgrade.GetUpgradeDetail(nameOfBuilding,level);
        newCapacity=upgradeDetails.capacity;
        newRate=upgradeDetails.rate;
    }
    void DisplayBuildingUpgradeInfo(){
        UpgradeBuildingName.text= nameOfBuilding ;
        UpgradeData.text= "To Level: "+ (level+1) +", Capacity: "+capacity+ ">" + newCapacity
        +", Rate Of Production: "+rateOfProduction+ ">" + newRate;
    }
    void GetUpgradeCost(){
        BuildingCost UpgradeCost=buildingUpgrade.GetUpgradeCost(nameOfBuilding,level+1);
        UpgradeCostText.text="Cost :"+ "Wood =" +UpgradeCost.woodCost +
        ", Grain =" +UpgradeCost.grainCost +", Stone =" +
        UpgradeCost.stoneCost +", Time =" +UpgradeCost.timeCost;
    }


    //Upgrade button is Clicked
    public void ConfirmUpgradeIsClicked(){
        buildingUpgrade.UpgradeClicked(Target);
    }
}
