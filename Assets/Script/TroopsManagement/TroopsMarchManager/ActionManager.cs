using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private GameObject target,theArmy;
    [SerializeField] private MiningManager miningManager;
    [SerializeField]private AttackManager attackManager;
    public void PerformAction(GameObject TheArmy){//called by expedition manager indireclty by the unit
    //after completing  it's march
        theArmy=TheArmy;
        target=theArmy.GetComponent<TheUnit>().target;
        // Debug.Log("will perform on this "+TheArmy.GetComponent<TheUnit>().ArmyId);  
        // Debug.Log(target);  
        if(target){
        AnalyseAction();
        }
        else{
            //called when target is reached to home
            Debug.Log("we going home analysed");
            Destroy(TheArmy);
        }
    }
    void AnalyseAction(){
        TheMine theMine=target.GetComponentInParent<TheMine>();
        TheCreep theCreep=target.GetComponentInParent<TheCreep>();
        BossArmy bossArmy=target.GetComponentInParent<BossArmy>();
        Boss boss=target.GetComponentInParent<Boss>();
        TowerInstance towerInstance=target.GetComponentInParent<TowerInstance>();
            // Debug.Log("12");
        if(theMine!=null){
            Debug.Log("Mining analysed");
            // if(theMine){
            //     Debug.Log("the mine ");
            // }
            // if(theArmy){
            //     Debug.Log("the Army ");
            // }
            InitiateMining(theMine);
        }
        if(theCreep!=null || boss!=null|| bossArmy!=null||towerInstance){
            Debug.Log("Attack analysed");
            InitiateAttack(target);
        }
        
    void InitiateMining(TheMine theMine){
        miningManager.InitiateMiningProcess(theArmy,theMine);
    }
    void InitiateAttack(GameObject Target){
        attackManager.InitiateAttack(theArmy,target);
    }
    }
}
