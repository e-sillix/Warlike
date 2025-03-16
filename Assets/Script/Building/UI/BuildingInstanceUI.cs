
using UnityEngine;
using TMPro;


public class BuildingInstanceUI : MonoBehaviour
{//this is a manager attached to BuildingUIManager ,for showing data
    [SerializeField] private GameObject BuildingInfoUIPanel,BuildingUpgradeUIPanel,OnGoingUpgrade;
    private GameObject Target;

    private string nameOfBuilding;
    int capacity,level,currentResourceAmount,newCapacity,newRate;
    float rateOfProduction;

    [SerializeField]private TextMeshProUGUI BuildingName,Stats; 
    [SerializeField] private TextMeshProUGUI UpgradeBuildingName,UpgradeData,UpgradeCostText;

    [SerializeField]private TextMeshProUGUI OnUpgradeBuildingName,OnLevel;

    private BuildingCost upgradeBuildingData;
    [SerializeField] private BuildingUpgrade buildingUpgrade;
    [SerializeField]private MessageManager messageManager;

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
        else if(Target.GetComponent<TheBarrack>()){
            TheBarrack theBarrack=Target.GetComponent<TheBarrack>();
            nameOfBuilding=theBarrack.barrackType.ToString();
            capacity=theBarrack.TrainingCappacity;
            level=theBarrack.level;
            rateOfProduction=theBarrack.rateOfTraining;
        }
        else if(Target.GetComponent<Base>()){
            Base baseBuilding=Target.GetComponent<Base>();
            Debug.Log("Base building is clicked");
            nameOfBuilding=baseBuilding.buildingName;
            level=baseBuilding.level;
        }
        else if(Target.GetComponent<Laboratory>()){
            Laboratory laboratory=Target.GetComponent<Laboratory>();
            Debug.Log("Laboratory building is clicked");
            nameOfBuilding=laboratory.buildingName;
            level=laboratory.level;
            rateOfProduction=laboratory.researchRate;
        }
        else{
            Debug.Log("Need to add more condition about the chosen building in BuildingInstanceUI");
      }
    }
    void DisplayBuildingInfoUI(){
        BuildingName.text = nameOfBuilding ;  
        if(Target.GetComponent<Farm>())
       { Stats.text= "Level: "+ level +", Capacity: "+capacity + ", Rate Of Production: "+rateOfProduction
         +"Resource, : "+currentResourceAmount+"/"+capacity;}
         else if(Target.GetComponent<TheBarrack>()){
            Stats.text= "Level: "+ level +",Training Capacity: "+capacity + 
            ", Rate Of Training: "+rateOfProduction;
         }
         else if(Target.GetComponent<Base>()){
             Stats.text= "Level: "+ level ;
         }
         else if(Target.GetComponent<Laboratory>()){
             Stats.text= "Level: "+ level +", Research Rate: "+rateOfProduction;
         }
    }
    public void CancelingUpgradeClicked(){
        //by instanceUI button
        Target.GetComponent<BuildingInstance>().CancelUpgrade();
    }
    void DisplayOngoingUpgradeData(){
        OnUpgradeBuildingName.text= nameOfBuilding+ " is being Upgraded.";
        OnLevel.text="Level :"+level+" TO "+(level+1);
    }

    
    //Building Upgrade related
    public void UpgradeIsClicked(GameObject target){
        //indirectly by buildingInstance

        

        Target=target;

        GetAllTheStats();//getting current info
        if(GetAllUpgradeStats()==0){
            messageManager.TriggerMaxBuildingUpgrade();
            return;
        }
        //need check the status of the building
        if(target.GetComponent<BuildingInstance>().ReturnBuildingStatus()){
            Debug.Log("Building is being upgraded");
            
            OnGoingUpgrade.SetActive(true);
            DisplayOngoingUpgradeData();
            return;
        }
        BuildingUpgradeUIPanel.SetActive(true);
        DisplayBuildingUpgradeInfo();
        GetUpgradeCost();
        

    } 
    int GetAllUpgradeStats(){
        UpgradeCostPayload upgradeDetails=buildingUpgrade.GetUpgradeDetail(nameOfBuilding,level);
        if(upgradeDetails==null){
            return 0;
        }
        newCapacity=upgradeDetails.capacity;
        newRate=upgradeDetails.rate;
        return 1;
    }
    void DisplayBuildingUpgradeInfo(){
        UpgradeBuildingName.text= nameOfBuilding ;
        if( Target.GetComponent<Farm>()){
        UpgradeData.text= "To Level: "+ (level+1) +", Capacity: "+capacity+ ">" + newCapacity
        +", Rate Of Production: "+rateOfProduction+ ">" + newRate;
        }else if(Target.GetComponent<TheBarrack>()){
             UpgradeData.text= "To Level: "+ (level+1) +", Capacity: "+capacity+ ">" + newCapacity
        +", Rate Of Training: "+rateOfProduction+ ">" + newRate;
        }
        else if(Target.GetComponent<Base>()){
            UpgradeData.text= "To Level: "+ (level+1);
        }
        else if(Target.GetComponent<Laboratory>()){
            UpgradeData.text= "To Level: "+ (level+1) +", Research Rate: "+rateOfProduction+ ">" + newRate;
        }       
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
