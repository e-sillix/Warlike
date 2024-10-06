using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsExpeditionManager : MonoBehaviour
{
    private GameObject target;
    [SerializeField] private MarchManager marchManager;
    [SerializeField] private ActionManager actionManager;
    private TheUnit[] Army;
    [SerializeField] private GameObject SpawnPoint; 

    public void PotentialTargetForMarchClicked(GameObject Target,RaycastHit hit){//clickedObject
        //this will be called by global 
        target=Target;  //storing target
        
        //trigger march manager stage 1 
        marchManager.ATargetIsClick(Target,hit);
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
    public void ExistingArmyIsChoosen(TheUnit TheArmy){
        InitiateMarchProcess(TheArmy);
    }
    void InitiateMarchProcess(TheUnit TheArmy){
        marchManager.InitiateTheMarchProcess(TheArmy ,target);
    }
    public void TargetReached(TheUnit TheArmy){//called by TheUnit when unit reached.
        actionManager.PerformAction(TheArmy);
    }
    public void ActionDone(TheUnit TheArmy){//called by actionManager ,when the action is done.
        if(TheArmy.IsReturn){
            TheArmy.target=SpawnPoint;
            InitiateMarchProcess(TheArmy);
        }
    }
}
