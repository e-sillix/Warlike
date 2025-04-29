using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attacking : MonoBehaviour
{
    private float timer = 0f;
    [SerializeField]private float RateOfAttack = 1f;
    private GameObject Target;
    private TheCreep theCreep;
    private BossArmy bossArmy;
    // private Boss boss;
    private BossAttacking bossAttacking;
    private TowerCombat towerCombat;

    private int totalHealth,armor,attackRange;
    private float health,Damage;

    private bool InCombact=false;
    public Image healthFill; // Reference to the HealthFill image.
    private float DistanceBetweenTarget,AttackingRange;
    public void StatsAssigning(int h,int d,int a,int r){
        //by troopsinstanceStatsmanager
        totalHealth=h;
        health=totalHealth;
        Damage=d;
        armor=a;
        attackRange=r;
        UpdateHealthVisual();
    }
    public void StartAttacking(GameObject target){
        Target=target;
        AttackingRange=GetComponent<TheUnit>().ReturnAttackRange();
        GetComponent<TroopsVisualInstance>().TriggerIdle();
        Debug.Log("i called idle startAttacking");
        GetComponent<TroopsVisualInstance>().TriggerAttacking();
        if(target.GetComponent<TheCreep>()){
            theCreep=target.GetComponent<TheCreep>();
            Debug.Log("enemy creep numbers:"+theCreep.ReturnCreepNumbers());
        }
        else if(target.GetComponent<BossArmy>()){
            bossArmy=target.GetComponent<BossArmy>();
            Debug.Log("enemy boss army");
        }
        else if (target.GetComponent<BossAttacking>()){
            bossAttacking=target.GetComponent<BossAttacking>();
            Debug.Log("enemy boss");
        }else if(target.GetComponent<TowerCombat>()){
            towerCombat=target.GetComponent<TowerCombat>();
            Debug.Log("enemy tower");
        }
        InCombact=true;
        
        // theCreep=TheCreep;
        // Debug.Log("enemy creep numbers:"+theCreep.ReturnCreepNumbers());
    }
    void Update(){
         // Increase the timer by the time passed since the last frame
        
        if(Target&&health>0&&InCombact==true){    
           if (Vector3.Distance(transform.position, Target.transform.position
                ) < AttackingRange){   
            if(theCreep){        
        timer += Time.deltaTime;

        // Check if one second has passed
        if (timer >= RateOfAttack)
        {   
            float ActualDamage=Damage*(health/(float)totalHealth);
           
            theCreep.TakeDamage(ActualDamage,
            gameObject.GetComponent<Attacking>());            

            // Reset the timer
            timer = 0f;
        }
        }
        else if(bossArmy){
            timer += Time.deltaTime;

            // Check if one second has passed
            if (timer >= RateOfAttack)
            {   
                float ActualDamage=Damage*(health/(float)totalHealth);
           
                bossArmy.TakeDamage(ActualDamage);            

                // Reset the timer
                timer = 0f;
                if(bossArmy.ReturnHealth()<=0){
                    Debug.Log("enemy boss army nulled");
                    RefreshTarget();
                }
            }
    }
    else if(bossAttacking){
        timer += Time.deltaTime;

        // Check if one second has passed
        if (timer >= RateOfAttack)
        {   
             float ActualDamage=Damage*(health/(float)totalHealth);
           
            bossAttacking.TakeDamage(ActualDamage,this);            

            // Reset the timer
            timer = 0f;
            if(bossAttacking.ReturnHealth()<=0){
                Debug.Log("enemy boss Destroyed");
                RefreshTarget();
            }

    }}
    else if(towerCombat){
        timer += Time.deltaTime;

        // Check if one second has passed
        if (timer >= RateOfAttack)
        {   
             float ActualDamage=Damage*(health/(float)totalHealth);
           
            towerCombat.TakeDamage(ActualDamage,this);            

            // Reset the timer
            timer = 0f;
            if(towerCombat.ReturnHealth()<=0){
                // Debug.Log("enemy tower Destroyed");
                RefreshTarget();
            }

    }}
    else{
        //this will be triggered if it was previously in combat
        if(InCombact){
            Debug.Log("i called idle incombat");
            GetComponent<TroopsVisualInstance>().TriggerIdle();
        }
        InCombact=false;
    }
    }
    else{
        GetComponent<TheUnit>().ReChaseEnemy();
    }}
    if(Target==null&&health>0&&InCombact==true){
        RefreshTarget();
    }
    }

    //is being called by creep directly 
    public float ReturnHealth(){
        return health;
    }
    public void TakeDamage(float Damage){
        health-=Damage;
        Debug.Log("damage took:"+Damage);
        //visual change
        UpdateHealthVisual();

        if(health<=0){
            Debug.Log("You Lost!!!!!");
            GetComponent<TheUnit>().TroopsDefeated();
        }
    }

    //

    void UpdateHealthVisual(){
        //change it's name to update stats++++++
        //and update other stats too like damage
        float fillPercent=(float)health / (float)totalHealth;
        // Debug.Log("health:"+fillPercent);
        healthFill.fillAmount = fillPercent;
    }
    public void RefreshTarget(){
        //this will be triggered when changing target or returning home.
        GetComponent<TroopsVisualInstance>().TriggerIdle();
        theCreep=null;
        bossArmy=null;
        bossAttacking=null;
        towerCombat=null;
        Target=null;
        InCombact=false;  

        // GetComponent<TroopsVisualInstance>().TriggerIdle();
    }
}
