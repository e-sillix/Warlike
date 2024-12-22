using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInstance : MonoBehaviour
{
    //this will be attached to every building ,interact with click.
    [SerializeField] private GameObject UIButtonPanel;
    private BuildingInstanceUI buildingInstanceUI;   

    public void assigningManager(BuildingInstanceUI BuildingInstanceUI){
        //this will be called by BuildingManager when prefab is created.
        buildingInstanceUI=BuildingInstanceUI;
    }
    
    public void BuildingClicked(){
        //this will be called by global ui ,after finding this script in parent of the collider.        
        //trigger ui button 
        UIButtonPanel.SetActive(true);
    }

    public void InfoClicked(){
        Debug.Log("Info is Clicked");
        buildingInstanceUI.InfoIsClicked(gameObject);
        // GetAllTheStats();
    }
    public void UpgradeClicked(){
        Debug.Log("Upgrade is Clicked");
    }
   
}
