using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsExpeditionManager : MonoBehaviour
{
    private GameObject target,pointer;
    // [SerializeField] private MarchManager marchManager;
    [SerializeField] private ActionManager actionManager;
    [SerializeField] private GlobalUIManager globalUIManager;
    [SerializeField] private NewArmyManger newArmyManger;
    [SerializeField] private GameObject MarchingPointer;
    [SerializeField] private MiningManager miningManager;
    private TheUnit[] Army;
    [SerializeField] private GameObject SpawnPoint; 
    private Vector3 position;
    private ExpeditionUI expeditionUI;
    private TheUnit ChoosenUnit;
    private TheUnit[] theUnits;
    
    void Start(){
        expeditionUI=GetComponent<ExpeditionUI>();
    }

    public void PotentialTargetForMarchClicked(GameObject Target,RaycastHit hit,GameObject Pointer){//
    // clickedObject
    //this will be called by spawnedPointer.
        //this will be called by global 
        target=Target;  //storing target
        position=hit.point;
        pointer=Pointer;
        //trigger march manager stage 1 -----

        // marchManager.ATargetIsClick(Target,hit);---------
        // Debug.Log("RE");

        expeditionUI.Stage2ConfirmationUI();
        // globalUIManager.UIisOpened();
        
    }
    public void CombatTargetClicked(GameObject Target){
        //by creepUI when clicked march button
        target=Target;
        expeditionUI.Stage2ConfirmationUI();
        //   globalUIManager.UIisOpened();
    }
    public void MineTargetClicked(GameObject Target){
        //by creepUI when clicked march button
        target=Target;
        expeditionUI.Stage2ConfirmationUI();
        //   globalUIManager.UIisOpened();
    }
    public TheUnit[] GetAllThePresentUnits(){//this will be called by ui indirectly for showing info of 
    //current present army
        //by ui stage1 confirmation
        theUnits = FindObjectsOfType<TheUnit>();
        // for(int i=0;i<theUnits.Length;i++){
        //     Debug.Log(theUnits[i].ArmyId);
        // }
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
        //By expedition ui 
        ArmyIsChoosed(choosedOne);
    }
    void ArmyIsChoosed(TheUnit choosedOne){
        ChoosenUnit=choosedOne;
        //march it.
        march();
    }
    public void MarchUsingDrag(TheUnit SelecedUnit,GameObject Target,RaycastHit hit){//by TouchMarching
        if(Target.GetComponentInParent<TheCreep>()){
            target=Target.GetComponentInParent<TheCreep>().gameObject;
        }else if(Target.GetComponentInParent<TheMine>()){
            target=Target.GetComponentInParent<TheMine>().gameObject;
        }else if(Target.GetComponentInParent<BossArmy>()){
            target=Target.GetComponentInParent<BossArmy>().gameObject;}
        else if(Target.GetComponentInParent<Boss>()){
            target=Target.GetComponentInParent<Boss>().gameObject;}
            else {
                target =Target;//for the ground
            }
        ChoosenUnit=SelecedUnit;
        position=hit.point;
        // target=Target;
        march();
    }
    void march(){
        // Debug.Log(ChoosenUnit.ArmyId);
        if(target.GetComponent<TheCreep>()){
            // ChoosenUnit.SetTroopsTarget(position,target,SpawnPoint,ToMarchPointer);
            Debug.Log("target is creep");
            ChoosenUnit.SetTroopsTargetCombat(target,SpawnPoint);     
            target.GetComponent<CreepUI>().DeSelectCreep();  
            EndStage();
            return;
            }
            else if(target.GetComponent<BossArmy>()){
                ChoosenUnit.SetTroopsTargetCombat(target,SpawnPoint);     
                target.GetComponent<BossArmyUI>().DeSelectArmy();  
                EndStage();
                return;
            }
            else if(target.GetComponent<Boss>()){
                ChoosenUnit.SetTroopsTargetCombat(target,SpawnPoint);     
                target.GetComponent<BossUI>().DeSelectBoss();  
                EndStage();
                return;
            }
            else if(target.GetComponent<TowerCombat>()){
                ChoosenUnit.SetTroopsTargetCombat(target,SpawnPoint);     
                // target.GetComponent<TowerCombat>().DeSelectBoss();  
                EndStage();
                return;
            }
        else if(target.GetComponent<TheMine>()){
            // ChoosenUnit.SetTroopsTarget(position,target,SpawnPoint,ToMarchPointer);
            Debug.Log("target is Mine");
            ChoosenUnit.SetTroopsTargetMine(target,SpawnPoint);     
            target.GetComponent<MineUI>().DeSelectMine();  
            EndStage();
            return;
        }
        Debug.Log("target is ground.");
        GameObject ToMarchPointer=Instantiate(MarchingPointer,position,Quaternion.identity);
        ChoosenUnit.SetTroopsTarget(position,target,SpawnPoint,ToMarchPointer);       
        EndStage();
        if(pointer)
        Destroy(pointer);
    }

    public void EndStage(){
        // globalUIManager.RefreshPermission();
        // globalUIManager.UIisClosed();

    }

    public void MarchDone(GameObject TheArmyInst){//called by TheUnit when unit reached.
        actionManager.PerformAction(TheArmyInst);
    }   
    public void ReturnTroopsToBase(GameObject Army,string selectedTroopType,int [] troopsToMarch){
        //by unit when target reached with null
        newArmyManger.ReturnTroops(selectedTroopType,troopsToMarch);
        miningManager.CollectMinedResources(Army);
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
