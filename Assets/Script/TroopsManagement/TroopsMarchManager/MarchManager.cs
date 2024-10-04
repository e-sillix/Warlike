using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchManager : MonoBehaviour
{
    public bool marchAllowed=true;//this will set up global ui and local
    [SerializeField]private LayerMask groundLayer,innerKingdomLayer; 
    [SerializeField] private UIMarchManager uIMarchManager;
    [SerializeField] private GlobalUIManager globalUIManager;
    [SerializeField] private TroopsCountManager troopsCountManager;
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
    public void GroundIsClicked(GameObject ClickedObject,RaycastHit hit){
        //triggered by global ui manager
        if(!IsInnerKingdomLayer(ClickedObject)){
            //this will be "not innerkingdom collider" condition                        
            position=hit.point;
            // marchAllowed=false;
            uIMarchManager.TriggerForMarchStage1(position);                            
            }
            else{
                Debug.Log(" not on ground or on innerkingdom");
                }    
            }        
    public void MarchTargetClicked(GameObject Target,string TargetType){
        targetType=TargetType;
        target=Target;
        
        Debug.Log(targetType);
        Debug.Log(target.transform.position);
        uIMarchManager.TriggerForMarchStage1(position);
        troopsAction=true;
    }


    private bool IsGroundLayer(GameObject obj)
    {
        return (groundLayer.value & (1 << obj.layer)) != 0;
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
        if(troopsAction){
            TheArmy.SetTroopsTarget(target);   
        }
        else{

        InitiateTheMarchProcess(TheArmy,position);
        }
     
    }    
   
    public void InitiateTheMarchProcess(TheUnit TheArmy,Vector3 position){
     //this will be triggered by MUIM if there is selected
     //it will be triggered by initiatenewmarchprocess  
     TheArmy.SetTargetPosition(position);
   }

    //end stage
    public void EndStage(){
        troopsAction=false;
        globalUIManager.RefreshPermission();
    }
}
