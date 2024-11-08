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
    
    public int[] troopsStats;//[lvl1,lvl2,...,lvl5]
    private int[] eachLvlLoad;//store each troop load capacity acc. to load.
    public string troopsType;//store type of troops inf,arch,mage....

    // private int NumberOfTroops;


//mining related
    private int[] resourcesTypeLoad={0,0,0};//[wood,grain,stone] store actual resource data.
    public bool isMining;//this will be changed by 

    public int totalResourceCapacity=10,miningRate=1;
    public int usedCapacity=0;

    private Mining mining;

    public void SetLoadCapacity(int Amount){//called by troopsExpedetionManager when spawning
        totalResourceCapacity=Amount;
    }

    void Start(){
        troopsExpeditionManager=FindAnyObjectByType<TroopsExpeditionManager>();
        troopsStatsManager=FindAnyObjectByType<TroopsStatsManager>();
        eachLvlLoad=troopsStatsManager.GetComponent<TroopsStatsManager>().GetTroopsLoadData(troopsType).load;
        mining = GetComponent<Mining>();
        SetLoadCapacity();

    }
    void SetLoadCapacity(){
        totalResourceCapacity=troopsStats[0]*eachLvlLoad[0]+troopsStats[1]*eachLvlLoad[1]+
        troopsStats[2]*eachLvlLoad[2]+troopsStats[3]*eachLvlLoad[3]+troopsStats[4]*eachLvlLoad[4];
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
    
         if(isMining)
        {
            // MineResource();
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
        // totalResourceCapacity=;

    //     // Debug.Log(TroopsType);
    //     // Debug.Log(TroopsData[0]+","+TroopsData[1]+","+TroopsData[2]+","+TroopsData[3]+","+
    //     // TroopsData[4]);
        
    //     // TroopsCountDisplayer.DisplaySoldiers(count);
     }
    public void SetTroopsTarget(Vector3 position,GameObject Target){   
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
        troopsExpeditionManager.MarchDone(gameObject);
    }

//action 
    void StopAllAction(){       
        if(isMining){
            isMining=false;
            mining.StopMining();
        }        
    }
//mining
    public int ReturnResourceCapacity(){
        return totalResourceCapacity;
    }
    public int ReturnMineRate(){
        return miningRate;
    }
  
   
    public void TransferResourceToTroops(int Amount,string mineType){
        usedCapacity+=Amount;
        if(mineType=="wood"){
            resourcesTypeLoad[0]+=Amount;
        }
        else if(mineType=="grain"){
            resourcesTypeLoad[1]+=Amount;
        }
        else if(mineType=="stone"){
            resourcesTypeLoad[2]+=Amount;
        }
        else{
            Debug.Log("trying to load something unknown");
        }
    }
    public int[] ReturnResourceTypeLoad(){
        return resourcesTypeLoad;
    }
}
