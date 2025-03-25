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
    
    public void StartTraining(int time)
    {
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
        }

        TrainingProgressBarPanel.gameObject.SetActive(false); // Hide UI when canceled
        // timeRemainingText.gameObject.SetActive(false);
    }

    private IEnumerator TrainingRoutine(int time)
    {
        Debug.Log("Training started...");
        float elapsedTime = 0f;
        TrainingProgressBarPanel.SetActive(true);  
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / time;
            trainingProgressBar.value = progress; // Update progress bar

            int remainingTime = Mathf.CeilToInt(time - elapsedTime); // Calculate remaining time
            UpdateTimeDisplay(remainingTime); // Update UI text

            yield return null; // Wait for next frame
        }

        Debug.Log("Training completed!");
        trainingProgressBar.value = 1;
        timeRemainingText.text = "00:00"; // Training is done
        TrainingProgressBarPanel.SetActive(false);

        // trainingProgressBar.gameObject.SetActive(false);
        // timeRemainingText.gameObject.SetActive(false);

        UpdateStateOfBarrack();
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
