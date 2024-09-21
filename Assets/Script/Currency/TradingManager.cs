using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingManager : MonoBehaviour
{
    [SerializeField] private CurrencyManager currencyManagerGO;
    private CurrencyManager currencyManager;

    private Dictionary<ResourceType, int> allResources;
    void Start(){
        currencyManager=currencyManagerGO.GetComponent<CurrencyManager>();
    }
    public bool IsEnoughResource(int woodCost,int grainCost,int stoneCost){
        //getting all the resources
        allResources = currencyManager.ReturnAllResources();

        //checking all the resources  ++++++++++ 
        //might return some numbers for ui missing resource counter
        return allResources[ResourceType.Wood] >= woodCost && 
        allResources[ResourceType.Grain] >= grainCost &&
        allResources[ResourceType.Stone] >= stoneCost;
    }   

    public void SpendingResources(int woodCost,int grainCost,int stoneCost){
        Debug.Log("Cost cutted :"+ woodCost+","+ grainCost+","+ stoneCost);
        currencyManager.SpendBuildingCost(woodCost, grainCost, stoneCost);
    }
}
