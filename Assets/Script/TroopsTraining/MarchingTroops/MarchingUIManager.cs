using UnityEngine.UI; // For UI components
using TMPro; // For TextMeshPro components if using TextMeshPro
using UnityEngine;

public class MarchingUIManager : MonoBehaviour
{//responsible for ui and accepting position and passing it to tmm and selecting army
    // Update is called once per frame

    public Slider troopsSlider;
    public TMP_InputField troopsInputField; // For TextMeshPro InputField    

    private int NumberOfTroops;
    public LayerMask groundLayer; 
    [SerializeField] UnitSelectionManager selectionManager;
    private UnitSelector selectedObject;
    
    [SerializeField] private TroopsMarchManager marchManager;
    [SerializeField] private GameObject MarchingUIPanel;
    [SerializeField] private ArmyCount armyCount;

    void Start()
    {
        setLimitValues();

        // Initialize the input field with the slider's starting value
        troopsInputField.text = Mathf.RoundToInt(troopsSlider.value).ToString();
        NumberOfTroops = Mathf.RoundToInt(troopsSlider.value);

        // Add listeners to handle value changes
        troopsSlider.onValueChanged.AddListener(OnSliderValueChanged);
        troopsInputField.onEndEdit.AddListener(OnInputFieldValueChanged);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))//this will move
            {                
                //this will pass the to check if it is a army
                //then selected
                if(CheckArmySelected(hit)||IsGroundLayer(hit.collider.gameObject)){
                selectedObject=selectionManager.selectedObject;

                
                if(CheckArmySelected(hit)){//return true it clicked on army
                    if(selectedObject!=null){//null if previous
                    selectionManager.DeselectTheUnit();//deselecting previous unit
                    }
                    selectionManager.selectTheUnit(CheckArmySelected(hit));// pass the clicked unit prefab
                }
                 else if (selectedObject != null && IsGroundLayer(hit.collider.gameObject))
                {                    
                    marchManager.InitiateTheMarchProcess(selectedObject,hit.point);

                }  
                else if(IsGroundLayer(hit.collider.gameObject)){
                    setLimitValues();
                    MarchingUIPanel.SetActive(true);
                }            
                }
                else{
                    if( selectedObject != null){
                        // Deselect the currently selected army ,if other object is clicked
                        selectedObject.Highlight(false);
                        selectionManager.DeselectTheUnit();//deselecting previous unit
                    }
                }
            }
        }
    }
    private bool IsGroundLayer(GameObject obj)
    {
        return (groundLayer.value & (1 << obj.layer)) != 0;
    }
    UnitSelector CheckArmySelected(RaycastHit hit){      
        UnitSelector clickedObject = hit.collider.GetComponent<UnitSelector>();  
        return clickedObject;
    }

//slider handling

public void setLimitValues(){//++++ need to pass a value for max capacity
        //this will be triggered by start and on every time barrack is clicked.RN TrainingUIManager 56.
        // Set the slider's min and max values
        troopsSlider.minValue = 0;
        troopsSlider.maxValue = armyCount.ReturnSoldierCount(); //get the total troops present
        troopsSlider.value = troopsSlider.maxValue;
    }
    

    // When the slider value changes
    void OnSliderValueChanged(float value)
    {
        NumberOfTroops = Mathf.RoundToInt(value);
        troopsInputField.text = NumberOfTroops.ToString(); // Update the input field to reflect slider's value
    }

    // When the input field value changes
    void OnInputFieldValueChanged(string value)
    {
        int number;
        if (int.TryParse(value, out number))
        {
            // Ensure the input value is clamped within the slider's range
            number = Mathf.Clamp(number, (int)troopsSlider.minValue, (int)troopsSlider.maxValue);
            NumberOfTroops = number;
            troopsSlider.value = number; // Update the slider to reflect the input field's value
        }
        else
        {
            // Reset to the last valid value if the input is not a valid number
            troopsInputField.text = NumberOfTroops.ToString();
        }
    }

    // Method to get the selected value if needed elsewhere
    public int GetNoofTroopsToTrain()
    {
        //this will be called to get how many troops to train
        return NumberOfTroops;
    }
    
}
