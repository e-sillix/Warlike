using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingCostManager : MonoBehaviour
{
    private TroopsTrainingManager troopsTrainingManager;
    [SerializeField]private TroopsStatsManager troopsStatsManager;
    private TroopsDataPayload troopDataLevelSpecific;
    private int woodCost,grainCost,stoneCost,trainTime;
    void Start(){
        troopsTrainingManager=GetComponent<TroopsTrainingManager>();
    }
    public int[] ReturnTrainingCost(int[] troopsNumbers,string troopType){
        woodCost=0;
        grainCost=0;
        stoneCost=0;
        trainTime=0;

        // Debug.Log(troopsNumbers[1]);
        //get troops stats
        for(int i=0;i<5;i++){//5 is level
            troopDataLevelSpecific=troopsStatsManager.GetTroopsData(troopType ,i+1);
            woodCost+=troopDataLevelSpecific.woodTrainingCost*troopsNumbers[i];
            grainCost+=troopDataLevelSpecific.woodTrainingCost*troopsNumbers[i];
            stoneCost+=troopDataLevelSpecific.stoneTrainingCost*troopsNumbers[i];
            trainTime+=troopDataLevelSpecific.TrainingTime*troopsNumbers[i];
            // Debug.Log(woodCost+","+grainCost+","+stoneCost+","+trainTime);
            
        }

        //formulate it and return
        

        return new int[] {woodCost,grainCost,stoneCost,trainTime};
    }
}
