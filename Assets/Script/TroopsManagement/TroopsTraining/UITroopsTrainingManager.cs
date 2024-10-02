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
    private int[] troopsData = new int[5];
    private TroopsDataPayload troopsStats;
    [SerializeField]private TextMeshProUGUI CostUI;   
    private int[] trainingCost=new int[4];
    private string barrackType;
    private int barrackCapacity;

    void Update(){
         if (Input.GetMouseButtonDown(0)) // Detect left mouse button click
        {//stage 1
            // Debug.Log("1");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                // Debug.Log("2");
                clickedObject = hit.collider.GetComponent<BarrackCollider>();            
                if(clickedObject){
                    // Debug.Log("3");
                    // TroopData=
                    troopsStats=troopsTrainingManager.BarrackIsClicked(clickedObject);
                    if(troopsTrainingManager.IsBarrackOccupied()){
                        Debug.Log("occupied");
                        TriggerUIForOngoingTraining();
                    }else{
                        TriggerUIForTraining();
                    }
                }    
            }
        }
    } 

    
    private void TriggerUIForTraining(){
        //levels, cost , barracklimits

        //update barrackCapacity UI
        barrackCapacity=troopsTrainingManager.GetTroopsCapacity();
        //pass it to slider 
        troopsTrainingSlider.SetMaxBarrackCapacityForUI(barrackCapacity);

        //display total troops capacity and type ++++++
        barrackType=troopsTrainingManager.troopType;
        //display all that
        // .text= barrackType;
        // .text=barrackCapacity;


        //triggering Starting training ui
        StartingTrainingUIPanel.SetActive(true);


    }   

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

    public void TrainIsClicked(){
        // Stage 2
        //this will be triggered by ui ,condition when barrack is not occupied.
        
        troopsData=troopsTrainingSlider.ReturnTroopsData();
        //pass it TTM

        if(tradingManager.IsEnoughResource(trainingCost[0],trainingCost[1],trainingCost[2])){//w,g,s,t
            Debug.Log("starting training");

            //cut the cost

            troopsTrainingManager.StartTrainingProcess(troopsData,trainingCost[3]); //time

            //disable ui with succes message
        }
        else{
            Debug.Log("Not Enough");
        }
    }  
    private void TriggerUIForOngoingTraining(){
        //cancellation,progress,boosting,troops data 
    }  
}
