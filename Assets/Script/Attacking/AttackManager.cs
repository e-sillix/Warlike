using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private Attacking attacking;

    public void InitiateAttack(GameObject TheUnit,TheCreep theCreep){
        TheUnit theUnit=TheUnit.GetComponent<TheUnit>();
        attacking=theUnit.GetComponent<Attacking>();
        attacking.StartAttacking(theCreep);

        Refresh();
    }

    // public void InitiateMiningProcess(GameObject TheUnit,TheMine theMine){
    //     TheUnit theUnit=TheUnit.GetComponent<TheUnit>();
    //     if(!theMine.IsMineOccupied()){
    //         if(theUnit.totalResourceCapacity>theUnit.usedCapacity){
    //             mining=theUnit.GetComponent<Mining>();
    //             mining.StartMining(theMine);
    //             theUnit.GetComponent<TheUnit>().isMining=true;
    //             theMine.setMineStatus(true);
    //             Refresh();
    //         }
    //         else{
    //             Debug.Log("can't load ,full ");
    //         }
    //     }
    //     else{
    //         Debug.Log("already occupied,can't mine");
    //     }
    // }
   
    void Refresh(){
        attacking=null;
    }
}
