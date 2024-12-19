using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class BuildingUIManager : MonoBehaviour
{
    //handle all the UI interaction for resoruce spawning   and function message triggered by RSpM and control
    //by rsm   
    [SerializeField] private GameObject CheckingUpUIGameobject,BuildingUIPanel;
    [SerializeField] private GameObject ConfirmationUI;
    [SerializeField] private GameObject NotEnoughCreditsGameobject;
    [SerializeField] private GameObject NoSpaceGameobject;
    [SerializeField] private GameObject NotInsideKingdom;
    [SerializeField] private GameObject Success;
    [SerializeField] private float messageDisappearingTime=0.5f;
    // [SerializeField] private ResourceSpawnManager SpawnManager;
    [SerializeField] private TradingManager tradingManagerCS;
    [SerializeField] BuildingManager buildingManager;
    [SerializeField]private TextMeshProUGUI woodsCostUI;    
    [SerializeField]private TextMeshProUGUI grainCostUI;    
    [SerializeField]private TextMeshProUGUI stoneCostUI;    
    // private BluePrint bluePrint;
    private int woodCost;
    private int grainCost;
    private int stoneCost;
    private BuildingCost buildingCost;
    private int status;   
    [SerializeField] private GlobalUIManager globalUIManager;   
    
    public void BuildingCostInit(BuildingCost BuildingCost){
        buildingCost=BuildingCost;
        AssigningCost();
        CheckingUpUIGameobject.SetActive(true);
        BuildingUIPanel.SetActive(false);
        DisplayingDataUI();
    }
    public void BuildOptionClicked(){
        //triggered by ui build button

        //check isenough
        if(!buildingManager.IsEnoughCredit()){
            MessageForNotEnoughCredit();
            status=3;
            RevertingUI();
        }        
        else{
            //this for creating blueprint
            buildingManager.BuildStage2();

            //trigger confirmationui
            ConfirmationUI.SetActive(true);

        }

        //there will be improvement like when click outside of kingdom++++++++++++++++++ 
		
    }

    public void ConfirmationClicked(){
        //this will be triggered by tick 
            status=buildingManager.ConfirmingBuilding();

            if(status==0){
                Debug.Log("success");
                RevertingUI();
                nullingCost();
                globalUIManager.RefreshPermission();
            }
            else {
                if(status==1){
                Debug.Log("Not inside kingdom");
                RevertingUI();
                nullingCost();
                //not inside kingdom
            }
            else if(status==2){

                // no refreshing ui here.
                Debug.Log("Not Enough space");
                MessageForNotEnoughSpace();
            }
            }
        //check the status and display a message depends on status
    }
    public void CancelIsClicked(){
        //when clicked cancellation in confirmation
        buildingManager.CancelationOfBuilding();
        nullingCost();
    }
    private void AssigningCost(){
        woodCost=buildingCost.woodCost;
        grainCost=buildingCost.grainCost;
        stoneCost=buildingCost.stoneCost; 
    }
    private void nullingCost(){
        //this will be triggered automatically when the building is done or canceled
        buildingCost=null;
        woodCost=0;
        grainCost=0;
        stoneCost=0; 
    }
    private void DisplayingDataUI(){
        //this will display costdata of that building. 
        woodsCostUI.text = "W:" + woodCost.ToString() ;      
        stoneCostUI.text = "G: " + stoneCost.ToString();       
        grainCostUI.text = "S: " + grainCost.ToString() ;      
    }

    public void RevertingUI(){
        ConfirmationUI.SetActive(false);
        CheckingUpUIGameobject.SetActive(false);
    }


//messages
    void MessageForNotEnoughCredit(){
        NotEnoughCreditsGameobject.SetActive(true);
        //create a function to turn it false after 1 sec.
        Invoke("HideNotEnoughCreditsMessage", messageDisappearingTime);
    }

// This method will deactivate the GameObject
    void HideNotEnoughCreditsMessage()
        {
            //this is getting invoked by messageFornotenoughcredit.
            NotEnoughCreditsGameobject.SetActive(false);
        }   
    public void MessageForNotEnoughSpace(){
        //this will be called by RSM
        NoSpaceGameobject.SetActive(true);
        Invoke("HideNotEnoughSpaceMessage", messageDisappearingTime);
    }
    void HideNotEnoughSpaceMessage()
        {
            NoSpaceGameobject.SetActive(false);
        } 
    public void MessageForNotInsideKingdom(){
        //this will be called by RSM
        NotInsideKingdom.SetActive(true);
        Invoke("HideMessageForNotInsideKingdom", messageDisappearingTime);
    }
    void HideMessageForNotInsideKingdom()
        {
            NotInsideKingdom.SetActive(false);
        } 
    public void MessageForSuccess(){
        //this will be called by RSM
        Success.SetActive(true);
        Invoke("HideMessageForSuccess", messageDisappearingTime);
    }
    void HideMessageForSuccess()
        {
           Success.SetActive(false);
        } 
}

    