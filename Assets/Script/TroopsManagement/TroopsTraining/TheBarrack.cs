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
    [SerializeField] private TrainingHandler trainingHandler;
    private int[] troopsDataLocal;

    //some buffs

    public int level=1;

    public void StartTraining(int[] troopsData,int time){
        troopsDataLocal=troopsData;
        UpdateStateOfBarrack(true);
        //call function  for training
        trainingHandler.StartTraining(time);
    }
    public void TrainingEnded(){
        UpdateStateOfBarrack(false);
    }
    public void UpgradeBarrackLevel(int Level){
        level=Level;
        TriggerPrefabChange();
    }
    private void TriggerPrefabChange(){
        //change the look of barrack
    }
    private void UpdateStateOfBarrack(bool status){
        isTrainingOngoing=status;
    }
}
