using UnityEngine.UI; 
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField]private int totalWood = 0;
    [SerializeField]private int totalGrain = 0;
    [SerializeField]private int totalStone = 0;
    public TextMeshProUGUI woodsCounter;    
    public TextMeshProUGUI grainCounter;    
    public TextMeshProUGUI stoneCounter;    
    [SerializeField] private int woodCurrentCurrency=0;
    
    
    private void UpdateUICounter(){
        woodsCounter.text = "Woods: " + totalWood.ToString();
        grainCounter.text = "Grains: " + totalGrain.ToString();
        stoneCounter.text = "Stone: " + totalStone.ToString();
    }


    public void AddResource(ResourceType type, int amount)
    {
        switch (type)
        {
            case ResourceType.Wood:
                totalWood += amount;
                break;
            case ResourceType.Grain:
                totalGrain += amount;
                break;
            case ResourceType.Stone:
                totalStone += amount;
                break;
        }
    }

    public void CollectingAllresourceAmount(){
        //this will be called by TriggerCollectionOfresourceAmount in farm (indirectly by ui icon)
        
        Farm[] allFarms = FindObjectsOfType<Farm>();
        foreach (Farm farm in allFarms)
        {
            // woodCurrentCurrency += farm.ReturnresourceAmount();
            // farm.triggerConsumingAnimation=true;
            int collected = farm.ReturnResourceAmount();
            AddResource(farm.resourceType, collected);
          
        }        
            UpdateUICounter();       
           
    }
    public int ReturnCurrentCurrency(){
        return woodCurrentCurrency;
    }

    public void SpendWood(int amount){
        woodCurrentCurrency=woodCurrentCurrency-amount;
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