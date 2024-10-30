using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningManager : MonoBehaviour
{
    private Mining mining;
    public void InitiateMiningProcess(GameObject TheUnit,TheMine theMine){
        TheUnit theUnit=TheUnit.GetComponent<TheUnit>();
        if(!theMine.IsMineOccupied()){
            if(theUnit.totalResourceCapacity>theUnit.usedCapacity){
                mining=theUnit.GetComponent<Mining>();
                mining.StartMining(theMine);
                theUnit.GetComponent<TheUnit>().isMining=true;
                theMine.setMineStatus(true);
                Refresh();
            }
            else{
                Debug.Log("can't load ,full ");
            }
        }
        else{
            Debug.Log("already occupied,can't mine");
        }
    }
   
    void Refresh(){
        mining=null;
    }
}
