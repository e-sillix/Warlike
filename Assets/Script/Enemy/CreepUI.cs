using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepUI : MonoBehaviour
{
    // public GameObject SelectedVisual,ToMarchVisual;
    // public void CreepSelected()
    // {
    //     Debug.Log("Creep selected");
    //     SelectedVisual.SetActive(true);
    // }
    // public void CreepDeselected()
    // {
    //     SelectedVisual.SetActive(false);
    // }
    // public void CreepIsMarchedTowards()
    // {
    //     Debug.Log("Creep Is Being marched towards");
    //     SelectedVisual.SetActive(false);
    //     ToMarchVisual.SetActive(true);
    // }
    // public void ArmyReached()
    // {
    //     Debug.Log("Army reached");
    //     ToMarchVisual.SetActive(false);
    // }

    [SerializeField] private GameObject SelectedUIGO,MarchOnUIGO;
  
    private TroopsExpeditionManager troopsExpeditionManager;
    // public void Dependency(TroopsExpeditionManager TroopsExpeditionManager){
    //     troopsExpeditionManager=TroopsExpeditionManager;
    // }

    public void CreepSelected(TroopsExpeditionManager TroopsExpeditionManager
    ){
        //triggered when creep is clicked.
        Debug.Log("creep selected.");
        troopsExpeditionManager=TroopsExpeditionManager;
        SelectedUIGO.SetActive(true);
    }
    public void DeSelectCreep(){
        //called by GlobalUI
        SelectedUIGO.SetActive(false);
    }
    public void PassiveSelected(){
        //when creep is being marched on.
        MarchOnUIGO.SetActive(true);
    }
    public void DeSelectCreepPassive(){
        //when target get march cancelled or left attack in mid 
        MarchOnUIGO.SetActive(false);
    }
    public void MarchClicked(){
        // troopsExpeditionManager.PotentialTargetForMarchClicked(gameObject);
        troopsExpeditionManager.CreepTargetClicked(gameObject);
    }
    public void InfoClicked(){

    }
}
