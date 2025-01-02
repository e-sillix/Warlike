using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using TMPro;

public class UITroopsTrainingManager : MonoBehaviour
{//this one will take input from user directly and pass it to TTM

    // private TheBarrack Barrack;
    private BarrackCollider clickedObject;
    [SerializeField] private TroopsTrainingManager troopsTrainingManager;
    [SerializeField]private TroopsTrainingSlider troopsTrainingSlider;
    [SerializeField] private GameObject StartingTrainingUIPanel;
    [SerializeField] private TrainingCostManager trainingCostManager;
    [SerializeField] private TradingManager tradingManager;
    [SerializeField] private GlobalUIManager globalUIManager;
    private int[] troopsData = new int[5];
    
    [SerializeField]private TextMeshProUGUI CostUI,troopsTypeUI;   
    private int[] trainingCost=new int[4];
    private string barrackType;
    private int barrackCapacity;

//stage 1    
    public void TriggerUIForTraining(){
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
    }   

public void TriggerUIForOngoingTraining(){
        //cancellation,progress,boosting,troops data 
        Debug.Log("training is going on,cancel panel will be here");
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


