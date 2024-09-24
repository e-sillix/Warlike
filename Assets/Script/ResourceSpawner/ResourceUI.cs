using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    //handle all the UI interaction for resoruce spawning   and function message triggered by RSpM and control
    //by rsm   
    [SerializeField] private GameObject CheckingUpUIGameobject;
    [SerializeField] private GameObject BottomLeftUIGameobject;
    [SerializeField] private GameObject ConfirmationUI;
    [SerializeField] private GameObject NotEnoughCreditsGameobject;
    [SerializeField] private GameObject NoSpaceGameobject;
    [SerializeField] private float messageDisappearingTime=0.5f;
    [SerializeField] private ResourceSpawnManager SpawnManager;
    [SerializeField] private TradingManager tradingManagerCS;
    public int woodCost;
    public int grainCost;
    public int stoneCost;
   
    public void ChoosedABuildng(){
        //triggered by build ui button
        //check if we have enough resource 
        if(tradingManagerCS.IsEnoughResource(woodCost,grainCost,stoneCost)){
            SpawnManager.TriggerBluePrint();
        }
        else{
            MessageForNotEnoughCredit();
        }
    }
    public void SpawnTheResource(){
        //this is called by UI yes confirmation
        SpawnManager.PlaceResources();

    }
    public void TheBuildingPlaced(){
        //this will be called by rsm when builing is done .
        tradingManagerCS.SpendingResources(woodCost, grainCost, stoneCost);
        RefreshUIConstruction();
        woodCost=0;
        grainCost=0;
        stoneCost=0;  
    }
     public void CancelBuilding(){
        //Triggered by ui cancel X button on confirmation
        SpawnManager.DestroyTheBlueprint();
        RefreshUIConstruction();
    }

//ui handling interactivity
    public void DisappearConfirmScreen(){
        //triggered only by RSpM or something logical,when spawning is done with no problem        
        RefreshUIConstruction();

    }
    public void RefreshUIConstruction(){
        //trigger to refresh ui to non constructive ui
        ConfirmationUI.SetActive(false);
        CheckingUpUIGameobject.SetActive(false);
        BottomLeftUIGameobject.SetActive(true);
    }
    void MessageForNotEnoughCredit(){
        NotEnoughCreditsGameobject.SetActive(true);
        RefreshUIConstruction();
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
}
