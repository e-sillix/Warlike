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
     private (int years, int months, int days, int hours, int minutes, int seconds) timeElapsed;


    // void Start(){
    //     troopsCountManager=FindObjectOfType<TroopsCountManager>();
    // }

    public int[] GetTroopsData(){
        //by buildingPersistenceManager
        return troopsDataLocal;
    }
    public TrainingHandler GetTrainingHandler(){
        //by buildingPersistenceManager
        return trainingHandler;
    }
    public void SettingPreviousData(int l,bool isTrainingOngoing,
float TrainingProgression,float TotalTime,int[] troopsData){
        level =l;
        GetComponent<BuildingInstance>().SetData();
        if(isTrainingOngoing){
            ContinueTraining(TrainingProgression,TotalTime,troopsData);
        }
    }
    void ContinueTraining(float TrainingProgression,float TotalTime,int[] troopsData){
        TimeElapsedManagement timeElapsedManagement=GetComponent
        <BuildingInstance>().ReturnTimeElapsedManagement();
        timeElapsed=timeElapsedManagement.CalculateTimeElapsed();
        if(timeElapsed.years>0||timeElapsed.months>0||timeElapsed.days>2){
// resourceAmount=capacity;
        troopsCountManager.LoadPreviousTroopsData();
        troopsCountManager.UpdateTroopsCount(barrackType.ToString(),troopsData);
        trainingHandler.RefreshData();
        Debug.Log("Troops added directly 1");
        }
        else{
            float timeElapsedInSeconds =timeElapsed.days * 86400 + timeElapsed.hours 
        * 3600 + timeElapsed.minutes * 60 + timeElapsed.seconds;
            if(TotalTime<timeElapsedInSeconds||TotalTime<(timeElapsedInSeconds+TrainingProgression))
            {
                Debug.Log("Total Time:"+TotalTime+" TimeElapsedInSeconds:"+timeElapsedInSeconds+
                " TrainingProgression:"+TrainingProgression);
                troopsCountManager.LoadPreviousTroopsData();
                troopsCountManager.UpdateTroopsCount(barrackType.ToString(),troopsData);
                trainingHandler.RefreshData();
            }else{
                Debug.Log("Resumed for training"+(int)(TotalTime-
                (timeElapsedInSeconds+TrainingProgression)));
                // StartTraining(troopsData,(int)(TotalTime-(timeElapsedInSeconds+TrainingProgression)));
                ResumeTraining(troopsData,(int)(timeElapsedInSeconds+TrainingProgression),
                TotalTime);
            }
        }
    }   
    void ResumeTraining(int[] troopsData,int time,float TotalTime){
        troopsDataLocal=troopsData;
        UpdateStateOfBarrack(true);
        //call function  for training
        trainingHandler.ResumeTraining(time,TotalTime);
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