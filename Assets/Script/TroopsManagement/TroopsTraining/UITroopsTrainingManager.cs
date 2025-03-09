
using UnityEngine;
using TMPro;

public class UITroopsTrainingManager : MonoBehaviour
{//this one will take input from user directly and pass it to TTM

    // private TheBarrack Barrack;
    [SerializeField] private TroopsTrainingManager troopsTrainingManager;
    [SerializeField]private TroopsTrainingSlider troopsTrainingSlider;
    [SerializeField] private GameObject StartingTrainingUIPanel, OngoingTrainingUIPanel;
    [SerializeField] private TrainingCostManager trainingCostManager;
    [SerializeField] private TradingManager tradingManager;
    [SerializeField] private GlobalUIManager globalUIManager;
    private int[] troopsData = new int[5];
    
    [SerializeField]private TextMeshProUGUI CostUI,troopsTypeUI;   
    private int[] trainingCost=new int[4];
    private string barrackType;
    private int barrackCapacity;

    [SerializeField] private GameObject slider1,slider2,slider3,slider4,slider5;

//stage 1    
    public void TriggerUIForTraining(int level){
        //levels, cost , barracklimits

        //update barrackCapacity UI
        barrackCapacity=troopsTrainingManager.GetTroopsCapacity();
        //pass it to slider 
        troopsTrainingSlider.SetMaxBarrackCapacityForUI(barrackCapacity);

        //display total troops capacity and type ++++++
        barrackType=troopsTrainingManager.troopType;

        troopsTypeUI.text="Train "+barrackType.ToString();
        //triggering Starting training ui
        StartingTrainingUIPanel.SetActive(true);
        ResetAllSlider();

        if (level >= 1 && level <= 5)
        {
            slider1.SetActive(true); // Only slider 1
        }
        else if (level >= 6 && level <= 10)
        {
            slider1.SetActive(true); // Only slider 1
            slider2.SetActive(true); // Only slider 1
        }
        else if(level >= 11 && level <= 15)
        {
            slider1.SetActive(true); // Only slider 1
            slider2.SetActive(true); // Only slider 1
            slider3.SetActive(true); // Only slider 1
        }
        else if(level >= 16 && level <= 20)
        {
            slider1.SetActive(true); // Only slider 1
            slider2.SetActive(true); // Only slider 1
            slider3.SetActive(true); // Only slider 1
            slider4.SetActive(true); // Only slider 1
        }
        else if(level >= 21 && level <= 25)
        {
            slider1.SetActive(true); // Only slider 1
            slider2.SetActive(true); // Only slider 1
            slider3.SetActive(true); // Only slider 1
            slider4.SetActive(true); // Only slider 1
            slider5.SetActive(true); // Only slider 1
        }
    }   
    void ResetAllSlider(){
        slider1.SetActive(false); 
        slider2.SetActive(false);
        slider3.SetActive(false);
        slider4.SetActive(false);
        slider5.SetActive(false);
    }

public void TriggerUIForOngoingTraining(){
        //cancellation,progress,boosting,troops data 
        Debug.Log("training is going on,cancel panel will be here");
        OngoingTrainingUIPanel.SetActive(true);
    }  
//stage 2 
    //this will give dynamic cost
    public void InputValueChanged(){
        //this will be triggered by all five slider on value change,this will be changed when adding input 
        //fields
        troopsData=troopsTrainingSlider.ReturnTroopsData();
        // Debug.Log(troopsData[0]);

        //update ui for showing troops cost
        trainingCost=trainingCostManager.ReturnTrainingCost(troopsData,barrackType);
        UpdateCostUI();
        
    }
    void UpdateCostUI(){
        CostUI.text="W: "+trainingCost[0]+"G: "+trainingCost[1]+"S: "+trainingCost[2]+"t: "+trainingCost[3];
    }

// Stage 3
    public void TrainIsClicked(){
        // Stage 2
        //this will be triggered by ui ,condition when barrack is not occupied.        
        troopsData=troopsTrainingSlider.ReturnTroopsData();
        //pass it TTM
        if(tradingManager.IsEnoughResource(trainingCost[0],trainingCost[1],trainingCost[2])){//w,g,s,t
            Debug.Log("starting training");
            //cut the cost
            tradingManager.SpendingResources(trainingCost[0],trainingCost[1],trainingCost[2]);
            //start
            troopsTrainingManager.StartTrainingProcess(troopsData,trainingCost[3]); //time
            //disable ui with succes message
            EndStage();
            RefreshUI();
        }
        else{
            Debug.Log("Not Enough");
        }
    }  
    public void TrainingCancellationIsChosen(){
        //this will be triggered by ui
        //cancel training
        troopsTrainingManager.CancelTraining();
        //disable ui with succes message
        EndStage();
        RefreshUI();
    }
    
//stage 4
    public void TrainingIsDone(){
        //this will be triggered by manager
        Debug.Log("show a message for training Done here ");
    }

    //end stage
    public void EndStage(){
        //refresh every thing here and in global ui
        //this will be run by cancel buttons
        globalUIManager.RefreshPermission();
    }
    private void RefreshUI(){
        StartingTrainingUIPanel.SetActive(false);
       
    }
}


