using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;
using Unity.Notifications;

public class SettingsManager : MonoBehaviour
{
   //Graphic settings
    public Slider FrameSlider;
    public TextMeshProUGUI FrameText;
    private GameStats gameStats;
    public Toggle soundToggle,notificationToggle;
    private int targetFPS;

    void Start()
    {
        gameStats=GetComponent<GameStats>();

        SoundStart();
        
        FrameStart();

        NotificationStart();
    }

    
    void SoundStart(){
        // Load saved state (if needed)
        soundToggle.isOn = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;

        // Apply initial sound state
        AudioListener.volume = soundToggle.isOn ? 1f : 0f;

        // Add listener for changes
        soundToggle.onValueChanged.AddListener(ToggleSound);
    }
    void NotificationStart(){
        notificationToggle.isOn = PlayerPrefs.GetInt("NotificationEnabled", 1) == 1;
        notificationToggle.onValueChanged.AddListener(ToggleNotification);
    }
    void FrameStart(){
        // Set min and max values
        FrameSlider.minValue = 20;
        FrameSlider.maxValue = 60;
        
        // Set default value
        targetFPS = gameStats.GetTargettedFPS();
        FrameSlider.value = targetFPS;
        FrameText.text = targetFPS.ToString();
        // Update text initially

        // Add listener to update text when slider value changes
        FrameSlider.onValueChanged.AddListener(UpdateText);
    }
    void ToggleSound(bool isOn)
    {
        AudioListener.volume = isOn ? 1f : 0f;
        PlayerPrefs.SetInt("SoundEnabled", isOn ? 1 : 0);
    }
    void ToggleNotification(bool isOn){
        PlayerPrefs.SetInt("NotificationEnabled", isOn ? 1 : 0);
        // PlayerPrefs.Save();
    }
    public void setCurrentData(){
        FrameSlider.value = gameStats.GetTargettedFPS();
        UpdateText(FrameSlider.value);
    }
    void UpdateText(float value)
    {
        FrameText.text = Mathf.RoundToInt(value).ToString();
        //triiger save UI

        gameStats.SetTargettedFPS(Mathf.RoundToInt(value));
    }
    
   
}
