using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class NewArmyManger : MonoBehaviour
{
    [SerializeField] private GameObject newArmyStage1Panel,Stage2ArmyInitiationPanel;
    [SerializeField] private Button cavalryButton, infantryButton,archerButton,mageButton;
    [SerializeField] private GameObject TheUnitPrefab;
    [SerializeField] private GameObject Spawnpoint;

    [SerializeField] private TroopsCountManager troopsCountManager;
    [SerializeField]private MessageManager messageManager;

    private int[] troopsNumber=new int[5];
    private int[] troopsToMarch=new int[5];
    [SerializeField] private MarchSlider marchSlider;

    private string selectedTroopType;
    private TheUnit newArmy;
    private GameObject newArmyGO;
    [SerializeField] private TroopsExpeditionManager troopsExpeditionManager;
    [SerializeField] private TextMeshProUGUI TroopsTypeUI,TroopsAction;

     void Start()
    {
        // Add listeners to each button
        cavalryButton.onClick.AddListener(() => SetTroopType("Cavalry"));
        infantryButton.onClick.AddListener(() => SetTroopType("Infantry"));
        archerButton.onClick.AddListener(() => SetTroopType("Archer"));
        mageButton.onClick.AddListener(() => SetTroopType("Mage"));
    }
    public void InitiateNewArmy(){
        newArmyStage1Panel.SetActive(true);//choosing troopstype
    }

    private void SetTroopType(string troopType)
    {
        selectedTroopType = troopType;
        // You can now use the 'selectedTroopType' string for further logic
        if(troopType!=""){
        Stage2ArmyInitiation();
        }
    } 
    void Stage2ArmyInitiation(){
        newArmyStage1Panel.SetActive(false);

        TroopsTypeUI.text=selectedTroopType.ToString();

        troopsNumber=troopsCountManager.GetTroopsCount(selectedTroopType);//set the max limit of troops level
        //slider one
        Stage2ArmyInitiationPanel.SetActive(true);

        
       
        //passing the troops data to ui slider
        marchSlider.SetTroopsLimits(troopsNumber);
    }

    public void MarchIsClicked(){//this will be called by ui march button
        troopsToMarch=marchSlider.ReturnTroopsData();
        int totalNumberOfTroops=0;
        for(int i=0;i<troopsToMarch.Length;i++){
            totalNumberOfTroops+=troopsToMarch[i];
        }
        if(totalNumberOfTroops>0){

        
        newArmyGO=Instantiate(TheUnitPrefab,Spawnpoint.transform.position, 
        Spawnpoint.transform.rotation);
     
        troopsCountManager.WithDrawTroops(selectedTroopType,troopsToMarch);
    //  +++++++++
    //  troopsCounter.WithDrawingTroopsFromBase(troopsCount);

        newArmy=newArmyGO.GetComponent<TheUnit>();
        newArmy.SetTroopsData(selectedTroopType,troopsToMarch);
        troopsExpeditionManager.ArmyCreationDone(newArmy);
        }
        else{
            messageManager.DisplayZeroTroopsMarch();
            // Debug.Log("March has 0 Troops!!");
        }
        EndStageUI();
    }
    public void EndStageUI(){
        //called by ui cancel of new army stage.
        newArmyStage1Panel.SetActive(false);
        Stage2ArmyInitiationPanel.SetActive(false);
    }



    //returning troops
    public void ReturnTroops(string selectedTroopType,int [] troopsToMarch){
        troopsCountManager.ReturnTroops(selectedTroopType,troopsToMarch);
    }
}
