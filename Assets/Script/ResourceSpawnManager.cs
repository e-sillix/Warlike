using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject farmPrefab;           // The actual farm prefab
    [SerializeField] private GameObject farmBluePrintPrefab;  // The blueprint prefab to show while positioning
    [SerializeField] private GameObject ResourcePriceManager;  //for price returning
    [SerializeField] private GameObject CurrencyManager;  //for current resources returning
    [SerializeField] private LayerMask groundLayer;              // The layer mask for the ground
    [SerializeField] private LayerMask BlueLayer;              // The layer mask for the ground
    [SerializeField] private float farmRadius = 2f;           // The radius around the farm to check for collisions
    [SerializeField] private KeyCode spawnKey = KeyCode.F;    // The key to spawn the blueprint
    [SerializeField] private KeyCode CancelKey = KeyCode.C;    // The key to cancel the constructions

    private GameObject currentBlueprint;    // The current blueprint instance

    private FarmBluePrint farmBluePrint;
    private ResourcePriceManager resourcePriceManager;
    private CurrencyManager currencyManager;
    private int currentResourcePrice;
    private int currency;
   
   void Start(){
    resourcePriceManager=ResourcePriceManager.GetComponent<ResourcePriceManager>();
    currencyManager=CurrencyManager.GetComponent<CurrencyManager>();
   }
    void Update()
    {       
        if (Input.GetKeyDown(spawnKey))
        {
            //Get price----------------
            currentResourcePrice=resourcePriceManager.ReturnFarmPrice();

            //Get current currency
            currency=currencyManager.ReturnCurrentCurrency();
            
            if (currentBlueprint == null)
            {
                //conditioning with current money
                SpawnBlueprint();                
            }
            else
            {
                if(currentResourcePrice<=currency){
                    PlaceFarm(); 
                }
                else{
                    Debug.Log("Not Enough");
                    //visual representation for not enough credits------------

                }
            }
        }
        else if(Input.GetKeyDown(CancelKey)){
            if(currentBlueprint!=null){
                //this will cancel the farm blueprint
                Destroy(currentBlueprint);
                currentBlueprint = null;
                Debug.Log("Canceled");
            }
        }

        if (currentBlueprint != null)
        {
            UpdateBlueprintPosition();
        }
    }

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
}
