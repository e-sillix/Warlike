using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBarrack : MonoBehaviour
{
   // Define an enum for different barrack types
    // public enum BarrackType
    // {
    //     Infantry,
    //     Archer,
    //     Cavalry,
    //     Mage
    // }

    // Public variable to select from dropdown in the Inspector
    public BarrackType barrackType;
    public bool isTrainingOngoing=false;
    public int TrainingCappacity=50;//this will be update with levels
    public float rateOfTraining=1;
    [SerializeField] private TrainingHandler trainingHandler;
    private int[] troopsDataLocal;
    private TroopsCountManager troopsCountManager;
    private TroopsTrainingManager troopsTrainingManager;

    //some buffs
    public string nameOfBuilding;
    public int level=1;

    // void Start(){
    //     troopsCountManager=FindObjectOfType<TroopsCountManager>();
    // }

    public void SettingPreviousData(int l){
        level =l;
        GetComponent<BuildingInstance>().SetData();
    }
    public void InitDependency(TroopsTrainingManager TroopsTrainingManager,
    TroopsCountManager TroopsCountManager){
        troopsTrainingManager=TroopsTrainingManager;
        troopsCountManager =TroopsCountManager;
    }
    public void TrainingIsClicked(){
        //triggered by clicking UI button train.
        troopsTrainingManager.TrainingIsChosen(this);
    }
    public void StartTraining(int[] troopsData,int time){
        troopsDataLocal=troopsData;
        UpdateStateOfBarrack(true);
        //call function  for training
        trainingHandler.StartTraining(time);
    }
    public void CancelTraining(){
        //cancel training
        trainingHandler.CancelTraining();
        UpdateStateOfBarrack(false);
    }
    private void UpdateStateOfBarrack(bool status){
        isTrainingOngoing=status;
    }
    public void TrainingEnded(){
        //this is called by training handler
        //allow the icons here
        Debug.Log("enable icon here");
        UpdateStateOfBarrack(false);
        //tell ui training ended or triiger message

        //for now add troops directly
        troopsCountManager.UpdateTroopsCount(barrackType.ToString(),troopsDataLocal);

    }
    public void UpgradeStats(int Level,int Capacity,int rate){
        level=Level;
        rateOfTraining=rate;
        TrainingCappacity=Capacity;

    }    
    public void SetStats(int Capacity,int rate){
        //called when app is reopen
        rateOfTraining=rate;
        TrainingCappacity=Capacity;
        // GetComponent<BuildingInstance>().SetData();
    }

}
public enum BarrackType
    {
        Infantry,
        Archer,
        Cavalry,
        Mage
    }