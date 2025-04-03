using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArmyUI : MonoBehaviour
{
    //BossArmyUI
    [SerializeField] private GameObject SelectedUIGO,MarchOnUIGO,SelectedRing;
    private InfoUIManager infoUIManager;
  
    private TroopsExpeditionManager troopsExpeditionManager;
    
    
    public void SetCreepRing(bool t){
        SelectedRing.SetActive(t);
    }
    public void BossArmySelected(TroopsExpeditionManager TroopsExpeditionManager,InfoUIManager 
    InfoUIManager){
        //triggered when creep is clicked.
        troopsExpeditionManager=TroopsExpeditionManager;
        infoUIManager=InfoUIManager;
        SelectedUIGO.SetActive(true);
        SelectedRing.SetActive(true);
    }
    public void DeSelectArmy(){
        //called by GlobalUI
        SelectedUIGO.SetActive(false);
        SelectedRing.SetActive(false);
    }
    public void PassiveSelected(){
        //when creep is being marched on.
        MarchOnUIGO.SetActive(true);
    }
    public void DeSelectArmyPassive(){
        //when target get march cancelled or left attack in mid 
        MarchOnUIGO.SetActive(false);

    }
    public void MarchClicked(){
        // troopsExpeditionManager.PotentialTargetForMarchClicked(gameObject);
        troopsExpeditionManager.CombatTargetClicked(gameObject);
    }
    public void InfoClicked(){
        // TheCreep creep = GetComponent<TheCreep>();
        // infoUIManager.CreepInfoClicked(creep.level, creep.barrackType.ToString(),creep.Rewards);
        BossArmy bossArmy = GetComponent<BossArmy>();
        infoUIManager.BossArmyInfoClicked(bossArmy.level, 
        bossArmy.barrackType.ToString(),bossArmy.Rewards);
    }
}
