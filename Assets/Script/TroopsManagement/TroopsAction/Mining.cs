using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{//attached to theUnit
    private Coroutine miningCoroutine;
    private TheUnit theUnit;
    private int capacity,minesResources,miningRate=1,minedAmount,usedCapacity;
    private float miningStartTime; // Track when mining started
    
    private TheMine theMine;//mining reference
    private string mineType;


    private int[] resourcesTypeLoad={0,0,0};
    
    void Start(){
        theUnit=GetComponent<TheUnit>();        
    }
    public void SetMiningStats(int trc){
        //by tism
        capacity=trc;
    }

    public bool IsMiningPossible(){
        // by mining manager
        return capacity>usedCapacity;
    }
    public void StartMining(TheMine TheMineP)
    {
        theMine=TheMineP;
        mineType=theMine.mineType.ToString();
        Debug.Log("starting mine:"+mineType);
        minesResources=theMine.ReturnResources();
        // usedCapacity=theUnit.usedCapacity;
        if(capacity<=usedCapacity){
            Debug.Log("can't load more");
            return;
        }
        
        if (miningCoroutine == null)
        {
            miningCoroutine = StartCoroutine(MineResource());
        }
    }

    // Function to stop mining and transfer collected resources
    public void StopMining()
    {//this is being called by theUnit 
        if (miningCoroutine != null)
        {
            StopCoroutine(miningCoroutine);
            CalculateAndStoreMinedResources(); // Calculate mined resources based on time spent
            miningCoroutine = null;
        }
        TransferResourcesToTroops(); // Transfer resources after calculation
    }

    // Coroutine to mine resource based on total required time
   private IEnumerator MineResource()
    {
        miningStartTime = Time.time; // Record the time when mining started

        // Calculate the maximum resource that can be mined based on available capacity and mine resources
        int maxPossibleMining = Mathf.Min(capacity-usedCapacity, minesResources);
        Debug.Log("capacity+MineResource"+(capacity-usedCapacity)+","+minesResources);
        
        // Calculate the total time needed to mine the required resource
        float miningDuration = maxPossibleMining / (float)miningRate;

        Debug.Log("Mining Duration:"+miningDuration);
        // Wait for the calculated mining duration
        yield return new WaitForSeconds(miningDuration);

        // If mining completes naturally, calculate total mined resources
        CalculateAndStoreMinedResources();
        Debug.Log("mining stopped with process");
        TransferResourcesToTroops();
        
    }

    // Calculate and store mined resources based on elapsed time
    private void CalculateAndStoreMinedResources()
    {
        float elapsedTime = Time.time - miningStartTime;
        minedAmount = Mathf.Min((int)(elapsedTime * miningRate), minesResources, capacity);

        // Deduct mined resources from the mine and store them

        //---------------------------------+++++++++++++=
        //deducting needed
        minesResources -= minedAmount;
        theMine.DeductResources(minedAmount); // Update mine's resources
        
        
    }    

    // Transfer collected resources to central storage
    private void TransferResourcesToTroops()
    {
        Debug.Log("Mined Resources:"+minedAmount);
        TransferResourceToTroops();
        Refresh();
    }

    void TransferResourceToTroops(){
        usedCapacity+=minedAmount;
        if(mineType=="wood"){
            resourcesTypeLoad[0]+=minedAmount;
        }
        else if(mineType=="grain"){
            resourcesTypeLoad[1]+=minedAmount;
        }
        else if(mineType=="stone"){
            resourcesTypeLoad[2]+=minedAmount;
        }
        else{
            Debug.Log("trying to load something unknown");
        }
    }

    void Refresh(){
        theMine.setMineStatus(false);
        minedAmount=0;
        minesResources=0;
        miningStartTime=0;
        theMine=null;
        theUnit.isMining=false;
        miningCoroutine = null;
    }



    public int[] ReturnResourcesTypeLoad(){
        return resourcesTypeLoad;
    }
    public int[] ReturnMiningData(){
        // for ui 
        return new int[] { capacity, usedCapacity };
    }
    
}
