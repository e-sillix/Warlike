using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Farm : MonoBehaviour
{
    public ResourceType resourceType; //for choosing the resources to produce
    public int level=1,rateOfProduction=1,capacity=20;
    // public string BuildingType="Farm";
    public string nameOfBuilding;
    [SerializeField] private CurrencyManager currencyManager;

    [SerializeField]private FarmAnimator farmAnimator;

    [SerializeField] private int leastAmountForConsuming;
    private bool isEnough=false;
    // public bool triggerConsumingAnimation=false;
    public int resourceAmount=0;
    private float timer = 0f,interval = 1f;
    private (int years, int months, int days, int hours, int minutes, int seconds) timeElapsed;

    public void SettingPreviousData(int l,int Amount,TimeSpan SavedTimeElapsed){        
        level =l;
        resourceAmount=Amount;
        AddTimeElapsation(SavedTimeElapsed);
        GetComponent<BuildingInstance>().SetData();
        
    }
    // public void OnApplicationQuit()
    // {
    //     GetComponent<BuildingInstance>().StoreTheProgress(resourceAmount);
    // }
    // public void SetResourceAmount(int amount){
    //     //this will be set by buildingPersistenceManager
    //     resourceAmount=amount;
    //     // timeElapsed=TimeElapsed;

    //     if(PlayerPrefs.HasKey("TimeElapsed"))
    //     {
    //     AddTimeElapsation();
    //     }
    // }
    void AddTimeElapsation(TimeSpan SavedTimeElapsed){
        // int TimeElapsed=GetComponent<BuildingInstance>().();
        // TimeElapsedManagement timeElapsedManagement=GetComponent
        // <BuildingInstance>().ReturnTimeElapsedManagement();
         timeElapsed = (
    SavedTimeElapsed.Days / 365,                 // Approximate years
    (SavedTimeElapsed.Days % 365) / 30,          // Approximate months
    SavedTimeElapsed.Days % 30,                  // Remaining days
    SavedTimeElapsed.Hours,
    SavedTimeElapsed.Minutes,
    SavedTimeElapsed.Seconds
);
        if(timeElapsed.years>0||timeElapsed.months>3){
resourceAmount=capacity;
        }
        else{
            float timeElapsedInSeconds = 
        timeElapsed.months * 2592000 + timeElapsed.days * 86400 + timeElapsed.hours 
        * 3600 + timeElapsed.minutes * 60 + timeElapsed.seconds;
        Debug.Log("TimeElapsedInSeconds:"+timeElapsedInSeconds);
        int Amount = (int)(timeElapsedInSeconds * rateOfProduction);
        if(Amount>capacity||(capacity<(Amount+resourceAmount))){
            resourceAmount=capacity;
        }
        else{
            Debug.Log("Amount added:"+Amount);
            resourceAmount+=Amount;
        }
        }
        // Debug.Log("Elapsed ResourceAmount:"+resourceAmount);
        

    }
    void Start(){
        if (currencyManager == null)
        {
            GameObject currencyManagerObject = GameObject.Find("CurrencyManager"); //parent based search
            if (currencyManagerObject != null)
            {
                currencyManager = currencyManagerObject.GetComponent<CurrencyManager>();

                if (currencyManager == null)
                {
                    Debug.LogError("CurrencyManager script component not found on the GameObject!");
                }
            }
            else
            {
                Debug.LogError("CurrencyManager GameObject not found in the scene!");
            }
        
        }
          
    }

    private void Update(){
         timer += Time.deltaTime;

        // Check if the timer has reached the interval
        if (timer >= interval && capacity>resourceAmount)
        {
            // Increment currency by 1
            UpdateresourceAmount(rateOfProduction);

            // Reset the timer
            timer = 0f;
        }

        //check if resources are enough to collect .
        if(resourceAmount>=leastAmountForConsuming){
            // isEnough=true;
            if(!isEnough){

            farmAnimator.TriggerReadyForConsuming();
            isEnough=true;
            }
        }
        // else{
        //     isEnough=false;
        //     // farmAnimator.TriggerNotEnoughForConsuming();
        // }
    }
    private void UpdateresourceAmount(int amount){
        //this continues update farm resourceAmount count.
        if(capacity<(resourceAmount+amount)){
            resourceAmount=capacity;
        }
        else{
            resourceAmount += amount;
        }
    }


    private void TriggerCollectionOfresourceAmount(){
        //calling function in currency manager to call for transfering all resourceAmount
        currencyManager.CollectingAllresourceAmount(resourceType); 
        //trigger whole resourceAmount prefabs animations of consuming.
        isEnough=false;
        GetComponent<BuildingInstance>().TriggerSaveAll();
        farmAnimator.TriggerConsuming();
    }

    public int ReturnResourceAmount(){
        int collected=resourceAmount;
        resourceAmount=0;

        //triggers animations for consuming and it is being accessed by farmanimator update
        // triggerConsumingAnimation=true;

        return collected;
    }
    

 
    public void OnClickIcon(){
    //click on image icon will triggers this.    
    //transfer currency and reset it to zero.
    // if(isEnough){   
    TriggerCollectionOfresourceAmount();
    // }
   
    }

    // public bool returnIsEnough(){
    //     //triggers animations of icon back
    //     return isEnough;
    // }
    
    public void UpgradeStats(int Level,int Capacity,int rate){
        level=Level;
        rateOfProduction=rate;
        capacity=Capacity;
    }
    public void SetStats(int Capacity,int rate){
        //called when app is reopen
        rateOfProduction=rate;
        capacity=Capacity;
    }
}

