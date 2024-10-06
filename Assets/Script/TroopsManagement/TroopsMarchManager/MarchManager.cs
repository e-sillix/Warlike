using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchManager : MonoBehaviour
{
    public bool marchAllowed=true;//this will set up global ui and local
    [SerializeField]private LayerMask groundLayer,innerKingdomLayer,enemyLayer,mineLayer; 
    [SerializeField] private UIMarchManager uIMarchManager;
    [SerializeField] private GlobalUIManager globalUIManager;
    [SerializeField] private TroopsCountManager troopsCountManager;
    [SerializeField] private TroopsExpeditionManager troopsExpeditionManager;
    private Vector3 position;  
    private GameObject clickedObject;
    private int[] troopsData;
    [SerializeField] private GameObject TheUnitPrefab;
    [SerializeField] private GameObject Spawnpoint;
    private GameObject TheArmyGO;
    private TheUnit TheArmy;
    private string targetType;
    private GameObject target;
    private bool troopsAction;

   
 //stage 1
    public void ATargetIsClick(GameObject Target,RaycastHit hit){
        //called by troopsExpedition
        if(IsGroundLayer(Target)){
            Debug.Log("Ground is Clicked.");
            GroundIsClicked(Target,hit);
        }
        else if(IsEnemyLayer(Target)){
            Debug.Log("Enemey is Clicked.");
            MarchTargetClicked(Target,"Enemy");
        }
        else if(IsMineLayer(Target)){
            Debug.Log("Mine is Clicked.");
            MarchTargetClicked(Target,"Mine");
        }
    }
    private bool IsGroundLayer(GameObject obj)
    {
        return (groundLayer.value & (1 << obj.layer)) != 0;
    }
    private bool IsEnemyLayer(GameObject obj)
    {
        return (enemyLayer.value & (1 << obj.layer)) != 0;
    }
    private bool IsMineLayer(GameObject obj)
    {
        return (mineLayer.value & (1 << obj.layer)) != 0;
    }
    void GroundIsClicked(GameObject ClickedObject,RaycastHit hit){
        //triggered by global ui manager
        if(!IsInnerKingdomLayer(ClickedObject)){
            //this will be "not innerkingdom collider" condition                        
            position=hit.point;
            // marchAllowed=false;
            uIMarchManager.TriggerForMarchStage1(position,null,"");                            
            }
            else{
                Debug.Log(" not on ground or on innerkingdom");
                }    
            }        
    void MarchTargetClicked(GameObject Target,string TargetType){
        //triggered by global ui manager
        targetType=TargetType;
        target=Target;
        uIMarchManager.TriggerForMarchStage1(position,target,TargetType);
        troopsAction=true;
    }
    


    private bool IsInnerKingdomLayer(GameObject obj)
    {
        return (innerKingdomLayer.value & (1 << obj.layer)) != 0;
    }

//stage 4
    public void StartMarching(string TroopsType,int[] TroopsData){
        troopsData=TroopsData;
        //spawn army with the given stats
        //pass the position to army
        TheArmyGO=Instantiate(TheUnitPrefab,Spawnpoint.transform.position, Spawnpoint.transform.rotation);
     
        troopsCountManager.WithDrawTroops(TroopsType,TroopsData);
    //  +++++++++
    //  troopsCounter.WithDrawingTroopsFromBase(troopsCount);

        TheArmy=TheArmyGO.GetComponent<TheUnit>();
        TheArmy.SetTroopsData(TroopsType,troopsData);
        // if(troopsAction){
        //     TheArmy.SetTroopsTarget(target);   
        // }
        // else{

        InitiateNewTheMarchProcess(TheArmy,position,target);     
        troopsExpeditionManager.StoreNewArmy(TheArmy);
    }    
   
    public void InitiateNewTheMarchProcess(TheUnit TheArmy,Vector3 position,GameObject Target){
     //this will be triggered by MUIM if there is selected
     //it will be triggered by initiatenewmarchprocess  
    //  TheArmy.SetTargetPosition(position);
        TheArmy.SetTroopsTarget(position,Target);
   }

    //end stage
    public void EndStage(){
        troopsAction=false;
        globalUIManager.RefreshPermission();
    }
}
