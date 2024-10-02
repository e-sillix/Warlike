using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingHandler : MonoBehaviour
{

    [SerializeField] private TheBarrack theBarrack;
     
    private Coroutine trainingCoroutine;
     public void StartTraining(int time)
    {
        if (trainingCoroutine != null)
        {
            StopCoroutine(trainingCoroutine); // If there's already a coroutine running, stop it
        }
        
        trainingCoroutine = StartCoroutine(TrainingRoutine(time)); // Start the coroutine with the training time
    }

    // Coroutine that runs for the duration of the training process
    private IEnumerator TrainingRoutine(int time)
    {
        Debug.Log("Training started...");

        // Wait for the specified time (in seconds) before proceeding
        yield return new WaitForSeconds(time);

        Debug.Log("Training completed!");

        // Once the training is done, update the state of the barrack (or any other action you want)
        UpdateStateOfBarrack();
    }

    void UpdateStateOfBarrack(){
        theBarrack.TrainingEnded();
    }
}
