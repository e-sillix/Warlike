using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private GameObject target,theArmy;
    [SerializeField] private MiningManager miningManager;
    public void PerformAction(GameObject TheArmy){//called by expedition manager indireclty by the unit
    //after completing  it's march
        theArmy=TheArmy;
        target=theArmy.GetComponent<TheUnit>().target;
        Debug.Log("will perform on this "+TheArmy.GetComponent<TheUnit>().ArmyId);  
        Debug.Log(target);  
        AnalyseAction();
    }
    void AnalyseAction(){
        TheMine theMine=target.GetComponentInParent<TheMine>();
        TheCreep theCreep=target.GetComponentInParent<TheCreep>();
            Debug.Log("12");
        if(theMine!=null){
            Debug.Log("Mining");
            InitiateMining(theMine);
        }
        if(theCreep!=null){
            Debug.Log("Attack");
        }
    }
    void InitiateMining(TheMine theMine){
        miningManager.InitiateMiningProcess(theArmy,theMine);
    }
}
