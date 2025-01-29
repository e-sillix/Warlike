using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineUI : MonoBehaviour
{
   [SerializeField] private GameObject SelectedUIGO,MarchOnUIGO;
  
    private TroopsExpeditionManager troopsExpeditionManager;
    private InfoUIManager infoUIManager;
    

    public void MineSelected(TroopsExpeditionManager TroopsExpeditionManager,InfoUIManager 
    InfoUIManager){
        //triggered when Mine is clicked.
        Debug.Log("Mine selected.");
        troopsExpeditionManager=TroopsExpeditionManager;
        infoUIManager=InfoUIManager;
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
        TheMine mine = gameObject.GetComponent<TheMine>();
        infoUIManager.MineInfoClicked(mine.level,mine.mineType.ToString(),mine.currentResource);
    }
}
