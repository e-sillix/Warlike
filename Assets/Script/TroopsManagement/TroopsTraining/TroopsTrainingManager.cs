using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsTrainingManager : MonoBehaviour
{//controlled by ui input 

    // private GameObject TheBarrackGO;
    private TheBarrack theBarrack;
    [SerializeField]private TroopsStatsManager troopsStatsManager;
    [SerializeField] private UITroopsTrainingManager uITroopsTrainingManager;
    [SerializeField] private MessageManager messageManager;
    public string troopType;

    public void TrainingIsChosen(TheBarrack TheBarrack){
        theBarrack=TheBarrack;
        troopType=theBarrack.barrackType.ToString();
        if(IsBarrackOccupied()){
            uITroopsTrainingManager.TriggerUIForOngoingTraining();
        }
        else{
             uITroopsTrainingManager.TriggerUIForTraining();
        }
    }
     
    bool IsBarrackOccupied(){
        return theBarrack.isTrainingOngoing;
    }
    public int GetTroopsCapacity(){
        //this will get troops trainig Capacity of the that given barrack
        //this will be called by ui manager   
        Debug.Log(theBarrack.TrainingCappacity)  ;   
        return theBarrack.TrainingCappacity;
    }
    public void CheckCreditsForTraining(){
        //this will be call every time the input is changed by ui
        //
    }
    public void StartTrainingProcess(int[] troopsData,int time){
        //get that barrack assign it.
        theBarrack.StartTraining(troopsData,time);
        messageManager.TrainingStartedMessage();
    }
    public void CancelTraining(){
        theBarrack.CancelTraining();
        messageManager.TrainingCancelledMessage();
    }
}
