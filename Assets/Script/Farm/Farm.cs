using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Video;

public class Farm : MonoBehaviour
{
    public ResourceType resourceType; //for choosing the resources to produce
    [SerializeField] private CurrencyManager currencyManager;

    [SerializeField] private int leastAmountForConsuming;
    private bool isEnough=false;
    public bool triggerConsumingAnimation=false;
    private int resourceAmount=0;
    private float timer = 0f;
    public float interval = 1f;

    private void Start(){
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
        if (timer >= interval)
        {
            // Increment currency by 1
            UpdateresourceAmount(1);

            // Reset the timer
            timer = 0f;
        }

        //check if resources are enough to collect .
        if(resourceAmount>=leastAmountForConsuming){
            isEnough=true;
        }
        else{
            isEnough=false;
        }
    }
    private void UpdateresourceAmount(int amount){
        //this continues update farm resourceAmount count.
        resourceAmount += amount;
    }


    private void TriggerCollectionOfresourceAmount(){
        //calling function in currency manager to call for transfering all resourceAmount
        currencyManager.CollectingAllresourceAmount(resourceType); 
        //trigger whole resourceAmount prefabs animations of consuming.
        isEnough=false;
    }

    public int ReturnResourceAmount(){
        int collected=resourceAmount;
        resourceAmount=0;

        //triggers animations for consuming and it is being accessed by farmanimator update
        triggerConsumingAnimation=true;

        return collected;
    }
    

 
    public void OnClickIcon(){
    //click on image icon will triggers this.    
    //transfer currency and reset it to zero.
    if(isEnough){   
    TriggerCollectionOfresourceAmount();
    }
   
    }

public bool returnIsEnough(){
        //triggers animations of icon back
        return isEnough;
    }

}

