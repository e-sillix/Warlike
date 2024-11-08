using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attacking : MonoBehaviour
{
    private float timer = 0f;
    [SerializeField]private float RateOfAttack = 1f;
    private TheCreep theCreep;

    [SerializeField]private int health,totalHealth=10;

    public int Damage=2;

    public Image healthFill; // Reference to the HealthFill image.
    private TroopsStatsManager troopsStatsManager;
    private TroopsInstanceStatsManager troopsInstanceStatsManager;
    void Start(){
        troopsStatsManager=FindAnyObjectByType<TroopsStatsManager>();
        troopsInstanceStatsManager=GetComponent<TroopsInstanceStatsManager>();
        troopsInstanceStatsManager.SetFightingStats();
        health=totalHealth;
        UpdateHealth();
    }
    public void StartAttacking(TheCreep TheCreep){
        theCreep=TheCreep;
        Debug.Log("enemy creep numbers:"+theCreep.ReturnCreepNumbers());
    }
    void Update(){
         // Increase the timer by the time passed since the last frame
        
        if(theCreep&&health>0){

        
        timer += Time.deltaTime;

        // Check if one second has passed
        if (timer >= RateOfAttack)
        {   
            theCreep.TakeDamage(Damage,gameObject.GetComponent<Attacking>());            

            // Reset the timer
            timer = 0f;
        }
    }
    }
    public int ReturnHealth(){
        return health;
    }
    public void TakeDamage(int Damage){
        health-=Damage;
        UpdateHealth();
        if(health<=0){
            Debug.Log("You Lost!!!!!");
        }
    }
    void UpdateHealth(){
        float fillPercent=(float)health / (float)totalHealth;
        Debug.Log("health:"+fillPercent);
        healthFill.fillAmount = fillPercent;
    }
}
