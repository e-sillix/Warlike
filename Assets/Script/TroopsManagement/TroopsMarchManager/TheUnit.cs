using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this one is attached to the main Army or troops.
public class TheUnit : MonoBehaviour
{
    public LayerMask groundLayer; // Assign this to your ground layer in the Inspector
    public float moveSpeed = 5f; // Speed of movement
    
    private Vector3 targetPosition;
    private bool shouldMove = false;
    [SerializeField] private GameObject SelectorIcon;
    public GameObject target;
    private string actionOfTroop;
    public bool IsReturn=false;

    public int ArmyId;
    private TroopsExpeditionManager troopsExpeditionManager;
    

    // private int NumberOfTroops;

    void Start(){
        troopsExpeditionManager=FindAnyObjectByType<TroopsExpeditionManager>();
    }

    void Update()
    {
        if (shouldMove)
        {
            // Move the object towards the target position smoothly
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 
            moveSpeed * Time.deltaTime);

            // Check if the object has reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                shouldMove = false; // Stop moving
                TargetReached();
            }
        }
    }

    // Method to set the target position and start moving
    void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
        Debug.Log("The unit 2"+targetPosition);
        
        shouldMove = true;
    }

    public void Highlight(bool isSelected)
    {    
        SelectorIcon.SetActive(isSelected);
    }
    public void SetTroopsData(string TroopsType,int[] TroopsData){
        // Debug.Log(TroopsType);
        // Debug.Log(TroopsData[0]+","+TroopsData[1]+","+TroopsData[2]+","+TroopsData[3]+","+
        // TroopsData[4]);
        
        // TroopsCountDisplayer.DisplaySoldiers(count);
    }
    public void SetTroopsTarget(Vector3 position,GameObject Target){        
        target=Target;              
        if(target.layer==6){
            SetTargetPosition(position);
        }
        else{
            SetTargetPosition(target.transform.position);            
        }        
    }
    void TargetReached(){
        // Debug.Log("target reached");
        troopsExpeditionManager.MarchDone(gameObject);
    }
}
