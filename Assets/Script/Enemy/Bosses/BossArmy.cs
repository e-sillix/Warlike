using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossArmy : MonoBehaviour
{
   public enum enemyType
    {
        Infantry,
        Archer,
        Cavalry,
        Mage
    }

    public enemyType barrackType;
    public Image healthFill;
    public string[] Rewards;

    // private int troopsQuantity = 5;
    [SerializeField] private int totalHealth = 6,
    moveSpeed = 4, attackRange = 10;
    public int level = 1;
    public float currentHealth,Damage=2;

    
    [SerializeField] private float RateOfAttack = 1f;
    private float timer = 0f;

    // private int currentHealth;
    private GameObject Target;
    public string KingDom;
    public int ArmyID;
    private Boss TheBoss;
    private GameObject MarchingTarget,Spawnpoint;
    private BossArmyManager bossArmyManager;
    private bool IsReturnBase,isAlive=true,isInjured=false,isReturningBase=false,
    isPatrolling=true,isTargetAArmy=false;

    private bool WalkingVar=false;
    private bool AttackingVar=false;


    // public void TargetAArmy(bool t){
    //     isTargetAArmy=true;
    // }
    void Start()
    {
        currentHealth = totalHealth;
        UpdateHealth();
    }
    public float ReturnHealth(){
        return currentHealth;
    }
    // public bool ReturnIsInjured(){        
    //     return isInjured;
    // }
    public bool ReturnIsInjured(){
        //boss army manager
        return isInjured;
    }
    public bool IsAlive(){
        return isAlive;
    }
    public void TakeDamage(float damage)
    {
        if(!isInjured){
            isInjured=true;
        }

        currentHealth -= damage;
        UpdateHealth();
        if (currentHealth <= 0)
        {
            OnDefeat();
        }
    }
    void UpdateHealth()
    {
        float fillPercent = (float)currentHealth / (float)totalHealth;
        healthFill.fillAmount = fillPercent;
    }
    void OnDefeat(){
        // creepSpawnManager.CreepDefeated();
        // Destroy(gameObject);
        isAlive=false;
        bossArmyManager.ArmyDefeated(ArmyID);
        ReturnBase();
        // Debug.Log("Boss Army Defeated.");
    }
    void OnInjured(){
         bossArmyManager.ArmyInjured(ArmyID);
    }
    public bool ReturnIsPatrolling(){
        return isPatrolling;
    }
    public void Dependency(string kingdom, int armyNumber,BossArmyManager BossArmyManager
    ,GameObject spawnpoint,Boss Boss){
        // currentHealth=totalHealth;
        KingDom=kingdom;
        ArmyID=armyNumber;
        Spawnpoint=spawnpoint;
        TheBoss=Boss;
        bossArmyManager=BossArmyManager;
    }
    public void TargetLocked(GameObject target){
        
        if(isAlive&&target){
        // Debug.Log("Army given the target.");

        Target=target;
        GetComponent<BossArmyVisual>().FaceTheTarget(Target.transform.position);
        IsReturnBase=false;
        isReturningBase=false;
        isPatrolling=false;
        isTargetAArmy=true;
        }
        // if(target==null){
        //     if(!isInjured){
        //         isPatrolling=true;
        //         IsReturnBase=false;
        //         isReturningBase=false;
        //         isTargetAArmy=false;
        //     }
        //     // else{
        //     //     isPatrolling=false;
        //     //     IsReturnBase=true;
        //     //     // isReturningBase=false;
        //     //     isTargetAArmy=false;
        //     // }
        // }
    }
    public void TargetLeft(){
        isTargetAArmy=false;
        Target=null;
        if(isInjured){
            OnInjured();
            ReturnBase();
        }else{
            isPatrolling=true;
        

            // IsReturnBase=false;
            // isReturningBase=false;
            // Target=null;
        }
    }
    
    public void ReturnBase(){
        //trigger by Boss when tresspasser can't be located.
        Target=Spawnpoint;
        GetComponent<BossArmyVisual>().FaceTheTarget(Target.transform.position);
        isPatrolling=false;
        IsReturnBase=true;
        isTargetAArmy=false;
    }
     void StartPatrolling(Transform patrolPoint){
        //trigger by Boss when tresspasser can't be located.
        Target=patrolPoint.gameObject;
        GetComponent<BossArmyVisual>().FaceTheTarget(Target.transform.position);
        isPatrolling=true;
        IsReturnBase=false;
        isReturningBase=false;

    }
    void Update(){

        if(isTargetAArmy&&Target){
            // Debug.Log("chasing army or tower");
            float distanceToAttacker = Vector3.Distance(transform.position, Target.transform.position);

            
            if(Target.GetComponent<TheUnit>()){
                if(!Target.GetComponent<Attacking>()){
                //if the target lost Attacking component while combat 
                TheBoss.TriggerDetection();
                return;
            }           
            }
        if ( distanceToAttacker >= attackRange)
            {
                Vector3 direction = (Target.transform.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
                if(!WalkingVar){
                    WalkingVar=true;
                    AttackingVar=false;
                    GetComponent<BossArmyVisual>().TriggerIdle();
                    GetComponent<BossArmyVisual>().TriggerWalk();
                }
            }
            else if( Target.GetComponent<Attacking>()){
                    timer += Time.deltaTime;
                    if(!AttackingVar){
                        AttackingVar=true;
                        WalkingVar=false;
                        GetComponent<BossArmyVisual>().TriggerIdle();
                        GetComponent<BossArmyVisual>().TriggerAttack();
                    }

                    if (timer >= RateOfAttack)
                    {
                        if(Target){
                            float ActualDamage=Damage*(currentHealth/(float)totalHealth);
                            Target.GetComponent<Attacking>().TakeDamage(ActualDamage);                            
                        }                        
                        timer = 0f;
                    }
                }
                else if(Target.GetComponent<TowerCombat>()){
                    timer += Time.deltaTime;
                     if(!AttackingVar){
                        AttackingVar=true;
                        WalkingVar=false;
                        GetComponent<BossArmyVisual>().TriggerIdle();
                        GetComponent<BossArmyVisual>().TriggerAttack();
                    }
                    if (timer >= RateOfAttack)
                    {
                        if(Target){
                            float ActualDamage=Damage*(currentHealth/(float)totalHealth);
                            Target.GetComponent<TowerCombat>().TakeDamage(ActualDamage);                            
                        }                        
                        timer = 0f;
                    }
                }
        }
        // else if(isTargetAArmy&&Target==null){
        //     BackToPatrol();
        // }
        
        if(isInjured&&isTargetAArmy==false){
            // Debug.Log("want to return home");
        if(isReturningBase==false){
            isReturningBase=true;
            ReturnBase();
        }
    }

        if(isPatrolling){
            // Debug.Log("patrolling");
            if(Target==null){
                StartPatrolling(bossArmyManager.GetPatrolPoint());
            }
            float distanceToAttacker = Vector3.Distance(transform.position, Target.transform.position);
             if ( distanceToAttacker >= attackRange)
            {
                Vector3 direction = (Target.transform.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
                if(!WalkingVar){
                    WalkingVar=true;
                    AttackingVar=false;
                    GetComponent<BossArmyVisual>().TriggerIdle();
                    GetComponent<BossArmyVisual>().TriggerWalk();
                }
            }
            else{
                // if(isPatrolling){
                    StartPatrolling(bossArmyManager.GetPatrolPoint());
                }
        }        


        if(IsReturnBase){
        // Debug.Log("Returning base");
        float distanceToAttacker = Vector3.Distance(transform.position, Target.transform.position);

            
            
            if ( distanceToAttacker >= attackRange)
            {
                Vector3 direction = (Target.transform.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
                if(!WalkingVar){
                    WalkingVar=true;
                    AttackingVar=false;
                    GetComponent<BossArmyVisual>().TriggerIdle();
                    GetComponent<BossArmyVisual>().TriggerWalk();
                }
            }
            else{
               
                    TheBoss.ArmyReachedBase(this);
                
            }
    }
    
    // else{
    //     //if target is null and not injured. ask for patrol points.
    //     StartPatrolling(bossArmyManager.GetPatrolPoint());
    //     Debug.Log("Boss Army got new location.");
    // }
    }
    
}
