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

    // private int totalBarrackCapacity = 0;
    [SerializeField] private float rateOfTraining=0.5f;
    private float adjustedTrainingTime;
    private int barracksCount = 0;
    private float elapsedTime = 0f;
    public bool isTrainingInProgress = false; // Track if training is active
    private Coroutine trainingCoroutine;
    [SerializeField] private TrainingManager trainingManager;
    private int troops;

    public void StartTrainingTroops(int troopsToTrain)
    {
        //By Training Manager
        //need to check if it has enough resources
        troops=troopsToTrain;
        Debug.Log(troops);
            GetAllTheBarracksStats();
            if (barracksCount > 0)
            {
                float notadjustedTime=rateOfTraining*troops;
                adjustedTrainingTime = notadjustedTime / barracksCount;
                elapsedTime = 0f; // Reset elapsed time before starting
                trainingCoroutine = StartCoroutine(TrainingRoutine());
                isTrainingInProgress = true;
            }
    }
    

    private void GetAllTheBarracksStats()
    {
        // Find all the barracks for their stats
        //this function is only created for getting troops training count
        
        barracksCount=0;
        Barracks[] allBarracks = FindObjectsOfType<Barracks>();
        barracksCount = allBarracks.Length;
    }
    private void UpdateAllTheBarracksStats()
    {
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
        isTrainingInProgress = false;

        trainingManager.TrainingDone(troops);
        // Code to add troops
    }

    public void ReInsitaiteTrainingTime()
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
    
}
