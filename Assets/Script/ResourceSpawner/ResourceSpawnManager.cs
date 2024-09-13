using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceSpawnManager : MonoBehaviour
{
    //should only accept trigger from ui and call the respective resource spawn managers
    //maybe dynamic trigger respective spawning

    [SerializeField] private GameObject farmPrefab;           // The actual farm prefab
    [SerializeField] private GameObject farmBluePrintPrefab;  // The blueprint prefab to show while positioning
    [SerializeField] private GameObject BarrackPrefab;           // The actual barrack prefab
    [SerializeField] private GameObject BarrackBluePrintPrefab;  // The blueprint prefab to show while positioning
    [SerializeField] private GameObject ResourcePriceManager;  //for price returning
    [SerializeField] private GameObject CurrencyManager;  //for current resources returning
    [SerializeField] private LayerMask groundLayer;              // The layer mask for the ground
    [SerializeField] private LayerMask BlueLayer;              // The layer mask for the ground
    // [SerializeField] private float farmRadius = 2f;           // The radius around the farm to check for collisions
    

    private GameObject currentBlueprint;    // The current blueprint instance

    private FarmBluePrint farmBluePrint;

    private ResourceStatsManager resourceStatsManager;
    private CurrencyManager currencyManager;
    private int currentResourcePrice;
    private int currency;
   
   void Start(){
    resourceStatsManager=ResourcePriceManager.GetComponent<ResourceStatsManager>();
    currencyManager=CurrencyManager.GetComponent<CurrencyManager>();
   }
    void Update()
    {//updates blueprint position on screen after the build option is clicked
        if (currentBlueprint != null)
        {
            UpdateBlueprintPosition();
        }
    }

//0. option of building and setting which building is chosen
    public void FarmIsChoosen(){
        //set current resource price
        //Get  and set price---------------- 
        currentResourcePrice=resourceStatsManager.ReturnFarmPrice();
        //set current resource prefab and blueprint

    }
    

//1.blueprint related
    void SpawnBlueprint()
    {
        // Instantiate the blueprint at the center of the screen
        currentBlueprint = Instantiate(farmBluePrintPrefab);
        farmBluePrint=currentBlueprint.GetComponent<FarmBluePrint>();
        UpdateBlueprintPosition();
    }

    void UpdateBlueprintPosition()
    {
        // Create a ray from the center of the screen
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Perform the raycast and check if it hits the ground
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            currentBlueprint.transform.position = hit.point;

        }
    }
//2.intiating
    void PlaceFarm()
    {
        if (currentBlueprint != null)
        {
            //
            // Check if the blueprint is in a valid location
            if (!farmBluePrint.ReturnIsColliding())
            {
                // Instantiate the actual farm and destroy the blueprint
                Instantiate(farmPrefab, currentBlueprint.transform.position, Quaternion.identity);
                Destroy(currentBlueprint);
                currentBlueprint = null;


                //cut the cost--------------
                currencyManager.SpendWood(currentResourcePrice);


                //nulling currentResourcePrice
                currentResourcePrice=0;
            }
        }
    } 

//3.taking input from ui
    public void TriggerBluePrint(){   
        //this will be triggered by UI build button
        // //Get price---------------- 
        // currentResourcePrice=resourceStatsManager.ReturnFarmPrice();    //this will be changed to dynamic
        //Get current currency
        currency=currencyManager.ReturnCurrentCurrency();
            
        if (currentBlueprint == null)
            {
                //conditioning with current money
                SpawnBlueprint();                
            }        
    }

//4.check currnency and spawn
    public void TriggerSpawning(){
        if(currentResourcePrice<=currency){
                    PlaceFarm(); 
                }
            else{
                    Debug.Log("Not Enough");
                    //visual representation for not enough credits------------
                    Destroy(currentBlueprint);//this will be removed
                    // or may trigger a panel of not enough resources. 
                }
    }   

//5.Cleanup
    public void CancleBuilding(){
        Destroy(currentBlueprint);
    }
}
