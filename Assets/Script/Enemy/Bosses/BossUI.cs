using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUI : MonoBehaviour
{
    [SerializeField] private GameObject SelectedUIGO,MarchOnUIGO;
    private InfoUIManager infoUIManager;
  
    private TroopsExpeditionManager troopsExpeditionManager;
    
    public void BossSelected(TroopsExpeditionManager TroopsExpeditionManager,InfoUIManager 
    InfoUIManager){
        //triggered when Boss is clicked.
        Debug.Log("Boss selected.");
        troopsExpeditionManager=TroopsExpeditionManager;
        infoUIManager=InfoUIManager;
        SelectedUIGO.SetActive(true);
    }
    public void DeSelectBoss(){
        //called by GlobalUI
        SelectedUIGO.SetActive(false);
    }
    public void PassiveSelected(){
        //when Boss is being marched on.
        MarchOnUIGO.SetActive(true);
    }
    public void DeSelectBossPassive(){
        //when target get march cancelled or left attack in mid 
        MarchOnUIGO.SetActive(false);
    }
    public void MarchClicked(){
        // troopsExpeditionManager.PotentialTargetForMarchClicked(gameObject);
        troopsExpeditionManager.CombatTargetClicked(gameObject);
    }
    public void InfoClicked(){
        Debug.Log("Info Clicked");
        // TheBoss Boss = GetComponent<TheBoss>();
        // infoUIManager.BossInfoClicked(Boss.level, Boss.barrackType.ToString(),Boss.Rewards);
    }
}
