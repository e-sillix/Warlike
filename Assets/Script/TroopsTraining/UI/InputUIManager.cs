using UnityEngine.UI; // For UI components
using TMPro; // For TextMeshPro components if using TextMeshPro
using UnityEngine;

public class InputUIManager : MonoBehaviour
{
    //this handles ui for taking input and giving how many to train
    public Slider resourceSlider;
    public TMP_InputField resourceInputField; // For TextMeshPro InputField    

    private int currentValue;
       

    private int GetTheMaximumCapacity()// this will removed for making it dynamic
    {       
        //only this function is getting limit in whole rn.
        int totalBarrackCapacity = 0;        
        Barracks[] allBarracks = FindObjectsOfType<Barracks>();        
        foreach (Barracks Barrack in allBarracks)
        {
            totalBarrackCapacity += Barrack.ReturnTroopsCapacity();//this will be changed named
        }
        return totalBarrackCapacity;
    }
    public void setLimitValues(){//++++ need to pass a value for max capacity
        //this will be triggered by start and on every time barrack is clicked.RN TrainingUIManager 56.
        // Set the slider's min and max values
        resourceSlider.minValue = 0;
        resourceSlider.maxValue = GetTheMaximumCapacity();
        resourceSlider.value = resourceSlider.maxValue;
    }
    void Start()
    {
        setLimitValues();

        // Initialize the input field with the slider's starting value
        resourceInputField.text = Mathf.RoundToInt(resourceSlider.value).ToString();
        currentValue = Mathf.RoundToInt(resourceSlider.value);

        // Add listeners to handle value changes
        resourceSlider.onValueChanged.AddListener(OnSliderValueChanged);
        resourceInputField.onEndEdit.AddListener(OnInputFieldValueChanged);
    }

    // When the slider value changes
    void OnSliderValueChanged(float value)
    {
        currentValue = Mathf.RoundToInt(value);
        resourceInputField.text = currentValue.ToString(); // Update the input field to reflect slider's value
    }

    // When the input field value changes
    void OnInputFieldValueChanged(string value)
    {
        int number;
        if (int.TryParse(value, out number))
        {
            // Ensure the input value is clamped within the slider's range
            number = Mathf.Clamp(number, (int)resourceSlider.minValue, (int)resourceSlider.maxValue);
            currentValue = number;
            resourceSlider.value = number; // Update the slider to reflect the input field's value
        }
        else
        {
            // Reset to the last valid value if the input is not a valid number
            resourceInputField.text = currentValue.ToString();
        }
    }

    // Method to get the selected value if needed elsewhere
    public int GetNoofTroopsToTrain()
    {
        //this will be called to get how many troops to trains
        return currentValue;
    }
}
