using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrint : MonoBehaviour
{
   private BoxCollider boxCollider;
    public LayerMask groundLayer;
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
    public bool ReturnIsColliding(){        
        return IsBlueColliding;
    }
}
