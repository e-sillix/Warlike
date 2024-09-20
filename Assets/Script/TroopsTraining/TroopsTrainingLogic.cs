using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this will be attached to troopstrainingmanager
public class TroopsTrainingLogic : MonoBehaviour
{
    //this will be triggered by ui panel
    // this will start training and will accept barracks capacity for training and dividing time.
    // and trigger their icons
    // for the barracks being spawned during training should also immediately should be handle somehow.

    private int totalBarrackCapacity = 0;
    [SerializeField] private GameObject ArmyCountManager;//this is for updating Count
    public float baseTrainingTime = 10f; // Base training time for one barrack
    private float adjustedTrainingTime;
    private int barracksCount = 0;
    private float elapsedTime = 0f;
    public bool isTrainingInProgress = false; // Track if training is active
    private Coroutine trainingCoroutine;
    private int TroopsCountCurrentlyTraining;
    [SerializeField] private ResourceSpending resourceSpending;

    public void StartTrainingTroops()
    {
        // this will be triggered by barrack prefab panel "yes"
        //need to check if it has enough resources
        if(resourceSpending.IsEnoughForTroops()){        
        if (!isTrainingInProgress)
        {
            GetAllTheBarracksStats();
            if (barracksCount > 0)
            {
                adjustedTrainingTime = baseTrainingTime / barracksCount;
                elapsedTime = 0f; // Reset elapsed time before starting
                trainingCoroutine = StartCoroutine(TrainingRoutine());
                resourceSpending.SpendingOnTraining();
                isTrainingInProgress = true;
            }
        }
        else
        {
            Debug.Log("Training is going on!!!!!!!");
        }
        }
        else{
            Debug.Log("not enough resources for training.");
        }
    }
    public void StopTrainingTroops(){
        //this will also be triggered by ui panel cancel
        Debug.Log("This one is underConstruction!!!!!!!");
    }

    private void GetAllTheBarracksStats()
    {
        // Find all the barracks for their stats
        //this function is only created for getting troops training count
        totalBarrackCapacity = 0;
        barracksCount=0;
        Barracks[] allBarracks = FindObjectsOfType<Barracks>();
        barracksCount = allBarracks.Length;
        foreach (Barracks Barrack in allBarracks)
        {
            totalBarrackCapacity += Barrack.ReturnTroopsCapacity();//this will be changed named
        }
        TroopsCountCurrentlyTraining=totalBarrackCapacity;
    }
    private void UpdateAllTheBarracksStats()
    {
        // update all the barracks for their stats
        totalBarrackCapacity = 0;
        barracksCount=0;
        Barracks[] allBarracks = FindObjectsOfType<Barracks>();
        barracksCount = allBarracks.Length;
    }

    private IEnumerator TrainingRoutine()
    {
        Debug.Log("Training started for " + adjustedTrainingTime + " seconds");

        while (elapsedTime < adjustedTrainingTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        TrainingDone();
    }

    private void TrainingDone()
    {
        Debug.Log("Training completed. Adding troops.");
        isTrainingInProgress = false;
        // Code to add troops
        TriggerBarracksAllIcons();
    }

    private void TriggerBarracksAllIcons()
    {
        // This should be called after the set amount of time
        Debug.Log("Triggering barracks icons");
        // Logic to update UI/icons   


        //this will be removed and changed the amount
        AcceptNewTroops(TroopsCountCurrentlyTraining);     

    }
    public void AcceptNewTroops(int Amount){
        //this will be triggered by clicking on Trigger icon option
        ArmyCount troopsCounterManager=ArmyCountManager.GetComponent<ArmyCount>();
        troopsCounterManager.AddSoldiers(Amount);
    }

    private void ReInsitaiteTrainingTime()
    {
        if (trainingCoroutine != null)
        {
            StopCoroutine(trainingCoroutine); // Stop the current training process

            // Recalculate the training time
            float remainingTime = adjustedTrainingTime - elapsedTime;
            UpdateAllTheBarracksStats();
            adjustedTrainingTime = remainingTime / barracksCount;
            elapsedTime = 0f;

            // Resume training with the new adjusted time
            trainingCoroutine = StartCoroutine(TrainingRoutine());
        }
    }

    public void IsTraining()
    {
        if (isTrainingInProgress)
        {
            Debug.Log("New barrack created during training");
            ReInsitaiteTrainingTime();
        }
    }
}
