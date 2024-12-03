using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsTrainingManager : MonoBehaviour
{//controlled by ui input 

    private GameObject TheBarrackGO;
    private TheBarrack theBarrack;
    [SerializeField]private TroopsStatsManager troopsStatsManager;
    public string troopType;
    private TroopsDataPayload troopsAllStats;
    private int barrackCapacity;
    private int[] troopsData=new int[5];

    public TroopsDataPayload BarrackIsClicked(BarrackCollider barrackCollider){//this is accepting script
        //by uitroopsTraining manager
        TheBarrackGO=barrackCollider.ReturnTheBarrack();
        theBarrack=TheBarrackGO.GetComponent<TheBarrack>();//assigned script
        // Debug.Log(theBarrack.barrackType);   
        troopType=theBarrack.barrackType.ToString();
        Debug.Log(troopType);
        troopsAllStats=troopsStatsManager.GetTroopsData(troopType ,1); 
        return troopsAllStats;
    }   
    public bool IsBarrackOccupied(){
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
    }
}
