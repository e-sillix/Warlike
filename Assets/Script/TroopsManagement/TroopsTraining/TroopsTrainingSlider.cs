using UnityEngine.UI; // For UI components
using TMPro; // For TextMeshPro components if using TextMeshPro
using UnityEngine;

public class TroopsTrainingSlider : MonoBehaviour
{
    [SerializeField] private Slider level1Slider,level2Slider,level3Slider,level4Slider,level5Slider;
    [SerializeField] private TextMeshProUGUI level1Counter,level2Counter,level3Counter
    ,level4Counter,level5Counter;
    // this one will be removed .

    // public TMP_InputField level1Field,level2Field,level3Field,level4Field,level5Field; 
    // For TextMeshPro InputField   

    private int BarrackCapacity ; // Max unit capacity
    private int currentTotal;           // Sum of all slider values

    void Start(){
        // Configure sliders to only allow whole number values
        {level1Slider.wholeNumbers = true;
        level2Slider.wholeNumbers = true;
        level3Slider.wholeNumbers = true;
        level4Slider.wholeNumbers = true;
        level5Slider.wholeNumbers = true;}

        //this will be passed to someother function++++++++++++
        // Set initial max value for each slider
        // {level1Slider.maxValue = BarrackCapacity;
        // level2Slider.maxValue = BarrackCapacity;
        // level3Slider.maxValue = BarrackCapacity;
        // level4Slider.maxValue = BarrackCapacity;
        // level5Slider.maxValue = BarrackCapacity;
        // }
        // Add listeners to each slider to detect value changes
        {level1Slider.onValueChanged.AddListener(delegate { AValueIsChanged(level1Slider, level1Counter); });
        level2Slider.onValueChanged.AddListener(delegate { AValueIsChanged(level2Slider, level2Counter); });
        level3Slider.onValueChanged.AddListener(delegate { AValueIsChanged(level3Slider, level3Counter); });
        level4Slider.onValueChanged.AddListener(delegate { AValueIsChanged(level4Slider, level4Counter); });
        level5Slider.onValueChanged.AddListener(delegate { AValueIsChanged(level5Slider, level5Counter); });
        }
    }

    public void SetMaxBarrackCapacityForUI(int barrackCapacity){
        //this will be called by ui manager
        BarrackCapacity=barrackCapacity;
        level1Slider.maxValue =  BarrackCapacity;
        level2Slider.maxValue = BarrackCapacity;
        level3Slider.maxValue = BarrackCapacity;
        level4Slider.maxValue = BarrackCapacity;
        level5Slider.maxValue = BarrackCapacity;               
    }
    
    void AValueIsChanged(Slider slider, TextMeshProUGUI counter){
        //this will be triggered by all five inputs
        // Update the corresponding TextMeshProUGUI to show the slider value
       // Calculate the current sum of all slider values
        int totalSliderValue = GetCurrentTotal();
        // If the total exceeds BarrackCapacity, adjust the last changed slider
        if (totalSliderValue > BarrackCapacity)
        {
            // Identify the slider that needs to be reduced
            Slider lastChangedSlider = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Slider>();

            // Reduce the last changed slider so the total stays within the limit
            if(lastChangedSlider){
                //this will be null when chosing different barrack.
            lastChangedSlider.value -= (totalSliderValue - BarrackCapacity);
            }
        }

        // Update the counter values for each slider
        level1Counter.text = Mathf.RoundToInt(level1Slider.value).ToString();
        level2Counter.text = Mathf.RoundToInt(level2Slider.value).ToString();
        level3Counter.text = Mathf.RoundToInt(level3Slider.value).ToString();
        level4Counter.text = Mathf.RoundToInt(level4Slider.value).ToString();
        level5Counter.text = Mathf.RoundToInt(level5Slider.value).ToString();
    }

    private int GetCurrentTotal()
    {//this will return total number of slider value.
        return Mathf.RoundToInt(level1Slider.value) + Mathf.RoundToInt(level2Slider.value) +
               Mathf.RoundToInt(level3Slider.value) + Mathf.RoundToInt(level4Slider.value) +
               Mathf.RoundToInt(level5Slider.value);
    }

    public void RefreshAllValues(){
        //this will be triggered by cancel ui button
        BarrackCapacity=0;
        level1Slider.maxValue =  BarrackCapacity;
        level2Slider.maxValue = BarrackCapacity;
        level3Slider.maxValue = BarrackCapacity;
        level4Slider.maxValue = BarrackCapacity;
        level5Slider.maxValue = BarrackCapacity;       
    }   


    public int[] ReturnTroopsData()
    {
        //this will be returned to ui manager
        int[] troopsData = new int[5]; // Array to hold the number of troops per level

        troopsData[0] = Mathf.RoundToInt(level1Slider.value);  // Troops for Level 1
        troopsData[1] = Mathf.RoundToInt(level2Slider.value);  // Troops for Level 2
        troopsData[2] = Mathf.RoundToInt(level3Slider.value);  // Troops for Level 3
        troopsData[3] = Mathf.RoundToInt(level4Slider.value);  // Troops for Level 4
        troopsData[4] = Mathf.RoundToInt(level5Slider.value);  // Troops for Level 5

        return troopsData;  // Returns the array containing troops per level
    }
}
