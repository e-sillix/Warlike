using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmBluePrint : MonoBehaviour
{
    private BoxCollider boxCollider;
    public LayerMask groundLayer;
    public LayerMask BlueLayer;
    [SerializeField] private GameObject Thecube;
    private bool IsBlueColliding;
    private void Start()
    {
       if (Thecube != null)
    {
        boxCollider = Thecube.GetComponent<BoxCollider>();

        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider component not found on Thecube GameObject.");
        }
    }
    else
    {
        Debug.LogError("Thecube GameObject is not assigned.");
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
        Renderer renderer = Thecube.GetComponent<Renderer>();
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
