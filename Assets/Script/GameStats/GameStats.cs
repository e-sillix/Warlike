using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMPro.TextMeshProUGUI fpsText;
    private float timer;
    private int frameCount;

    private void Start()
    {
        Application.targetFrameRate = 60;
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
}
