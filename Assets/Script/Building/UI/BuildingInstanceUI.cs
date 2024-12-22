using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingInstanceUI : MonoBehaviour
{
    [SerializeField] private GameObject BuildingInfoUIPanel;
    private GameObject Target;

    private string nameOfBuilding;
    int capacity,level,currentResourceAmount,rateOfProduction;
    [SerializeField]private TextMeshProUGUI BuildingName,Stats; 

    
    public void InfoIsClicked(GameObject target){
        //called by buildingInstance linked function info
        Target=target;
        BuildingInfoUIPanel.SetActive(true);
        GetAllTheStats();
        DisplayInfoUI();
    }
    void GetAllTheStats(){
        if(Target.GetComponent<Farm>()){
            Farm farm=Target.GetComponent<Farm>();
            nameOfBuilding=farm.resourceType.ToString();
            capacity=farm.capacity;
            currentResourceAmount=farm.resourceAmount;
            level=farm.level;
            rateOfProduction=farm.rateOfProduction;
        }
        // else if{
      // }
    }
    void DisplayInfoUI(){
        BuildingName.text = nameOfBuilding ;  
        Stats.text= "Level: "+ level +", Capacity: "+capacity + ", Rate Of Production: "+rateOfProduction
         +"Resource, : "+currentResourceAmount+"/"+capacity;
    }
}
