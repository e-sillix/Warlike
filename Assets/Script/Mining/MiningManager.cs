using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningManager : MonoBehaviour
{
    private Mining mining;
    public void InitiateMiningProcess(GameObject TheUnit,TheMine theMine){
        Mining mining=TheUnit.GetComponent<Mining>();
        if(!theMine.IsMineOccupied()){
            if(mining.IsMiningPossible()){                
                mining.StartMining(theMine);
                TheUnit.GetComponent<TheUnit>().isMining=true;
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
