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
        LayerMask combinedLayerMask = ~groundLayer&~BlueLayer;
        // Use the BoxCollider's center and size for the overlap check
        Collider[] hitColliders = Physics.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.extents, Quaternion.identity,combinedLayerMask);
        //Debug.Log(hitColliders.Length);
        if( hitColliders.Length!=0){
            IsBlueColliding=true;
        }else{
            IsBlueColliding=false;
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
}
