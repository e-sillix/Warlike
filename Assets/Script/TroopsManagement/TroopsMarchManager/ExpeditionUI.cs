using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ExpeditionUI : MonoBehaviour
{
    [SerializeField] private GameObject InitialConfirmPanel,ConfirmPanel2;
    [SerializeField] private Button[] ArmyButton;
    [SerializeField] private GameObject[] ArmyButtonGO;

    [SerializeField] private GameObject CreateButton;

    [SerializeField] private TextMeshProUGUI[] armyId;
    
    public int ArmyLimit=5;
    private TroopsExpeditionManager troopsExpeditionManager;
    private TheUnit ChoosenUnit;
    private TheUnit[] Armys;
    void Start(){
        troopsExpeditionManager=GetComponent<TroopsExpeditionManager>();
        // Add listeners to each button
        

        // Army1Button.onClick.AddListener(()=>ExitingIsChoosen(1));
    }
    public void TriggerConfirmationUI(){
        //by expedition manager 
        // Debug.Log("Triggered");
        InitialConfirmPanel.SetActive(true);//the one will with tick and cross panel
    }
    public void Stage2ConfirmationUI(){//triggered by stage 1 confirmation ui
        //the one with exiting army or creating another army option
        ConfirmPanel2.SetActive(true);
        Armys=troopsExpeditionManager.GetAllThePresentUnits();
            int armyCount = Mathf.Min(Armys.Length, 5); // Ensure we handle only up to 5 armies
            if(armyCount>=ArmyLimit){
                CreateButton.SetActive(false);
            }
            else{
                CreateButton.SetActive(true);
            }
            for (int i = 0; i < armyCount; i++) {
                ArmyButtonGO[i].SetActive(true);              // Activate the button
                armyId[i].text = Armys[i].ArmyId.ToString();  // Set the army ID on the button

                int index = i; // Capture 'i' locally to prevent closure issue in the listener
                ArmyButton[i].onClick.AddListener(() => ArmyIsChosen(Armys[index])); // Assign the listener
            }

            // Optionally hide extra buttons if fewer than 5 armies
            for (int i = armyCount; i < ArmyButtonGO.Length; i++) {
                ArmyButtonGO[i].SetActive(false); // Hide buttons for unused army slots
            }      


    }
    void ArmyIsChosen(TheUnit ChoosedOne){
        ChoosenUnit=ChoosedOne;
        Debug.Log("clicked"+ChoosedOne.ArmyId);

        troopsExpeditionManager.ExistingArmyIsChoosen(ChoosenUnit);

        EndStageTriggered();
    }

    public void NewArmyChoosen(){//by new create option ui
        troopsExpeditionManager.NewArmyChoosenClicked();
        InitialConfirmPanel.SetActive(false);
        ConfirmPanel2.SetActive(false);
    }
   
    public void EndStageTriggered(){
        //this will be triggered by ui cancel buttons
        // refresh all ui;

        // SetTroopType("");
        // MarchingStage1UIPanel.SetActive(false);
        // MarchingStage2UIPanel.SetActive(false);
        // MarchingStage3UIPanel.SetActive(false);
        // MarchingStage4UIPanel.SetActive(false);
        // marchManager.EndStage();
        // target=null;
        InitialConfirmPanel.SetActive(false);
        ConfirmPanel2.SetActive(false);

        troopsExpeditionManager.EndStage();
    }
}
