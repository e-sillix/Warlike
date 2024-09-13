using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Video;

public class Farm : MonoBehaviour
{
    [SerializeField] private CurrencyManager currencyManager;

    [SerializeField] private int leastAmountForConsuming;
    private bool isConsumed;   //for starting consuming animation.
    private bool isEnough=false;
    public bool triggerConsumingAnimation=false;
    private int woods=0;
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
            UpdateWoods(1);

            // Reset the timer
            timer = 0f;
        }

        //check if resources are enough to collect .
        if(woods>=leastAmountForConsuming){
            isEnough=true;
        }
        else{
            isEnough=false;
        }
    }
    private void UpdateWoods(int amount){
        //this continues update farm woods count.
        woods += amount;

    }


    private void TriggerCollectionOfWoods(){
        //calling function in currency manager to call for transfering all woods
        currencyManager.CollectingAllWoods(); 
        //trigger whole woods prefabs animations of consuming.
        isEnough=false;
    }

    public int ReturnWoods(){
        return woods;
    }
    public void ResetWoods(){
        woods=0;
    }

 
    public void OnClickIcon(){
    //click on image icon will triggers this.    
    //transfer currency and reset it to zero.
    if(isEnough){

   
    TriggerCollectionOfWoods();

    //triggers animations for consuming and it is being accessed by farmanimator update
    triggerConsumingAnimation=true;
    }
   
    }

public bool returnIsEnough(){
        //triggers animations of icon back
        return isEnough;
    }

}
