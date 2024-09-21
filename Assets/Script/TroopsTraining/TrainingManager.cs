using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    //this is only called by barrack on creation and ttl when done training
    [SerializeField] private ResourceSpending resourceSpending;
    [SerializeField] private InputUIManager inputUIManager;
    [SerializeField] private TradingManager tradingManager;
    [SerializeField] private TroopsTrainingLogic trainingLogic;
    [SerializeField] private ArmyCount troopsCounter;

    private int woodCostTroops,grainCostTroops,stoneCostTroops,numberOfTroops;
    
    bool IsTrainingInProgress(){
        //called by barrack start() ,also in this too starting training
        return trainingLogic.isTrainingInProgress;
    }
    void GetStatsOfTroopsToTrain(){
        //get the basics stats
        woodCostTroops=resourceSpending.woodCost;
        stoneCostTroops=resourceSpending.stoneCost;
        grainCostTroops=resourceSpending.grainCost;
        //get the numbers
        numberOfTroops=inputUIManager.GetNoofTroopsToTrain();
    }
    void ResetStats(){
        woodCostTroops=0;
        stoneCostTroops=0;
        grainCostTroops=0;
        numberOfTroops=0;
    }

    bool ResourceCheck(){
        //manages the cost of training ,more like passing and taking numbers
        GetStatsOfTroopsToTrain();

        //pass these stats around
        return tradingManager.IsEnoughResource(woodCostTroops*numberOfTroops,
        grainCostTroops*numberOfTroops,stoneCostTroops*numberOfTroops);
    }


    void CuttingCost(){
        //this will cut the cost
        tradingManager.SpendingResources(woodCostTroops*numberOfTroops,
        grainCostTroops*numberOfTroops,stoneCostTroops*numberOfTroops);
    }

    void StartingTraining(){
        //Triggering Training
        trainingLogic.StartTrainingTroops(numberOfTroops); 
    }

    public void InitiatingTrainingProcess(){
        //this will be triggered by UI Train
        if(!IsTrainingInProgress()){        
        if(ResourceCheck()){//checkresource
            if(numberOfTroops!=0){
            CuttingCost();
            StartingTraining();
            }
            else{
                Debug.Log("Choose some number");
            }
        }  
        else{
            //trigger not Enough UI or something by child.
            Debug.Log("not enough resources");
        }}
        else{
            Debug.Log("Training is going on");
        }
    }

    public void StopTrainingTroops(){
        //this will also be triggered by ui panel cancel
        Debug.Log("This one is underConstruction!!!!!!!");
    }
    
    public void checkMidTrainingCreation(){
        if(IsTrainingInProgress()){
            Debug.Log("mid training barrack creation");
            MidTrainingBarrackCreation();
        }
    }
    void MidTrainingBarrackCreation(){
        //this one should be triggered by barrack start()
        trainingLogic.ReInsitaiteTrainingTime();
        //triggering Recalculation
    }

    public void TrainingDone(int Amount){
        //triggered by child trainingLogic
        //it should return numbers of troops to counter        
        troopsCounter.AddSoldiers(Amount);

        //Tell ui to trigger

        //reseting stats
        ResetStats();
    }

}
