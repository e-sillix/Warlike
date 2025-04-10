using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerBluePrint : MonoBehaviour
{
    private bool movingAllowed;
    private GameObject UnderConstructionTowerPrefab;
    [SerializeField]private Vector3 boxVolumeCollider;
    [SerializeField]private Vector3 FBoxVolumeCollider;
    [SerializeField]private float distanceBetweenBase;
    [SerializeField]private GameObject RendererObj;
    private CameraSystem cameraSystem;
    private TroopsExpeditionManager troopsExpeditionManager;
    private TowerPointPlacer towerPointPlacer;
    private MessageManager messageManager;
    // [SerializeField]private float DistanceBetweenEnemyTower;

    public void AllDependencies(GameObject towerPrefab,CameraSystem CameraSystem,
    MessageManager MessageManager,TroopsExpeditionManager TroopsExpeditionManager
    ,TowerPointPlacer TowerPointPlacer){
        UnderConstructionTowerPrefab=towerPrefab;
        cameraSystem=CameraSystem;
        messageManager=MessageManager;
        cameraSystem.SetException(true);    
        troopsExpeditionManager=TroopsExpeditionManager;
        towerPointPlacer=TowerPointPlacer;
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
        UpdateColorOfRenderer(CheckPosition());
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
        GameObject g=Instantiate(UnderConstructionTowerPrefab,
        transform.position,Quaternion.identity);
        g.GetComponent<UnderConstructionTower>().Dependency(troopsExpeditionManager,
        towerPointPlacer);
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
    int c=0;
    Collider[] hits2 = Physics.OverlapBox(boxCenter, FBoxVolumeCollider, boxRotation);
    foreach(Collider h in hits2){
        TowerInstance tower = h.GetComponentInParent<TowerInstance>();
        if(tower&&tower.IsTowerBelongToPlayer()){
            // Debug.Log("✅ Found a tower in box area!");
            c++;
            return true; // Exit early if found
        }
    }
    float distance = Vector3.Distance(Vector3.zero, transform.position);
    if(distance<distanceBetweenBase){
        Debug.Log("❌ Tower was close to the base.");
        return true;
    }
    else{
         Debug.Log("✅ Tower was far from the base.");
    }
    if(c==0){
        Debug.Log("No Friendly tower nearby.");
        return false;
    }
    // Debug.Log("❌ No tower found in box area.");
    return true;
}
    bool CheckForNearbyPlayerTower(){
        return false;
    }

    void UpdateColorOfRenderer(bool l){
        Renderer renderer = RendererObj.GetComponent<Renderer>();
    if (renderer != null)
    {
        // Example: Change color based on collision
        if (l)
        {
            renderer.material.color = Color.green; // Collision detected
        }
        else
        {
            renderer.material.color = Color.red; // No collision
        }
    }
    else{
        Debug.Log("Rendere not there");
    }
    }
}
