using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpending : MonoBehaviour
{//******remember to change ui to it too for triggering each building or something like farm.in prefab maybe.
//**remember to remove traininguitrigger for infantry for chooosing 
    //this will store stats for troops cost for now
    //and handle spending on them and returning is is it to TroopsTrainingLogic


    private Dictionary<string, TroopsCost> TroopsCosts;
    private TroopsCost CostData;
    // [SerializeField] private TradingManager tradingManager;
    public int woodCost,grainCost,stoneCost; 

    void Start()
    {
        TroopsCosts = new Dictionary<string, TroopsCost>();

       // Add costs for different Troopss (name of Troops as key)
        TroopsCosts.Add("Infantry", new TroopsCost(wood: 2, grain: 3, stone: 1));

        //they will be added in future

        // TroopsCosts.Add("Archer", new TroopsCost(wood: 10, grain: 5, stone: 10));
        // TroopsCosts.Add("Cavalry", new TroopsCost(wood: 15, grain: 5, stone: 20));
        // TroopsCosts.Add("Mage", new TroopsCost(wood: 20, grain: 20, stone: 20));
        // Add more Troopss here as needed
    }


    // this for getting the data related to troops
    public void InfantryIsChosen(){
        //this will be triggered by UI of different type of soldier

        //rightnow it is triggered by traininguimanager 54 .need to be removed for making it dynamic--------

        CostData=GetTroopsCost("Infantry");
        woodCost=CostData.woodCostTr;
        grainCost=CostData.grainCostTr;
        stoneCost=CostData.stoneCostTr;
    }
    private TroopsCost GetTroopsCost(string TroopsName)
    {
        if (TroopsCosts.TryGetValue(TroopsName, out TroopsCost cost))
        {
            return cost;
        }
        else
        {
            Debug.LogError("Building name not found in dictionary: " + TroopsName);
            return null;
        }
    }

}