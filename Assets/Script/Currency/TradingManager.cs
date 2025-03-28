using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingManager : MonoBehaviour
{
    [SerializeField] private CurrencyManager currencyManagerGO;
    private CurrencyManager currencyManager;

    private int[] allResources;

    private int wood,grain,stone;
    void Start(){
        currencyManager=currencyManagerGO.GetComponent<CurrencyManager>();
    }
    public bool IsEnoughResource(int woodCost,int grainCost,int stoneCost){
        //getting all the resources
        allResources = currencyManager.ReturnAllResources();
        wood=allResources[0];
        grain=allResources[1];
        stone=allResources[2];
        //checking all the resources  ++++++++++ 
        //might return some numbers for ui missing resource counter
        return wood >= woodCost && 
        grain >= grainCost &&
        stone>= stoneCost;
    }   

    public void SpendingResources(int woodCost,int grainCost,int stoneCost){
        Debug.Log("Cost cutted :"+ woodCost+","+ grainCost+","+ stoneCost);
        currencyManager.SpendBuildingCost(woodCost, grainCost, stoneCost);
    }
}
