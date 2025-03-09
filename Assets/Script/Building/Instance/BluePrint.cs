using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrint : MonoBehaviour
{
    //deals with blue print ,it's movement and collision which is returned to RSpM
    //***************************
    //remember to set blueprint collider to layer blue.
    private BoxCollider boxCollider;

    [SerializeField] private GameObject TheCollider;

    [SerializeField] private GameObject BlueprintVisual;
    
    private bool movingAllowed;
    private void Start()
    {
       if (TheCollider != null)
    {
        boxCollider = TheCollider.GetComponent<BoxCollider>();

        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider component not found on TheCollider GameObject.");
        }
    }
    else
    {
        Debug.LogError("TheCollider GameObject is not assigned.");
    }
    }

    private void Update()
    {
        UpdateBlueprintPosition();
        
        if (boxCollider != null)
        {
            // IsBluePrintColliding();
            UpdateColorOfBluePrint();
        }
        
    }

   

    bool IsBlueprintColliding(){
        Collider[] colliders = Physics.OverlapBox(boxCollider.bounds.center, 
        boxCollider.bounds.extents, Quaternion.identity);

        foreach (Collider col in colliders)
        {
            GameObject parentObject = col.gameObject; // Get the topmost parent

            if (parentObject.layer == LayerMask.NameToLayer("Building"))  // Check if it has the required layer
                {
                    // Debug.Log("Parent with BlueLayer is colliding: " + parentObject.name);
                    return true;
                }
        }
        return false;
    }
    bool IsBlueprintInside(){
        Collider[] colliders = Physics.OverlapBox(boxCollider.bounds.center, 
        boxCollider.bounds.extents, Quaternion.identity);

        foreach (Collider col in colliders)
        {
            GameObject Object = col.gameObject; // Get the topmost parent

            if (Object.layer == LayerMask.NameToLayer("InnerKingdom"))  // Check if it has the required layer
                {
                    Debug.Log("Parent with InnerKingdom is colliding: " + Object.name);
                    return true;
                }
        }
        return false;

    }
    //*will update position according to camera position
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
                    if (detectedBlueprint) {
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
    
}

        }
        if(touch.phase==TouchPhase.Ended){
            movingAllowed=false;
        }
    }
    }


    //*updating color according to collisions
    private void UpdateColorOfBluePrint(){
        Renderer renderer = BlueprintVisual.GetComponent<Renderer>();
    if (renderer != null)
    {
        // Example: Change color based on collision
        if (IsBlueprintColliding()|| !IsBlueprintInside())
        {
            Debug.Log("Colliding");
            renderer.material.color = Color.red; // Collision detected
        }
        else
        {
            Debug.Log("Not Colliding");
            renderer.material.color = Color.white; // No collision
        }
    }
    else{
        Debug.Log("Rendere not there");
    }
    }

    //*return to RSM for check for placing farm
    public bool ReturnIsColliding(){        
        return IsBlueprintColliding();
    }
    // public bool ReturnBuildingUI(){        
    //     return buildingUI;
    // }
    public bool ReturnIsInsideKingdom(){
        return IsBlueprintInside();
    }
}
