using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attacking : MonoBehaviour
{
    private float timer = 0f;
    [SerializeField]private float RateOfAttack = 1f;
    private TheCreep theCreep;
    private BossArmy bossArmy;
    // private Boss boss;
    private BossAttacking bossAttacking;

    private int health,Damage,totalHealth,armor,attackRange;


    public Image healthFill; // Reference to the HealthFill image.

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
        }
        
        
        // theCreep=TheCreep;
        // Debug.Log("enemy creep numbers:"+theCreep.ReturnCreepNumbers());
    }
    void Update(){
         // Increase the timer by the time passed since the last frame
        if(health>0){        
            if(theCreep){        
        timer += Time.deltaTime;

        // Check if one second has passed
        if (timer >= RateOfAttack)
        {   
            float ActualDamage=(float)Damage*((float)health/(float)totalHealth);
           
            theCreep.TakeDamage((int)ActualDamage,
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
                bossArmy.TakeDamage(Damage);            

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
            bossAttacking.TakeDamage(Damage,this);            

            // Reset the timer
            timer = 0f;
            if(bossAttacking.ReturnHealth()<=0){
                Debug.Log("enemy boss nulled");
                RefreshTarget();
            }

    }}
    }
    }

    //is being called by creep directly 
    public int ReturnHealth(){
        return health;
    }
    public void TakeDamage(int Damage){
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
        theCreep=null;
        bossArmy=null;
        bossAttacking=null;
    }
}
