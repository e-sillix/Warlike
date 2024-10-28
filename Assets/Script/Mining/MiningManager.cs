using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningManager : MonoBehaviour
{
    private Mining mining;
    public void InitiateMiningProcess(GameObject theUnit,TheMine theMine){
        mining=theUnit.GetComponent<Mining>();
        mining.StartMining(theMine);
        theUnit.GetComponent<TheUnit>().isMining=true;
        Refresh();
    }
   
    void Refresh(){
        mining=null;
    }
}
