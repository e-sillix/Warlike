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

   public (int years, int months, int days, int hours, int minutes, int seconds) CalculateTimeElapsed()
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

        // Extract different time components
        int years = currentTime.Year - lastTime.Year;
        int months = currentTime.Month - lastTime.Month;
        int days = currentTime.Day - lastTime.Day;
        int hours = timeDifference.Hours;
        int minutes = timeDifference.Minutes;
        int seconds = timeDifference.Seconds;

        // Adjust negative values for months and days
        if (months < 0)
        {
            years--;
            months += 12;
        }
        if (days < 0)
        {
            months--;
            days += DateTime.DaysInMonth(lastTime.Year, lastTime.Month);
        }

        // Debug log for reference
        Debug.Log($"Elapsed Time: {years} Years, {months} Months, {days} Days, {hours} Hours, {minutes} Minutes, {seconds} Seconds.");

        // Return as a tuple
        return (years, months, days, hours, minutes, seconds);
    }
    else
    {
        Debug.Log("No previous time found. First-time launch.");
        return (0, 0, 0, 0, 0, 0);
    }
}


}
