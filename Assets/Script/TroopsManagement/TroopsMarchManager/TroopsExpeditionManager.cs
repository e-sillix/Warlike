using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsExpeditionManager : MonoBehaviour
{
    private GameObject target;
    // [SerializeField] private MarchManager marchManager;
    [SerializeField] private ActionManager actionManager;
    [SerializeField] private GlobalUIManager globalUIManager;
    [SerializeField] private NewArmyManger newArmyManger;
    private TheUnit[] Army;
    [SerializeField] private GameObject SpawnPoint; 
    private Vector3 position;
    private ExpeditionUI expeditionUI;
    private TheUnit ChoosenUnit;
    private TheUnit[] theUnits;
    void Start(){
        expeditionUI=GetComponent<ExpeditionUI>();
    }

    public void PotentialTargetForMarchClicked(GameObject Target,RaycastHit hit){//clickedObject
        //this will be called by global 
        target=Target;  //storing target
        position=hit.point;
        //trigger march manager stage 1 -----

        // marchManager.ATargetIsClick(Target,hit);---------

        expeditionUI.TriggerConfirmationUI();
        
    }

    public TheUnit[] GetAllThePresentUnits(){//this will be called by ui indirectly for showing info of 
    //current present army
        //by ui stage1 confirmation
        theUnits = FindObjectsOfType<TheUnit>();
        for(int i=0;i<theUnits.Length;i++){
            Debug.Log(theUnits[i].ArmyId);
        }
        return theUnits;
    }
    public void NewArmyChoosenClicked(){//by ui manager
        newArmyManger.InitiateNewArmy();
    }
    public void ArmyCreationDone(TheUnit newArmy){//called by newarmymanager after creation is done
        ChoosenUnit=newArmy;//this is for storing 

        ArmyIsChoosed(ChoosenUnit);        
    }
    public void ExistingArmyIsChoosen(TheUnit choosedOne){
        ArmyIsChoosed(choosedOne);
    }
    public void ArmyIsChoosed(TheUnit choosedOne){
        ChoosenUnit=choosedOne;

        //march it.
        march();
    }
    void march(){
        // Debug.Log(ChoosenUnit.ArmyId);
        ChoosenUnit.SetTroopsTarget(position,target);

    }

    public void EndStage(){
        globalUIManager.RefreshPermission();
    }

    public void MarchDone(GameObject TheArmyInst){//called by TheUnit when unit reached.
        actionManager.PerformAction(TheArmyInst);
    }








    
    public void StoreNewArmy(TheUnit TheArmy){
        //called by marchmanager when created new army
        for(int i=0; i<5;i++){
            if(!Army[i]){
                Army[i]=TheArmy;
                return;
            }
        }
        // InitiateMarchProcess(TheArmy);
    }
    // public void ExistingArmyIsChoosen(TheUnit TheArmy){
    //     InitiateMarchProcess(TheArmy);
    // }
    void InitiateMarchProcess(TheUnit TheArmy){
        // marchManager.InitiateTheMarchProcess(TheArmy ,target);
    }
    
    public void ActionDone(TheUnit TheArmy){//called by actionManager ,when the action is done.
        if(TheArmy.IsReturn){
            TheArmy.target=SpawnPoint;
            InitiateMarchProcess(TheArmy);
        }
    }
}
