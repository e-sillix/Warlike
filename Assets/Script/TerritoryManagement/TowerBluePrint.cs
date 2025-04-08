using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBluePrint : MonoBehaviour
{
    private bool movingAllowed;
    private GameObject UnderConstructionTowerPrefab;
    [SerializeField]private Vector3 boxVolumeCollider;
    private CameraSystem cameraSystem;

    private MessageManager messageManager;
    // [SerializeField]private float DistanceBetweenEnemyTower;

    public void AllDependencies(GameObject towerPrefab,CameraSystem CameraSystem,
    MessageManager MessageManager){
        UnderConstructionTowerPrefab=towerPrefab;
        cameraSystem=CameraSystem;
        messageManager=MessageManager;
        cameraSystem.SetException(true);    
    }
   private void Update()
    {
        UpdateBlueprintPosition();
    }
    void UpdateBlueprintPosition()
    {
        
        if (Input.touchCount == 1)
        {
        Touch touch = Input.GetTouch(0);
        if(touch.phase==TouchPhase.Began){
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
                int layerMask = LayerMask.GetMask("Blue", "Ground"); // Allow only "Blue" and "Ground"

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
                    {
                    BluePrint detectedBlueprint = hit.collider.GetComponentInParent<BluePrint>();
                     TowerBluePrint BTower=hit.collider.GetComponentInParent<TowerBluePrint>();
                    if (detectedBlueprint||BTower) {
                    
                    //    return;
                    movingAllowed=true;
                    }
                    else{
                        movingAllowed=false;
                    }
                    
                }
        }
        if (movingAllowed && touch.phase == TouchPhase.Moved)
        {

    Ray touchRay = Camera.main.ScreenPointToRay(touch.position);
    Ray prevTouchRay = Camera.main.ScreenPointToRay(touch.position - touch.deltaPosition);

    Plane groundPlane = new Plane(Vector3.up, transform.position); // Use current Y instead of y=0

    if (groundPlane.Raycast(prevTouchRay, out float enterPrev) && groundPlane.Raycast(touchRay, out float enterCurr))
    {
        Vector3 prevPoint = prevTouchRay.GetPoint(enterPrev);
        Vector3 currPoint = touchRay.GetPoint(enterCurr);

        Vector3 moveDir = prevPoint - currPoint;
        
        // Preserve Y position by setting it to current Y
        transform.position -= new Vector3(moveDir.x, 0, moveDir.z);
        CheckPosition();
}

        }
        if(touch.phase==TouchPhase.Ended){
            movingAllowed=false;
        }
    }
    }


    public void PlaceTower(){
        if(CheckPosition()){
        cameraSystem.SetException(false);
        Instantiate(UnderConstructionTowerPrefab,transform.position,Quaternion.identity);
        Destroy(gameObject);
        }
        else{
            messageManager.TowerIsOnEnemeyTerritory();
            // Debug.Log("Tower in Vicinity");
        }

    }
    public void CancelTower(){
        cameraSystem.SetException(false);
        Destroy(gameObject);
    }
    bool CheckPosition(){
       //returns false when there is a tower .
    Vector3 boxCenter = transform.position;
    Vector3 boxHalfExtents = boxVolumeCollider; // Adjust size as needed
    Quaternion boxRotation = Quaternion.identity; // No rotation

    // Overlap box returns all colliders within the box area
    Collider[] hits = Physics.OverlapBox(boxCenter, boxHalfExtents, boxRotation);

    foreach (Collider hit in hits)
    {
        TowerInstance tower = hit.GetComponentInParent<TowerInstance>();

        if (tower&&!tower.IsTowerBelongToPlayer())
        {
            // Debug.Log("✅ Found a tower in box area!");
            return false; // Exit early if found
        }
    }

    // Debug.Log("❌ No tower found in box area.");
    return true;
}

    
}
