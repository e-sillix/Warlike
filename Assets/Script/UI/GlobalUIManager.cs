using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GlobalUIManager : MonoBehaviour
{
    [SerializeField]private LayerMask groundLayer,enemyLayer,mineLayer;
    [SerializeField] private GameObject MarchPointer;

    private bool permissionForUI=true,IsUIOpen=false; //this will be falsed by other cancel managers only
    private GameObject clickedObject;
    [SerializeField] private TroopsTrainingManager troopsTrainingManager;
    [SerializeField] private TroopsExpeditionManager troopsExpeditionManager;
    [SerializeField] private TroopsUI troopsUI;
    private GameObject lastClicked,currentClicked;
    private GameObject spawnedPointer;
    // private bool MarchTargetClicked;
    void Update(){                   
                     
        // if(permissionForUI){        
        if (Input.GetMouseButtonDown(0)){ // Detect left mouse button click
         if (EventSystem.current.IsPointerOverGameObject())
            {
                 if(spawnedPointer){
                        Destroy(spawnedPointer);
                    }
                return;
                
            }  
            // if(IsUIOpen){
            //     //this is triggered when clicking outside of the UI.
            //     //close the UI

            //     //return.
            // }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))//this will move
                {        
                    // Prevent interaction if clicking on UI
                    clickedObject=hit.collider.gameObject;
                    currentClicked=clickedObject;
                     if(spawnedPointer){
                        Destroy(spawnedPointer);
                    }
                    ClickAnalysis(clickedObject,hit);
                }
            else{
                Debug.Log("Hitted nothing");
            }
            if( lastClicked!=currentClicked){
                // permissionForUI=true;
                Debug.Log("Different building clicked");
                if (lastClicked != null) {
                    BuildingInstance instance = lastClicked.GetComponentInParent<BuildingInstance>();
                    if (instance != null) {
                        Debug.Log("previous building has BuildingInstance");
                        instance.DisableUI();
                    } else {
                        Debug.Log("No BuildingInstance found on the last clicked object.");
                    }
                }    
                // else if(MarchTargetClicked){
                //     troopsExpeditionManager.CancelMarchUI();
                // }
            }
            lastClicked=currentClicked;
        }
    // }}

    void ClickAnalysis(GameObject ClickedObject,RaycastHit hit){

        if(IsGroundLayer(ClickedObject)||IsEnemyLayer(ClickedObject)||IsMineLayer(ClickedObject)){
            Debug.Log("March target clicked");
            // MarchTargetClicked=true;
            spawnedPointer=Instantiate(MarchPointer,hit.point,Quaternion.identity);
            spawnedPointer.GetComponent<SpawnedPointer>().Dependency(troopsExpeditionManager
            ,ClickedObject,hit);
            // troopsExpeditionManager.PotentialTargetForMarchClicked(ClickedObject,hit);
        }
        else if(ClickedObject.GetComponentInParent<BuildingInstance>()){
            Debug.Log("building clicked.");
            ClickedObject.GetComponentInParent<BuildingInstance>().BuildingClicked();
        }
        else if(ClickedObject.GetComponentInParent<TheUnit>()){
            Debug.Log("Troops Clicked.");
            // TheUnit theUnit= ClickedObject.GetComponentInParent<TheUnit>();
            TroopsInstanceUI troopsInstanceUI=ClickedObject.GetComponentInParent<TroopsInstanceUI>();
            //nothing should happen visibly
            troopsInstanceUI.GetTroopsUIComponent(troopsUI,GetComponent<GlobalUIManager>());
            troopsInstanceUI.TriggerUIButtons();
        }
        //for ui buttons too
        // permissionForUI=false;
    }
    }
//for ground clicked for march probably
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
    public void RefreshPermission(){
        // permissionForUI=true;
    }
    // public void UIisOpened(){
    //     IsUIOpen=true;
    // }
    // public void UIisClosed(){
    //     IsUIOpen=false;
    // }
}
