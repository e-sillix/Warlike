using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GlobalUIManager : MonoBehaviour
{
    [SerializeField]private LayerMask groundLayer,enemyLayer,mineLayer;
    [SerializeField] private GameObject MarchPointer,UIInterference;

    // private bool permissionForUI=true,IsUIOpen=false; //this will be falsed by other cancel managers only
    private GameObject clickedObject;
    [SerializeField] private CameraSystem cameraSystem;
    [SerializeField] private TroopsTrainingManager troopsTrainingManager;
    [SerializeField] private TroopsExpeditionManager troopsExpeditionManager;
    [SerializeField] private InfoUIManager infoUIManager;
    [SerializeField] private TroopsUI troopsUI;
    private GameObject lastClicked,currentClicked;
    private GameObject spawnedPointer;

    // private bool IsUIOpen=false;

   
    public bool IsUIInterfering(){
        // IsAnyChildActive(Transform parent)
{
    foreach (Transform child in UIInterference.transform)
    {
        if (child.gameObject.activeInHierarchy) // Checks if child is fully active in the scene
        {
            return true; // Found an active child
        }
    }
    return false; // No active child found
}
    }
    public void TapAction(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = ~LayerMask.GetMask("IgnoreClick"); // Exclude "IgnoreClick" layer
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
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
                    BuildingInstance buildinstance = lastClicked.GetComponentInParent<BuildingInstance>();
                    // TheUnit theUnit=lastClicked.GetComponentInParent<TheUnit>();
                    TroopsInstanceUI troopsInstanceUI=lastClicked.GetComponentInParent<TroopsInstanceUI>();
                    CreepUI creepUI=lastClicked.GetComponentInParent<CreepUI>();
                    MineUI mineUI=lastClicked.GetComponentInParent<MineUI>();
                    BossArmyUI bossArmyUI=lastClicked.GetComponentInParent<BossArmyUI>();
                    BossUI bossUI=lastClicked.GetComponentInParent<BossUI>();
                    if (buildinstance != null) {
                        Debug.Log("previous building has BuildingInstance");
                        //this deselects the buildings
                        buildinstance.DisableUI();
                    }
                    else if(troopsInstanceUI){
                        Debug.Log("Last Clicked Was a unit.");
                        troopsInstanceUI.RefreshUIB();
                    } 
                    else if(creepUI){
                        creepUI.DeSelectCreep();
                    }
                    else if(mineUI){
                        mineUI.DeSelectMine();
                    }
                    else if(bossArmyUI){
                        bossArmyUI.DeSelectArmy();
                    }
                    else if(bossUI){
                        bossUI.DeSelectBoss();
                    }
                    else {
                        Debug.Log("No BuildingInstance found on the last clicked object.");
                    }
                }                    
            }
            lastClicked=currentClicked;
    }
    // void Update(){                   
                     
        // // if(permissionForUI){        
        // if (Input.GetMouseButtonDown(0)){ // Detect left mouse button click
        //  if (EventSystem.current.IsPointerOverGameObject())
        //     {               
        //         return;                
        //     } 
            
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     int layerMask = ~LayerMask.GetMask("IgnoreClick"); // Exclude "IgnoreClick" layer
        //     if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        //     {        
        //             // Prevent interaction if clicking on UI
        //             clickedObject=hit.collider.gameObject;
        //             currentClicked=clickedObject;
        //              if(spawnedPointer){
        //                 Destroy(spawnedPointer);
        //             }
        //             ClickAnalysis(clickedObject,hit);
        //         }
        //     else{
        //         Debug.Log("Hitted nothing");
        //     }
        //     if( lastClicked!=currentClicked){
        //         // permissionForUI=true;
        //         Debug.Log("Different building clicked");
        //         if (lastClicked != null) {
        //             BuildingInstance buildinstance = lastClicked.GetComponentInParent<BuildingInstance>();
        //             // TheUnit theUnit=lastClicked.GetComponentInParent<TheUnit>();
        //             TroopsInstanceUI troopsInstanceUI=lastClicked.GetComponentInParent<TroopsInstanceUI>();
        //             CreepUI creepUI=lastClicked.GetComponentInParent<CreepUI>();
        //             MineUI mineUI=lastClicked.GetComponentInParent<MineUI>();
        //             BossArmyUI bossArmyUI=lastClicked.GetComponentInParent<BossArmyUI>();
        //             BossUI bossUI=lastClicked.GetComponentInParent<BossUI>();
        //             if (buildinstance != null) {
        //                 Debug.Log("previous building has BuildingInstance");
        //                 //this deselects the buildings
        //                 buildinstance.DisableUI();
        //             }
        //             else if(troopsInstanceUI){
        //                 Debug.Log("Last Clicked Was a unit.");
        //                 troopsInstanceUI.RefreshUIB();
        //             } 
        //             else if(creepUI){
        //                 creepUI.DeSelectCreep();
        //             }
        //             else if(mineUI){
        //                 mineUI.DeSelectMine();
        //             }
        //             else if(bossArmyUI){
        //                 bossArmyUI.DeSelectArmy();
        //             }
        //             else if(bossUI){
        //                 bossUI.DeSelectBoss();
        //             }
        //             else {
        //                 Debug.Log("No BuildingInstance found on the last clicked object.");
        //             }
        //         }    
        //         // else if(MarchTargetClicked){
        //         //     troopsExpeditionManager.CancelMarchUI();
        //         // }
        //     }
        //     lastClicked=currentClicked;
        // }    // }}    
    // }
    void ClickAnalysis(GameObject ClickedObject,RaycastHit hit){

        if(IsGroundLayer(ClickedObject)){
            Debug.Log("March target clicked");
            // MarchTargetClicked=true;
            spawnedPointer=Instantiate(MarchPointer,hit.point,Quaternion.identity);
            spawnedPointer.GetComponent<SpawnedPointer>().Dependency(troopsExpeditionManager
            ,ClickedObject,hit);
            // troopsExpeditionManager.PotentialTargetForMarchClicked(ClickedObject,hit);
        }
        // else if(IsEnemyLayer(ClickedObject)){
        //     ClickedObject.GetComponentInParent<CreepUI>().CreepSelected(troopsExpeditionManager,
        //     infoUIManager);
        //     cameraSystem.SetFocusOn(ClickedObject);
        // }     
        else if(ClickedObject.GetComponentInParent<CreepUI>())  {
             ClickedObject.GetComponentInParent<CreepUI>().CreepSelected(troopsExpeditionManager,
            infoUIManager);
            cameraSystem.SetFocusOn(ClickedObject);
        } 
        // else if(IsMineLayer(ClickedObject)){
        //     ClickedObject.GetComponentInParent<MineUI>().MineSelected(troopsExpeditionManager,
        //     infoUIManager);
        //     cameraSystem.SetFocusOn(ClickedObject);
        // }
        else if(ClickedObject.GetComponentInParent<MineUI>()){
            ClickedObject.GetComponentInParent<MineUI>().MineSelected(troopsExpeditionManager,
            infoUIManager);
            cameraSystem.SetFocusOn(ClickedObject);
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
        else if(ClickedObject.GetComponentInParent<BossArmyUI>()){
            Debug.Log("Boss army clicked");
            ClickedObject.GetComponentInParent<BossArmyUI>().BossArmySelected(troopsExpeditionManager,
            infoUIManager);
            cameraSystem.SetFocusOn(ClickedObject);
        }
        else if(ClickedObject.GetComponentInParent<BossUI>()){
            Debug.Log("Boss clicked");
            ClickedObject.GetComponentInParent<BossUI>().BossSelected(troopsExpeditionManager,
            infoUIManager);
            cameraSystem.SetFocusOn(ClickedObject);
        }
        
    }
//for ground clicked for march probably
    private bool IsGroundLayer(GameObject obj)
    {
        return (groundLayer.value & (1 << obj.layer)) != 0;
    }
    // private bool IsEnemyLayer(GameObject obj)
    // {
    //     return (enemyLayer.value & (1 << obj.layer)) != 0;
    // }
    // private bool IsMineLayer(GameObject obj)
    // {
    //     return (mineLayer.value & (1 << obj.layer)) != 0;
    // }
    public void RefreshPermission(){
    //     // permissionForUI=true;
    }
    // public void UIisOpened(){
    //     IsUIOpen=true;
    // }
    // public void UIisClosed(){
    //     IsUIOpen=false;
    // }
}
