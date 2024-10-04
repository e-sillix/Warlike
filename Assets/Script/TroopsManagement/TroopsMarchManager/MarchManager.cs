using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchManager : MonoBehaviour
{
    public bool marchAllowed=true;//this will set up global ui and local
    [SerializeField]private LayerMask groundLayer,innerKingdomLayer; 
    [SerializeField] private UIMarchManager uIMarchManager;
    [SerializeField] private GlobalUIManager globalUIManager;
    private Vector3 position;  
    private GameObject clickedObject;
    private int[] troopsData;
    [SerializeField] private GameObject TheUnitPrefab;
    [SerializeField] private GameObject Spawnpoint;
    private GameObject TheArmyGO;
    private TheUnit TheArmy;

    //stage 1
    // void Update(){
    //     if(marchAllowed){
    //         if (Input.GetMouseButtonDown(0)){ // Detect left mouse button click
    //             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //             RaycastHit hit;

    //             // Perform the raycast
    //             if (Physics.Raycast(ray, out hit))//this will move
    //             {  
    //                 clickedObject=hit.collider.gameObject;
    //                 // Debug.Log(clickedObject.layer);
    //                 if(IsGroundLayer(clickedObject)&&!IsInnerKingdomLayer(clickedObject)){
    //                     //this will be "not innerkingdom collider" condition                        
    //                         position=hit.point;
    //                         // marchAllowed=false;
    //                         uIMarchManager.TriggerForMarchStage1(position);                            
    //                 }
    //                 else{
    //                     Debug.Log(" not on ground or on innerkingdom");
    //                 }
                    
    //             }        
    //         }
    //     }
    //     else{
    //         Debug.Log("March Not Allowed ,manager");
    //     }
    // }
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

     //cut the soldier count from the base
    //  +++++++++
    //  troopsCounter.WithDrawingTroopsFromBase(troopsCount);

        TheArmy=TheArmyGO.GetComponent<TheUnit>();
        TheArmy.SetTroopsData(TroopsType,troopsData);
        InitiateTheMarchProcess(TheArmy,position);
     
    }    
   
    public void InitiateTheMarchProcess(TheUnit TheArmy,Vector3 position){
     //this will be triggered by MUIM if there is selected
     //it will be triggered by initiatenewmarchprocess  
     TheArmy.SetTargetPosition(position);
   }

    //end stage
    public void EndStage(){
        globalUIManager.RefreshPermission();
    }
}
