using UnityEngine.UI; // For UI components
using TMPro; // For TextMeshPro components if using TextMeshPro
using UnityEngine;

public class MarchSlider : MonoBehaviour
{
    //this will become dynamically one with training slider probably
  
    [SerializeField] private Slider level1Slider,level2Slider,level3Slider,level4Slider,level5Slider;
    [SerializeField] private TextMeshProUGUI level1Counter,level2Counter,level3Counter
    ,level4Counter,level5Counter;
    // this one will be removed .

    // public TMP_InputField level1Field,level2Field,level3Field,level4Field,level5Field; 
    // For TextMeshPro InputField   

    private int level1CounterLM,level2CounterLM,level3CounterLM,level4CounterLM,level5CounterLM;
    [SerializeField]private float TroopsCapacity=9999999999999; // This will be dynamically set in future
    private int level1Value,level2Value,level3Value,level4Value,level5Value; //this will store and return
    //value
    private int[] troopsData;
    private int currentTotal;           // Sum of all slider values
    private int totalSliderValue;
   

    void Start(){
        // Configure sliders to only allow whole number values
        {level1Slider.wholeNumbers = true;
        level2Slider.wholeNumbers = true;
        level3Slider.wholeNumbers = true;
        level4Slider.wholeNumbers = true;
        level5Slider.wholeNumbers = true;}

        {level1Slider.onValueChanged.AddListener(delegate { AValueIsChanged(level1Slider, level1Counter); });
        level2Slider.onValueChanged.AddListener(delegate { AValueIsChanged(level2Slider, level2Counter); });
        level3Slider.onValueChanged.AddListener(delegate { AValueIsChanged(level3Slider, level3Counter); });
        level4Slider.onValueChanged.AddListener(delegate { AValueIsChanged(level4Slider, level4Counter); });
        level5Slider.onValueChanged.AddListener(delegate { AValueIsChanged(level5Slider, level5Counter); });
        }
    }

    public void SetTroopsLimits(int[] TroopsData) 
{
    // This will be called by UI Manager
    troopsData = TroopsData;

    SetUIElement(level1Slider, level1Counter, troopsData[0]);
    SetUIElement(level2Slider, level2Counter, troopsData[1]);
    SetUIElement(level3Slider, level3Counter, troopsData[2]);
    SetUIElement(level4Slider, level4Counter, troopsData[3]);
    SetUIElement(level5Slider, level5Counter, troopsData[4]);
}

// Helper function to set slider and text visibility
private void SetUIElement(Slider slider, TextMeshProUGUI counterText, int value)
{
    bool isActive = value > 0;
    slider.gameObject.SetActive(isActive); // Hide if value is 0
    counterText.gameObject.SetActive(isActive);

    if (isActive)
    {
        slider.maxValue = value; // Set max value only if it's visible
        counterText.text = 0.ToString(); // Update counter text
        slider.value=0;
    }
}


    
    void AValueIsChanged(Slider slider, TextMeshProUGUI counter){
        //this will be triggered by all five inputs
        // Calculate the current total sum of all slider values
        totalSliderValue = GetCurrentTotal();

        // If the total exceeds TroopsCapacity, adjust the last changed slider
        if (totalSliderValue > TroopsCapacity)
        {
            // Identify the last changed slider
            Slider lastChangedSlider = UnityEngine.EventSystems.EventSystem
            .current.currentSelectedGameObject?.GetComponent<Slider>();

            if (lastChangedSlider != null)
            {
                // Reduce the last changed slider value to stay within the limit
                lastChangedSlider.value -= (totalSliderValue - TroopsCapacity);
            }
        }
        else
        {
            // Update local variables for each slider value
            level1CounterLM = Mathf.FloorToInt(level1Slider.value);
            level2CounterLM = Mathf.FloorToInt(level2Slider.value);
            level3CounterLM = Mathf.FloorToInt(level3Slider.value);
            level4CounterLM = Mathf.FloorToInt(level4Slider.value);
            level5CounterLM = Mathf.FloorToInt(level5Slider.value);
        }

        // Update the corresponding TextMeshProUGUI with the new slider value
        level1Counter.text = level1CounterLM.ToString();
        level2Counter.text = level2CounterLM.ToString();
        level3Counter.text = level3CounterLM.ToString();
        level4Counter.text = level4CounterLM.ToString();
        level5Counter.text = level5CounterLM.ToString();
    }

     void UpdateAllValues(){
    // Manually trigger the value update for all sliders
    AValueIsChanged(level1Slider, level1Counter);
    AValueIsChanged(level2Slider, level2Counter);
    AValueIsChanged(level3Slider, level3Counter);
    AValueIsChanged(level4Slider, level4Counter);
    AValueIsChanged(level5Slider, level5Counter);
}
    private int GetCurrentTotal()
    {//this will return total number of slider value.        
        currentTotal=Mathf.FloorToInt(level1Slider.value) + Mathf.FloorToInt(level2Slider.value) +
               Mathf.FloorToInt(level3Slider.value) + Mathf.FloorToInt(level4Slider.value) +
               Mathf.FloorToInt(level5Slider.value);

        // Debug.Log("currnt"+currentTotal);
        return currentTotal;
    }

    public void RefreshAllValues(){
        //this will be triggered by cancel ui button        
        level1Slider.maxValue =  0;
        level2Slider.maxValue = 0;
        level3Slider.maxValue = 0;
        level4Slider.maxValue = 0;
        level5Slider.maxValue = 0;   
        level1CounterLM=0;
        level2CounterLM=0;
        level3CounterLM=0;
        level4CounterLM=0;
        level5CounterLM=0;    
    }   


    public int[] ReturnTroopsData()
    {
        UpdateAllValues();
        //this will be returned to ui manager
        int[] troopsData = new int[5]; // Array to hold the number of troops per level

        troopsData[0] = level1CounterLM;  // Troops for Level 1
        troopsData[1] = level2CounterLM;  // Troops for Level 2
        troopsData[2] = level3CounterLM;  // Troops for Level 3
        troopsData[3] = level4CounterLM;  // Troops for Level 4
        troopsData[4] = level5CounterLM;  // Troops for Level 5
        // Debug.Log(troopsData[0]+","+troopsData[1]+","+troopsData[2]+","+troopsData[3]+","+troopsData[4]);
        // Debug.Log("total slider value"+totalSliderValue);
        return troopsData;  // Returns the array containing troops per level
    }
}


