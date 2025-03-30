using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrainingHandler : MonoBehaviour
{
    [SerializeField] private TheBarrack theBarrack;
    [SerializeField]private GameObject TrainingProgressBarPanel;
    [SerializeField] private Slider trainingProgressBar; // Assign in Inspector
    [SerializeField] private TextMeshProUGUI timeRemainingText; // Assign for time display

    private Coroutine trainingCoroutine;
    private float ProgressedTime;
    private float CompletionTime;
    private bool isTrainingOngoing;
    

    public void RefreshData(){
        ProgressedTime=0;
        CompletionTime=0;
        isTrainingOngoing=false;
    }
    public float GetProgressedTime(){
        //by buildingPersistenceManager
        // Debug.Log("ProgressedTime is "+ProgressedTime);
        return ProgressedTime;
    }
    public float GetTotalTime(){
        //by buildingPersistenceManager
        // Debug.Log("CompletionTime is "+CompletionTime);
        return CompletionTime;
    }
    public bool ReturnIsTrainingOngoing(){
        //return true if the training is ongoing
        return isTrainingOngoing;
    }
    public void ResumeTraining(int time,float TotalTime){
         ProgressedTime=time;
         CompletionTime=TotalTime;
        if (trainingCoroutine != null)
        {
            StopCoroutine(trainingCoroutine);
            TrainingProgressBarPanel.SetActive(false);
        }

        // trainingProgressBar.gameObject.SetActive(true); // Show UI
        trainingProgressBar.value = ProgressedTime/CompletionTime; // Reset progress
        UpdateTimeDisplay((int)(CompletionTime-ProgressedTime)); // Show full time

        trainingCoroutine = StartCoroutine(TrainingRoutine((int)CompletionTime));
    }
    public void StartTraining(int time)
    {   
        RefreshData();
        CompletionTime=time;
        if (trainingCoroutine != null)
        {
            StopCoroutine(trainingCoroutine);
            TrainingProgressBarPanel.SetActive(false);
        }

        // trainingProgressBar.gameObject.SetActive(true); // Show UI
        trainingProgressBar.value = 0; // Reset progress
        UpdateTimeDisplay(time); // Show full time

        trainingCoroutine = StartCoroutine(TrainingRoutine(time));
    }

    public void CancelTraining()
    {
        if (trainingCoroutine != null)
        {
            StopCoroutine(trainingCoroutine);
            RefreshData();
            // isTrainingOngoing=false;
        }

        TrainingProgressBarPanel.gameObject.SetActive(false); // Hide UI when canceled
        // timeRemainingText.gameObject.SetActive(false);
    }

    private IEnumerator TrainingRoutine(int time)
    {
        isTrainingOngoing=true;
        // Debug.Log("Training started...");
        // ProgressedTime = 0f;
        TrainingProgressBarPanel.SetActive(true);  
        while (ProgressedTime < time)
        {
            ProgressedTime += Time.deltaTime;
            float progress = ProgressedTime / time;
            trainingProgressBar.value = progress; // Update progress bar

            int remainingTime = Mathf.CeilToInt(time - ProgressedTime); // Calculate remaining time
            UpdateTimeDisplay(remainingTime); // Update UI text

            yield return null; // Wait for next frame
        }
        isTrainingOngoing=false;
        // Debug.Log("Training completed!");
        trainingProgressBar.value = 1;
        timeRemainingText.text = "00:00"; // Training is done
        TrainingProgressBarPanel.SetActive(false);

        // trainingProgressBar.gameObject.SetActive(false);
        // timeRemainingText.gameObject.SetActive(false);

        UpdateStateOfBarrack();
        RefreshData();
    }

    void UpdateStateOfBarrack()
    {
        theBarrack.TrainingEnded();
    }

    private void UpdateTimeDisplay(int secondsLeft)
    {
        int minutes = secondsLeft / 60;
        int seconds = secondsLeft % 60;
        timeRemainingText.text = $"{minutes:D2}:{seconds:D2}"; // Format: MM:SS
    }
}
