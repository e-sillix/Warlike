using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalUIManager : MonoBehaviour
{
    [SerializeField]private LayerMask groundLayer,enemyLayer,mineLayer;

    private bool permissionForUI=true; //this will be falsed by other cancel managers only
    private GameObject clickedObject;
    [SerializeField] private UITroopsTrainingManager uITroopsTrainingManager;
    [SerializeField] private MarchManager marchManager;
    [SerializeField] private TroopsExpeditionManager troopsExpeditionManager;
    void Update(){
        if(permissionForUI){        
        if (Input.GetMouseButtonDown(0)){ // Detect left mouse button click
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))//this will move
                {
                    clickedObject=hit.collider.gameObject;
                    ClickAnalysis(clickedObject,hit);
                }
            else{
                Debug.Log("Hitted nothing");
            }
        }
    }}

    void ClickAnalysis(GameObject ClickedObject,RaycastHit hit){
        // if(IsGroundLayer(ClickedObject)){
        //     Debug.Log("Ground is Clicked.");
        //     marchManager.GroundIsClicked(ClickedObject,hit);
        // }
        // else if(IsEnemyLayer(ClickedObject)){
        //     Debug.Log("Enemey is Clicked.");
        //     marchManager.MarchTargetClicked(ClickedObject,"Enemy");
        // }
        // else if(IsMineLayer(ClickedObject)){
        //     Debug.Log("Mine is Clicked.");
        //     marchManager.MarchTargetClicked(ClickedObject,"Mine");
        // }
        if(IsGroundLayer(ClickedObject)||IsEnemyLayer(ClickedObject)||IsMineLayer(ClickedObject)){
            troopsExpeditionManager.PotentialTargetForMarchClicked(ClickedObject,hit);
        }
        else if(ClickedObject.GetComponent<BarrackCollider>()){
            Debug.Log("Barrack is clicked");
            uITroopsTrainingManager.BarrackIsClicked(ClickedObject.GetComponent<BarrackCollider>());
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
