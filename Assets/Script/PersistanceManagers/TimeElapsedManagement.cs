using System;
using UnityEngine;

public class TimeElapsedManagement : MonoBehaviour
{
    void OnApplicationQuit()
    {
        PlayerPrefs.SetString("TimeElapsed", DateTime.UtcNow.ToString()); // Store UTC 
        // time for consistency
        PlayerPrefs.Save();
    }

    void Start()
    {
        // Store current time
        DateTime currentTime = DateTime.UtcNow;

        if (PlayerPrefs.HasKey("TimeElapsed"))
        {
            // Retrieve last saved time
            string lastTimeString = PlayerPrefs.GetString("TimeElapsed");

            // Convert stored string back to DateTime
            DateTime lastTime = DateTime.Parse(lastTimeString);

            // Calculate elapsed time
            TimeSpan timeDifference = currentTime - lastTime;

            // Convert to seconds
            float elapsedSeconds = (float)timeDifference.TotalSeconds;

            Debug.Log("Elapsed time since last session: " + elapsedSeconds + " seconds.");
        }
        else
        {
            Debug.Log("No previous time found. First-time launch.");
        }
    }
}
