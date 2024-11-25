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
    public float closeDistance=0.4f;
    [SerializeField] private GameObject SelectorIcon;
    public GameObject target;

    public bool IsReturn=false;

    public int ArmyId;

   
    private TroopsExpeditionManager troopsExpeditionManager;
    private TroopsStatsManager troopsStatsManager;
    
    public int[] troopsStats;//[lvl1,lvl2,...,lvl5] number of each troops
    public string troopsType;//store type of troops inf,arch,mage....
    private GameObject spawnpoint;
    



//mining related
    public bool isMining;//this will be changed by 


    private Mining mining;
    
    void Start(){
        troopsExpeditionManager=FindAnyObjectByType<TroopsExpeditionManager>();
        troopsStatsManager=FindAnyObjectByType<TroopsStatsManager>();        
        mining = GetComponent<Mining>();       
    }    

    void Update()
    {
        if (shouldMove)
        {
            // Move the object towards the target position smoothly
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 
            moveSpeed * Time.deltaTime);

            // Check if the object has reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < closeDistance)
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
        // Debug.Log("The unit 2"+targetPosition);
        
        shouldMove = true;
    }

    public void Highlight(bool isSelected)
    {    
        SelectorIcon.SetActive(isSelected);
    }
    public void SetTroopsData(string TroopsType,int[] TroopsData){
        troopsType=TroopsType;
        troopsStats=TroopsData;
       
     }
    public void SetTroopsTarget(Vector3 position,GameObject Target,GameObject SpawnPoint){   
        spawnpoint=SpawnPoint;
        StopAllAction();     

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
        if(target==null&&IsReturn==true){
        troopsExpeditionManager.ReturnTroopsToBase(troopsType,troopsStats);

        }
        troopsExpeditionManager.MarchDone(gameObject);
    }

//action 
    void StopAllAction(){       
        if(isMining){
            isMining=false;
            mining.StopMining();
        }        
    }
  

  //returning troops
  public void ReturnTroops(){
    target=null;
    //called by ui buttons
    SetTargetPosition(spawnpoint.transform.position);
    IsReturn=true;
    
    }
}
