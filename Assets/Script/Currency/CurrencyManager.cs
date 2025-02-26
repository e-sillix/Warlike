using UnityEngine.UI; 
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField]private int deltaWood ;
    [SerializeField]private int deltaGrain ;
    [SerializeField]private int deltaStone ;
    [SerializeField]private TextMeshProUGUI woodsCounter;    
    [SerializeField]private TextMeshProUGUI grainCounter;    
    [SerializeField]private TextMeshProUGUI stoneCounter;    
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
        UpdateUICounter();
    }
    
    private void UpdateUICounter()
{
    // Update counter UI with formatted numbers
    woodsCounter.text = "W: " + NumberFormatter.FormatNumber(resourceCurrencies[ResourceType.Wood]);
    grainCounter.text = "G: " + NumberFormatter.FormatNumber(resourceCurrencies[ResourceType.Grain]);
    stoneCounter.text = "S: " + NumberFormatter.FormatNumber(resourceCurrencies[ResourceType.Stone]);
}

    public static class NumberFormatter
{
    public static string FormatNumber(float num)
    {
        if (num >= 1_000_000_000) // Billion
            return (num / 1_000_000_000f).ToString("0.##") + "B";
        else if (num >= 1_000_000) // Million
            return (num / 1_000_000f).ToString("0.##") + "M";
        else if (num >= 1_000) // Thousand
            return (num / 1_000f).ToString("0.#") + "K";
        else
            return num.ToString("0"); // Normal number
    }
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
    public void CollectMinedResource(int[] resource){
        resourceCurrencies[ResourceType.Wood] += resource[0];
        resourceCurrencies[ResourceType.Grain] += resource[1];
        resourceCurrencies[ResourceType.Stone] += resource[2];
        Debug.Log("Resource added to the treasure {w,g,s}: "+resource[0]+resource[1]+resource[2]);
        UpdateUICounter();   

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