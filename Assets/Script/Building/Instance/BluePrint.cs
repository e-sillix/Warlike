using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrint : MonoBehaviour
{
    //deals with blue print ,it's movement and collision which is returned to RSpM
    //***************************
    //remember to set blueprint collider to layer blue.
    private BoxCollider boxCollider;
    public LayerMask groundLayer;
    public LayerMask innerKingdomLayer;
    public LayerMask outerKingdomLayer;

    public LayerMask BlueLayer;
    [SerializeField] private GameObject TheCollider;
    private bool IsBlueColliding;
    private bool buildingUI;
    private bool IsInsideKingdom;
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
            IsBluePrintColliding();
            UpdateColorOfBluePrint();
        }
        
    }

    private void IsBluePrintColliding()
    {       
        bool groundCollision = Physics.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.extents, Quaternion.identity, groundLayer).Length != 0;
        bool blueCollision = Physics.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.extents, Quaternion.identity, BlueLayer).Length != 0;
        bool outerKingdomCollision = Physics.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.extents, Quaternion.identity, outerKingdomLayer).Length != 0;
        bool innerKingdomCollision = Physics.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.extents, Quaternion.identity, innerKingdomLayer).Length != 0;

        LayerMask allowedLayers = groundLayer | BlueLayer | outerKingdomLayer | innerKingdomLayer;
        LayerMask allOtherLayers = ~allowedLayers;

        bool otherCollision = Physics.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.extents, Quaternion.identity, allOtherLayers).Length != 0;

        // Set IsBlueColliding to true only if all required layers are colliding and there are no other collisions
        if (groundCollision && blueCollision && outerKingdomCollision && 
        innerKingdomCollision && !otherCollision)//false if colliding with other than it should
        {
            IsBlueColliding = false;
        }
        else
        {
            IsBlueColliding = true;
        }
        if(innerKingdomCollision){
            // buildingUI=true; 
            IsInsideKingdom=true;
        }
        else{
            // buildingUI=false;//this will be used to turn off ui.
            IsInsideKingdom=false;
        }
    }

    //*will update position according to camera position
    void UpdateBlueprintPosition()
    {
        // Create a ray from the center of the screen
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Perform the raycast and check if it hits the ground
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            transform.position = hit.point;

        }
    }


    //*updating color according to collisions
    private void UpdateColorOfBluePrint(){
        Renderer renderer = TheCollider.GetComponent<Renderer>();
    if (renderer != null)
    {
        // Example: Change color based on collision
        if (ReturnIsColliding())
        {
            renderer.material.color = Color.red; // Collision detected
        }
        else
        {
            renderer.material.color = Color.white; // No collision
        }
    }
    }

    //*return to RSM for check for placing farm
    public bool ReturnIsColliding(){        
        return IsBlueColliding;
    }
    // public bool ReturnBuildingUI(){        
    //     return buildingUI;
    // }
    public bool ReturnIsInsideKingdom(){
        return IsInsideKingdom;
    }
}
