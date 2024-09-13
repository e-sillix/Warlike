using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NullScript : MonoBehaviour
{
    // A dictionary to store the building data (prefabs, blueprints, prices)
    private Dictionary<string, BuildingData> buildingDataDict = new Dictionary<string, BuildingData>();

    [SerializeField] private GameObject ResourcePriceManager;  // For price returning
    [SerializeField] private GameObject CurrencyManager;       // For current resources returning
    [SerializeField] private LayerMask groundLayer;            // The layer mask for the ground

    private GameObject currentBlueprint;    // The current blueprint instance
    private BuildingData currentBuildingData;
    private ResourceStatsManager resourceStatsManager;
    private CurrencyManager currencyManager;
    private int currentResourcePrice;
    private int currency;

    void Start()
    {
        resourceStatsManager = ResourcePriceManager.GetComponent<ResourceStatsManager>();
        currencyManager = CurrencyManager.GetComponent<CurrencyManager>();

        // Initialize building data dictionary for each building type
        buildingDataDict.Add("Farm", new BuildingData
        {
            prefab = farmPrefab,
            blueprintPrefab = farmBluePrintPrefab,
            price = resourceStatsManager.ReturnFarmPrice()
        });

        buildingDataDict.Add("Barracks", new BuildingData
        {
            prefab = barrackPrefab,
            blueprintPrefab = barrackBlueprintPrefab,
            price = resourceStatsManager.ReturnBarrackPrice()
        });

        // Add more buildings here as needed...
    }

    void Update()
    {
        // Updates blueprint position on screen after the build option is clicked
        if (currentBlueprint != null)
        {
            UpdateBlueprintPosition();
        }
    }

    void SpawnBlueprint()
    {
        // Instantiate the blueprint at the center of the screen
        currentBlueprint = Instantiate(currentBuildingData.blueprintPrefab);
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

    void PlaceBuilding()
    {
        if (currentBlueprint != null)
        {
            // Check if the blueprint is in a valid location
            var blueprintComponent = currentBlueprint.GetComponent<FarmBluePrint>(); // Use a more generic name if necessary
            if (blueprintComponent != null && !blueprintComponent.ReturnIsColliding())
            {
                // Instantiate the actual building and destroy the blueprint
                Instantiate(currentBuildingData.prefab, currentBlueprint.transform.position, Quaternion.identity);
                Destroy(currentBlueprint);
                currentBlueprint = null;

                // Deduct the cost
                currencyManager.SpendWood(currentResourcePrice);

                // Reset current resource price
                currentResourcePrice = 0;
            }
        }
    }

    public void TriggerBlueprint(string buildingType)
    {
        // Triggered by UI build button, e.g., "Farm", "Barracks"
        if (buildingDataDict.ContainsKey(buildingType))
        {
            currentBuildingData = buildingDataDict[buildingType];

            // Get price and current currency
            currentResourcePrice = currentBuildingData.price;
            currency = currencyManager.ReturnCurrentCurrency();

            if (currentBlueprint == null)
            {
                // Check if player has enough money
                if (currency >= currentResourcePrice)
                {
                    SpawnBlueprint();
                }
                else
                {
                    Debug.Log("Not enough resources!");
                    // Trigger a visual indication for not enough resources
                }
            }
        }
        else
        {
            Debug.LogError("Building type not found: " + buildingType);
        }
    }

    public void TriggerSpawning()
    {
        if (currency >= currentResourcePrice)
        {
            PlaceBuilding();
        }
        else
        {
            Debug.Log("Not enough resources!");
            Destroy(currentBlueprint);  // Remove or handle this differently for better UX
        }
    }

    public void CancelBuilding()
    {
        Destroy(currentBlueprint);
        currentBlueprint = null;
    }
}

// Class to hold building-related data
[System.Serializable]
public class BuildingData
{
    public GameObject prefab;          // Actual building prefab
    public GameObject blueprintPrefab; // Blueprint prefab to show
    public int price;                  // Price of the building
}
