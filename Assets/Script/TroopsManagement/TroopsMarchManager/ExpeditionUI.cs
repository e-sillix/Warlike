using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ExpeditionUI : MonoBehaviour
{
    [SerializeField] private GameObject InitialConfirmPanel,ConfirmPanel2;
    [SerializeField] private Button Army1Button,Army2Button,Army3Button,Army4Button,Army5Button;
    [SerializeField] private TextMeshProUGUI army1Id,army2Id,army3Id,army4Id,army5Id;
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
        Debug.Log("Triggered");
        InitialConfirmPanel.SetActive(true);//the one will with tick and cross panel
    }
    public void Stage2ConfirmationUI(){//triggered by stage 1 confirmation ui
        //the one with exiting army or creating another army option
        ConfirmPanel2.SetActive(true);
        Armys=troopsExpeditionManager.GetAllThePresentUnits();
        
        if(Armys[0]){
        army1Id.text=Armys[0].ArmyId.ToString();
        Army1Button.onClick.AddListener(()=>ArmyIsChosen(Armys[0]));
        }

        if(Armys[1]){
        army2Id.text=Armys[1].ArmyId.ToString();
        Army2Button.onClick.AddListener(()=>ArmyIsChosen(Armys[1]));
        }

        if(Armys[2]){
        army3Id.text=Armys[2].ArmyId.ToString();
        Army3Button.onClick.AddListener(()=>ArmyIsChosen(Armys[2]));
        }
        
        if(Armys[3]){
        army4Id.text=Armys[3].ArmyId.ToString();
        Army4Button.onClick.AddListener(()=>ArmyIsChosen(Armys[3]));
        }
        
        if(Armys[4]){
        army5Id.text=Armys[4].ArmyId.ToString();
        Army5Button.onClick.AddListener(()=>ArmyIsChosen(Armys[4]));
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
        EndStageTriggered();
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
