using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this one is attached to the main Army or troops.
public class TheUnit : MonoBehaviour
{
    public LayerMask groundLayer; // Assign this to your ground layer in the Inspector
    public float moveSpeed = 5f,boostSpeed=1.5f; // Speed of movement    
    private Vector3 targetPosition;
    private bool shouldMove = false;
    public float closeDistance=5f;
    [SerializeField] private GameObject SelectorIcon;
    [SerializeField]private GameObject SelectedRings;
    public GameObject target;
    public bool IsReturn=false;
    public int ArmyId;   
    private TroopsExpeditionManager troopsExpeditionManager;
    // private TroopsStatsManager troopsStatsManager;
    
    public int[] troopsStats;//[lvl1,lvl2,...,lvl5] number of each troops
    public string troopsType;//store type of troops inf,arch,mage....
    private GameObject spawnpoint,Pointer;
    
    private bool isDefeated=false;

//mining related
    public bool isMining;//this will be changed by 

    private float interactionRange,attackRange;
    
    private Mining mining;

    private bool isTargetIsEnemy=false;
    private bool UpdateTroopsDirection=true;
    private TroopsVisualInstance troopsVisualInstance;
    private bool isTargetMoveable;
    private GameObject TerritoryCollider;
    // private int BossId=0;
    
    public bool returnIsDefeated(){
        // Debug.Log("Return :"+isDefeated);
        return isDefeated;
    }
    public void SetTerritoryCollider(GameObject g){
        TerritoryCollider=g;
    }
    public int GetBossId(){
        return TerritoryCollider.GetComponent<UnitTerritoryCollider>().ReturnBreachingBossId();
    }
    public void AssignAttackRange(float AttackRange){
        attackRange=AttackRange;
    }
    public float ReturnAttackRange(){
        return attackRange;
    }
    void Start(){
        troopsExpeditionManager=FindAnyObjectByType<TroopsExpeditionManager>();
        // troopsStatsManager=FindAnyObjectByType<TroopsStatsManager>();        
        mining = GetComponent<Mining>();   
        troopsVisualInstance=GetComponent<TroopsVisualInstance>();    

    }    
    public void SetMoveSpeed(int speed){
        moveSpeed=speed;
        // Debug.Log("speed set to:"+speed);

    }
    void Update()
    {       
        //  if(target!=null){
        //     if(!target.gameObject.activeInHierarchy){
        //          StopAllAction();}
        //  } 
        if (shouldMove)
        {
            // Move the object towards the target position smoothly
            if(isTargetMoveable==false)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 
            moveSpeed * Time.deltaTime);
            else{
                if(target&& target.transform!=null)
                transform.position=Vector3.MoveTowards(transform.position, target.transform.position
                ,moveSpeed * Time.deltaTime);
                else{
                    StopAllAction();
                }
            }




            if(UpdateTroopsDirection){
        
            troopsVisualInstance.UpdateTroopsDirection(targetPosition);
            troopsVisualInstance.TriggerWalking();
            UpdateTroopsDirection=false;
        }   


            if(isTargetIsEnemy&& attackRange!=0){
                interactionRange=attackRange;
            }else{
                interactionRange=closeDistance;
            }



            // Check if the object has reached the target position
            if(isTargetMoveable==false){
            if (Vector3.Distance(transform.position, targetPosition) < interactionRange)
            {
                shouldMove = false; // Stop moving
                TargetReached();
                Destroy(Pointer);
            }}
            else{
                if(target&& target.transform!=null)
                {if (Vector3.Distance(transform.position, target.transform.position
                ) < interactionRange)
            {
                shouldMove = false; // Stop moving
                TargetReached();
                Destroy(Pointer);
            }}
            // else{
            //     StopAllAction();
            //     shouldMove=true;
            // }
            }
        } 
        // else{
        //     if(target!=null){
        //         GetComponent<Attacking>().RefreshTarget();
        //         // shouldMove=true;s
        //         SetTroopsTargetCombat(target, spawnpoint);
        //     }
        // }
               
    }

    // Method to set the target position and start moving
    void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
        // Debug.Log("The unit 2"+targetPosition);
        
        shouldMove = true;
        UpdateTroopsDirection=true;
        // if(troopsVisualInstance==null){
        //     troopsVisualInstance=GetComponent<TroopsVisualInstance>();
        // }
        // troopsVisualInstance.TriggerWalking();
    }

    public void Highlight(bool isSelected)
    {    
        SelectorIcon.SetActive(isSelected);
        SelectedRings.SetActive(isSelected);
    }
    public void SetTroopsData(string TroopsType,int[] TroopsData){
        troopsType=TroopsType;
        troopsStats=TroopsData;
       
     }
    public void SetTroopsTarget(Vector3 position,GameObject Target,GameObject SpawnPoint
    ,GameObject pointer){   
        spawnpoint=SpawnPoint;
        if(Pointer){
            Destroy(Pointer);
        }
        Pointer=pointer;
        StopAllAction();     

        target=Target;              
        if(target.layer==6){
            SetTargetPosition(position);
            isTargetMoveable=false;
        }
        else{
            SetTargetPosition(target.transform.position);            
        }        
    }
    public void SetTroopsTargetCombat(GameObject Target,GameObject SpawnPoint){
        spawnpoint=SpawnPoint;
        isTargetMoveable=true;
        if(Pointer){
            Destroy(Pointer);
        }
        StopAllAction();     
        isTargetIsEnemy=true;
        target=Target;  
        if(target.GetComponent<CreepUI>()){
            target.GetComponent<CreepUI>().PassiveSelected();  
        }
        else if(target.GetComponent<BossArmyUI>()){
            target.GetComponent<BossArmyUI>().PassiveSelected();  
        }
        else if(target.GetComponent<BossUI>()){
            target.GetComponent<BossUI>().PassiveSelected();

        }
        SetTargetPosition(target.transform.position); 
    }
    public void ReChaseEnemy(){
        SetTargetPosition(target.transform.position); 
    }
    public void SetTroopsTargetMine(GameObject Target,GameObject SpawnPoint){
        spawnpoint=SpawnPoint;
        isTargetMoveable=false;
        if(Pointer){
            Destroy(Pointer);
        }
        StopAllAction();     

        target=Target;  
        target.GetComponent<MineUI>().PassiveSelected();  
        SetTargetPosition(target.transform.position); 
    }
    void TargetReached(){
        // Debug.Log("target reached");
        troopsVisualInstance.TriggerIdle();
        if(target!=null){
           if(target.GetComponent<TheCreep>()){
            target.GetComponent<CreepUI>().DeSelectCreepPassive();
        } 
        else if(target.GetComponent<BossArmy>()){
            target.GetComponent<BossArmyUI>().DeSelectArmyPassive();
        }
        else if(target.GetComponent<Boss>()){
            target.GetComponent<BossUI>().DeSelectBossPassive();
        }
        else if(target.GetComponent<TheMine>()){
            target.GetComponent<MineUI>().DeSelectMinePassive();
        } 
        }   
        if(target==null&&IsReturn==true){
        troopsExpeditionManager.ReturnTroopsToBase(gameObject,troopsType,troopsStats);
        }
        troopsExpeditionManager.MarchDone(gameObject);
    }

//action 
   public void StopAllAction(){   
        isTargetIsEnemy=false;    
        shouldMove=false;

        GetComponent<TroopsVisualInstance>().TriggerIdle();
        if(isMining){
            isMining=false;
            mining.StopMining();
        }
        if(target!=null){
           if(target.GetComponent<TheCreep>()){
            target.GetComponent<CreepUI>().DeSelectCreepPassive();
        }  
        else if(target.GetComponent<BossArmy>()){
            target.GetComponent<BossArmyUI>().DeSelectArmyPassive();
        }
        else if(target.GetComponent<Boss>()){
            target.GetComponent<BossUI>().DeSelectBossPassive();
        }
        else if(target.GetComponent<TheMine>()){
            target.GetComponent<MineUI>().DeSelectMinePassive();
        }
        }   
        if(GetComponent<Attacking>()){

            GetComponent<Attacking>().RefreshTarget();    
        }
        else{
            Debug.Log("You already lost");
        }
    }
  

  //returning troops
  public void ReturnTroops(){
    target=null;
    GetComponent<Attacking>().RefreshTarget();
    //called by ui buttons
    SetTargetPosition(spawnpoint.transform.position);
    isTargetMoveable=false;
    IsReturn=true;    
    }

    public void StopMarching(){
        //called by ui buttons.
        target=null;
        GetComponent<Attacking>().RefreshTarget();
        shouldMove=false;
    }
    public void TroopsDefeated(){
        //ghosted speed
        moveSpeed*=boostSpeed;
        isDefeated=true;
         troopsVisualInstance.TriggerIdle();
        //have to make troops untargetable.
        if(GetComponent<TroopsInstanceUI>())
        Destroy(GetComponent<TroopsInstanceUI>().ReturnUIButton());
        if(GetComponent<Attacking>())
        Destroy(GetComponent<Attacking>());
        // Destroy(GetComponent<C)
        Debug.Log("is Defeated:"+isDefeated);
        ReturnTroops();
    }
}
