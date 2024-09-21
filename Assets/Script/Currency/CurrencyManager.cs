using UnityEngine.UI; 
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField]private int deltaWood = 0;
    [SerializeField]private int deltaGrain = 0;
    [SerializeField]private int deltaStone = 0;
    public TextMeshProUGUI woodsCounter;    
    public TextMeshProUGUI grainCounter;    
    public TextMeshProUGUI stoneCounter;    
    // [SerializeField] private int woodCurrentCurrency=0;
    private Dictionary<ResourceType, int> resourceCurrencies = new Dictionary<ResourceType, int>()
    {
        { ResourceType.Wood, 0 },
        { ResourceType.Grain, 0 },
        { ResourceType.Stone, 0 }
    };
    
    void Start(){
        //initializing with editor given no..
        resourceCurrencies[ResourceType.Wood] = deltaWood;
        resourceCurrencies[ResourceType.Grain] = deltaGrain;
        resourceCurrencies[ResourceType.Stone] = deltaStone;
    }
    
    private void UpdateUICounter(){
        //update counter ui
        woodsCounter.text = "Woods: " + resourceCurrencies[ResourceType.Wood].ToString();
        grainCounter.text = "Grains: " + resourceCurrencies[ResourceType.Grain].ToString();
        stoneCounter.text = "Stone: " + resourceCurrencies[ResourceType.Stone].ToString();
    }


    public void AddResource(ResourceType type, int amount)
    {
        //collection of resource by farms.
        switch (type)
        {
            case ResourceType.Wood:
                resourceCurrencies[ResourceType.Wood] += amount;
                break;
            case ResourceType.Grain:
                resourceCurrencies[ResourceType.Grain] += amount;
                break;
            case ResourceType.Stone:
                resourceCurrencies[ResourceType.Stone] += amount;
                break;
        }
    }

    public void CollectingAllresourceAmount(ResourceType resourceTypeToCollect){
        //this will be called by TriggerCollectionOfresourceAmount in farm (indirectly by ui icon)
        
        Farm[] allFarms = FindObjectsOfType<Farm>();
        foreach (Farm farm in allFarms)
        {
            if (farm.resourceType == resourceTypeToCollect)
            {
                int collected = farm.ReturnResourceAmount();
                AddResource(farm.resourceType, collected);
            }
          
        }        
            UpdateUICounter();   
    }
     public Dictionary<ResourceType, int> ReturnAllResources()
    {
        return new Dictionary<ResourceType, int>(resourceCurrencies); // Return a copy of the dictionary
    }

    public void SpendBuildingCost(int woodCost,int grainCost,int stoneCost){
        //spending resources 
        //this needs to be changed.
        resourceCurrencies[ResourceType.Wood] -= woodCost;
        resourceCurrencies[ResourceType.Grain] -= grainCost;
        resourceCurrencies[ResourceType.Stone] -= stoneCost;     
        UpdateUICounter();
    }
   
}


//for all resources
public enum ResourceType
{
    Wood,
    Grain,
    Stone
}