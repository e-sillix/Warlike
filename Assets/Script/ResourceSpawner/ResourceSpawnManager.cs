using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceSpawnManager : MonoBehaviour
{
    //dynamic respective spawning for both original and blueprint,currency management for spawning

    [SerializeField] private GameObject ResourcePriceManager;  //for price returning
    [SerializeField] private GameObject CurrencyManager;  //for current resources returning
   
    // [SerializeField] private float farmRadius = 2f;           // The radius around the farm to check for collisions
    

    private GameObject currentBlueprint;    // The current blueprint instance


    private CurrencyManager currencyManager;
    public int currentResourcePrice;
    private int currency;
    public GameObject chosenBlueprint;
    public GameObject chosenResource;
   
   void Start(){ 
    currencyManager=CurrencyManager.GetComponent<CurrencyManager>();
   }    
    //1.taking input from ui
    public void TriggerBluePrint(){   
        //this will be triggered by UI build button
       //Get current currency
        currency=currencyManager.ReturnCurrentCurrency();
            
        if (currentBlueprint == null)
            {
                //conditioning with current money
                SpawnBlueprint();                
            }       
            else{
                Destroy(currentBlueprint);
                currentBlueprint=null;
                SpawnBlueprint();
            } 
    }

//2.blueprint related
    void SpawnBlueprint()
    {
        // Instantiate the blueprint at the center of the screen
        currentBlueprint = Instantiate(chosenBlueprint);
    }

//4.check currency and spawn
    public void TriggerSpawning(){
        //this is called by UI yes confirmation
        if(currentResourcePrice<=currency){
                    PlaceResources(); 
                }
            else{
                    Debug.Log("Not Enough");
                    //visual representation for not enough credits------------
                    Destroy(currentBlueprint);//this will be removed
                    // or may trigger a panel of not enough resources. 
                }
    }   
    //2.intiating
    void PlaceResources()
    {
        if (currentBlueprint != null)
        {
            //
            // Check if the blueprint is in a valid location
            if (!currentBlueprint.GetComponent<BluePrint>().ReturnIsColliding())
            {
                // Instantiate the actual farm and destroy the blueprint
                Instantiate(chosenResource, currentBlueprint.transform.position, Quaternion.identity);
                Destroy(currentBlueprint);
                currentBlueprint = null;


                //cut the cost--------------
                currencyManager.SpendWood(currentResourcePrice);


                //nulling currentResourcePrice
                currentResourcePrice=0;
            }
        }
    } 

//5.Cleanup
    public void CancleBuilding(){
        //Triggered by ui cancel X button on confirmation
        Destroy(currentBlueprint);
    }
}
