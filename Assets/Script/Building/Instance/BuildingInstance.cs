using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInstance : MonoBehaviour
{
    //this will be attached to every building ,interact with click.
    [SerializeField] private GameObject UIButtonPanel;
    private BuildingUpgrade buildingUpgrade;

    public void assigningBuildingUpgrade(BuildingUpgrade buildingUpgradeManager){
        buildingUpgrade=buildingUpgradeManager;
    }
    public void BuildingClicked(){
        //this will be called by global ui ,after finding this script in parent of the collider.        
        //trigger ui button 
        UIButtonPanel.SetActive(true);
    }


    //this might be relocated somewhere.
    public void UpgradeButtonIsClicked(){
        //called by directly by ui button
        //send this GO to BuildingUpgrade(manager)
        buildingUpgrade.UpgradeTarget(gameObject);

    }

}
