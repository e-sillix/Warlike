using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalUIManager : MonoBehaviour
{
    [SerializeField]private LayerMask groundLayer,enemyLayer,mineLayer;

    private bool permissionForUI=true; //this will be falsed by other cancel managers only
    private GameObject clickedObject;
    [SerializeField] private TroopsTrainingManager troopsTrainingManager;
    [SerializeField] private TroopsExpeditionManager troopsExpeditionManager;
    [SerializeField] private TroopsUI troopsUI;
    private GameObject lastClicked,currentClicked;
    void Update(){
        if(permissionForUI){        
        if (Input.GetMouseButtonDown(0)){ // Detect left mouse button click
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))//this will move
                {
                    clickedObject=hit.collider.gameObject;
                    currentClicked=clickedObject;
                    ClickAnalysis(clickedObject,hit);
                }
            else{
                Debug.Log("Hitted nothing");
            }
            if( lastClicked!=currentClicked){
                permissionForUI=true;
            }
            lastClicked=currentClicked;
        }
    }}

    void ClickAnalysis(GameObject ClickedObject,RaycastHit hit){        
        if(IsGroundLayer(ClickedObject)||IsEnemyLayer(ClickedObject)||IsMineLayer(ClickedObject)){
            troopsExpeditionManager.PotentialTargetForMarchClicked(ClickedObject,hit);
        }
        else if(ClickedObject.GetComponentInParent<TheBarrack>()){
            Debug.Log("Barrack is clicked");
            ClickedObject.GetComponentInParent<BuildingInstance>().BuildingClicked();
        }
        else if(ClickedObject.GetComponentInParent<BuildingInstance>()){
            Debug.Log("building clicked.");
            ClickedObject.GetComponentInParent<BuildingInstance>().BuildingClicked();
        }
        else if(ClickedObject.GetComponentInParent<TheUnit>()){
            Debug.Log("Troops Clicked.");
            TheUnit theUnit= ClickedObject.GetComponentInParent<TheUnit>();
            TroopsInstanceUI troopsInstanceUI=ClickedObject.GetComponentInParent<TroopsInstanceUI>();
            //nothing should happen visibly
            troopsInstanceUI.GetTroopsUIComponent(troopsUI,GetComponent<GlobalUIManager>());
            troopsInstanceUI.TriggerUIButtons();
            
            //---
            // troopsUI.TroopsClicked(ClickedObject.GetComponentInParent<TheUnit>());
        }
        //for ui buttons too
        permissionForUI=false;
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
        permissionForUI=true;
    }


}
