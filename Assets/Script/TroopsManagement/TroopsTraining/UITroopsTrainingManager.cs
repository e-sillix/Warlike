using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class UITroopsTrainingManager : MonoBehaviour
{//this one will take input from user directly and pass it to TTM

    // private TheBarrack Barrack;
    private BarrackCollider clickedObject;
    [SerializeField] private TroopsTrainingManager troopsTrainingManager;
    [SerializeField]private TroopsTrainingSlider troopsTrainingSlider;
    [SerializeField] private GameObject StartingTrainingUIPanel;
    private int[] troopsData = new int[5];
    private TroopsDataPayload troopsStats;
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
        //display total troops capacity ++++++

        //and ui panel info 


        //triggering Starting training ui
        StartingTrainingUIPanel.SetActive(true);



    }   

    //this will give dynamic cost
    public void InputValueChanged(){
        //this will be triggered by all five slider on value change,this will be changed when adding input 
        //fields
        troopsData=troopsTrainingSlider.ReturnTroopsData();
        Debug.Log(troopsData[0]);

        //update ui for showing troops cost 
    }

    public void TrainIsClicked(){
        // Stage 2
        //this will be triggered by ui ,condition when barrack is not occupied.
        
        troopsData=troopsTrainingSlider.ReturnTroopsData();
        //pass it TTM

        if(enough){

        }
        else{
           // Display not enough message
        }


    }  
    private void TriggerUIForOngoingTraining(){
        //cancellation,progress,boosting,troops data 
    }  
}
