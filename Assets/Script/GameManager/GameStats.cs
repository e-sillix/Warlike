using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Ensure you have this for TextMeshPro support

public class GameStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;
    [SerializeField] private int targetFPS ;

    private float timer;
    private int frameCount;

    void Start()
    {
        // Load saved FPS setting (default = 60 FPS)
        Application.targetFrameRate = PlayerPrefs.GetInt("TargetFPS", targetFPS);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        frameCount++;

        if (timer >= 1f) // Every 1 second
        {
            fpsText.text = "FPS: " + frameCount; // Display FPS
            frameCount = 0; // Reset frame count
            timer = 0f; // Reset timer
        }
    }

    public void SetTargettedFPS(int fps)
    {
        // targetFPS = fps;
        PlayerPrefs.SetInt("TargetFPS", fps);
        PlayerPrefs.Save(); // Ensure data is stored immediately
        Application.targetFrameRate = fps; // Apply FPS cap
    }

    public int GetTargettedFPS()
    {
        //return the limit chosen by the player
        return PlayerPrefs.GetInt("TargetFPS", 60);;
    }
}
