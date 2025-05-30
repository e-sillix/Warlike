using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreepUI : MonoBehaviour
{
   
    [SerializeField] private GameObject SelectedUIGO,MarchOnUIGO,SelectedRing;
    private InfoUIManager infoUIManager;
    [SerializeField]private TextMeshProUGUI LevelIndi;
  
    private TroopsExpeditionManager troopsExpeditionManager;
    // public void Dependency(TroopsExpeditionManager TroopsExpeditionManager){
    //     troopsExpeditionManager=TroopsExpeditionManager;
    // }
    public void SetCreepRing(bool t){
        SelectedRing.SetActive(t);
    }
    public void SetLevel(int l){
        LevelIndi.text=l.ToString();
    }
    public void CreepSelected(TroopsExpeditionManager TroopsExpeditionManager,InfoUIManager 
    InfoUIManager){
        //triggered when creep is clicked.
        Debug.Log("creep selected.");
        troopsExpeditionManager=TroopsExpeditionManager;
        infoUIManager=InfoUIManager;
        SelectedUIGO.SetActive(true);
        SelectedRing.SetActive(true);
    }
    public void DeSelectCreep(){
        //called by GlobalUI
        SelectedUIGO.SetActive(false);
        SelectedRing.SetActive(false);
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
        troopsExpeditionManager.CombatTargetClicked(gameObject);
    }
    public void InfoClicked(){
        TheCreep creep = GetComponent<TheCreep>();
        infoUIManager.CreepInfoClicked(creep.level, creep.barrackType.ToString(),creep.Rewards);
    }
}
