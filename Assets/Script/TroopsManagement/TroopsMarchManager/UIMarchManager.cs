using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMarchManager : MonoBehaviour
{
    
    [SerializeField] private MarchManager marchManager;
    [SerializeField] private TroopsCountManager troopsCountManager;
    [SerializeField] private GameObject MarchingStage1UIPanel,MarchingStage2UIPanel,MarchingStage3UIPanel,
    MarchingStage4UIPanel;
    private int[] troopsNumber=new int[5];
    private int[] troopsToMarch=new int[5];
    private Vector3 position;   
    // References to the UI buttons
    [SerializeField] private Button cavalryButton;
    [SerializeField] private Button infantryButton;
    [SerializeField] private Button archerButton;
    [SerializeField] private Button mageButton;
    [SerializeField] private MarchSlider marchSlider;

    private string selectedTroopType;


    void Start()
    {
        // Add listeners to each button
        cavalryButton.onClick.AddListener(() => SetTroopType("Cavalry"));
        infantryButton.onClick.AddListener(() => SetTroopType("Infantry"));
        archerButton.onClick.AddListener(() => SetTroopType("Archer"));
        mageButton.onClick.AddListener(() => SetTroopType("Mage"));
    }
     // Function to set the troop type
    private void SetTroopType(string troopType)
    {
        selectedTroopType = troopType;
        // Debug.Log("Selected Troop Type: " + selectedTroopType);
        // You can now use the 'selectedTroopType' string for further logic
        if(troopType!=""){
        MarchStage3Trigger();
        }
    } 
   
    public void TriggerForMarchStage1(Vector3 Position){
        //starting tick or icon one
        //automatically by stage 1 in marchmanger
        position=Position;
        // Debug.Log(position);
        //ui triggering
        MarchingStage1UIPanel.SetActive(true);

        //spawn a ui element on position ,like a anchor
    }

//stage 2
    public void MarchStage2Trigger(){    
        //triggered by march here button,the yes one 
        MarchingStage1UIPanel.SetActive(false);
        MarchingStage2UIPanel.SetActive(true);

        // get types of troops to display dynamically
        
    }
//stage 3
    private void MarchStage3Trigger(){
        //triggered by march button in stage 2 panel or here indirectly by settroopstype
        // get data for that troops type number,set it to max slider value all five
        MarchingStage2UIPanel.SetActive(false);
        troopsNumber=troopsCountManager.GetTroopsCount(selectedTroopType);//set the max limit of troops level
        //according to counting
        // Debug.Log("in ui");
        // for (int i =0;i<5;i++){
        //     Debug.Log(troopsNumber[i]);
        // }
        //slider one
        MarchingStage3UIPanel.SetActive(true);
        //passing the troops data to ui slider
        marchSlider.SetTroopsLimits(troopsNumber);
    }
//stage 4
    public void MarchStage4Trigger(){
        //triggered by slider ui march button
        troopsToMarch=marchSlider.ReturnTroopsData();
        //  Debug.Log("in ui");
        // for (int i =0;i<5;i++){
        //     Debug.Log(troopsToMarch[i]);
        // }
        marchManager.StartMarching(selectedTroopType,troopsToMarch);//pass the troops numbers with levels
        EndStageTriggered();

        // triggersuccessmessage();
    }


//end stage
    public void EndStageTriggered(){
        //this will be triggered by ui cancel buttons
        // refresh all ui;
        // marchAllowed =true;
        SetTroopType("");
        MarchingStage1UIPanel.SetActive(false);
        MarchingStage2UIPanel.SetActive(false);
        MarchingStage3UIPanel.SetActive(false);
        MarchingStage4UIPanel.SetActive(false);
    }
}
