using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBarrack : MonoBehaviour
{
   // Define an enum for different barrack types
    public enum BarrackType
    {
        Infantry,
        Archers,
        Cavalry,
        Mages
    }

    // Public variable to select from dropdown in the Inspector
    public BarrackType barrackType;
    public bool isTrainingOngoing=false;
    public int TrainingCappacity=50;//this will be update with levels
    public float rateOfTraining=1;

    //some buffs

    public int level=1;

    public void UpgradeBarrackLevel(int Level){
        level=Level;
        TriggerPrefabChange();
        UpdateStatsOfBarrack();
    }
    private void TriggerPrefabChange(){
        //change the look of barrack
    }
    private void UpdateStatsOfBarrack(){

    }
}
