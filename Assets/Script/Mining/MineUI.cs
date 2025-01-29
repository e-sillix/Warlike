using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineUI : MonoBehaviour
{
   [SerializeField] private GameObject SelectedUIGO,MarchOnUIGO;
  
    private TroopsExpeditionManager troopsExpeditionManager;
    

    public void MineSelected(TroopsExpeditionManager TroopsExpeditionManager
    ){
        //triggered when Mine is clicked.
        Debug.Log("Mine selected.");
        troopsExpeditionManager=TroopsExpeditionManager;
        SelectedUIGO.SetActive(true);
    }
    public void DeSelectMine(){
        //called by GlobalUI
        SelectedUIGO.SetActive(false);
    }
    public void PassiveSelected(){
        //when creep is being marched on.
        MarchOnUIGO.SetActive(true);
    }
    public void DeSelectMinePassive(){
        //when target get march cancelled or left attack in mid 
        MarchOnUIGO.SetActive(false);
    }
    public void MarchClicked(){
        troopsExpeditionManager.MineTargetClicked(gameObject);
    }
    public void InfoClicked(){

    }
}
