using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TheCreep : MonoBehaviour
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

    private int troopsQuantity = 5;
    [SerializeField] private int totalHealth = 10, moveSpeed = 4, attackRange = 1, chasingRange = 15;
    public int  level = 1;
    public float Damage = 5f,health;

    private bool attackerAlive=false;
    private float timer = 0f;
    [SerializeField] private float RateOfAttack = 1f;

    private Attacking attacker;
    private Transform attackerTransform;
    private CreepSpawnManager creepSpawnManager;
    private bool isWalking,isAttacking;
    private RewardManager rewardManager;
    
    public int[] resourceRewards;

    void Start()
    {
        health = totalHealth;
    }
    public float ReturnHealth(){
        //by attacking
        return health;
    }
    public void Dependency(CreepSpawnManager CreepSpawnManager, RewardManager RewardManager){
        creepSpawnManager=CreepSpawnManager;
        rewardManager=RewardManager;
    }

    public int ReturnCreepNumbers()
    {
        return troopsQuantity;
    }

    public void TakeDamage(float Damage, Attacking attacking)
    {
        
        if(attacker==null || !attackerAlive){
            //this is reselecting target after a target goes beyond range
            attacker = attacking;
            Debug.Log("New Target Acquired.");
            GetComponent<CreepVisuals>().FaceTheTarget(attacker.gameObject.transform.position);
        }
        
        attackerTransform = attacker.transform;  // Capture the attacker's transform
        health -= Damage;
        attackerAlive = true;
        UpdateHealth();
        if (health <= 0)
        {
            Debug.Log("Creeps Defeated!!!!");
            OnDefeat();
        }
    }

    void Update()
    {
        if (attacker != null)
        {
            float distanceToAttacker = Vector3.Distance(transform.position, attackerTransform.position);

            // If within chasing range and not already in attack range, move towards attacker
            if (distanceToAttacker <= chasingRange && distanceToAttacker > attackRange)
            {
                ChaseAttacker();
                // GetComponent<CreepVisuals>().TriggerWalk();
                if(isWalking==false){
                    isWalking=true;
                    GetComponent<CreepVisuals>().TriggerWalk();
                }
            }
            else{
                if(isWalking){
                    isWalking=false;
                    isAttacking=false;
                    GetComponent<CreepVisuals>().TriggerIdle();
                }
            
             if (distanceToAttacker <= attackRange)
            {
                // Engage in attack
                if (attackerAlive && attacker.ReturnHealth() > 0)
                {
                    if(isAttacking==false){
                        isWalking=false;
                        isAttacking=true;
                        GetComponent<CreepVisuals>().TriggerAttack();
                    }
                    timer += Time.deltaTime;

                    if (timer >= RateOfAttack)
                    {
                        float ActualDamage = Damage * (health / (float)totalHealth);
                        Debug.Log("Creep Damage:"+Damage+"Actual Damage:"
                        +ActualDamage);
                        attacker.TakeDamage(ActualDamage);
                        UpdateHealth();

                        if (attacker.ReturnHealth() <= 0)
                        {
                            isAttacking=false;
                            isWalking=false;
                            GetComponent<CreepVisuals>().TriggerIdle();
                            attackerAlive = false;
                        }

                        timer = 0f;
                    }
                }
            }
            else
            {
               
                // Stop chasing and reset if out of chasing range
                StopChasing();
                 if(isWalking||isAttacking){
                    isWalking=false;
                    isAttacking=false;
                    GetComponent<CreepVisuals>().TriggerIdle();
                }
            }
        }}
    }

    void ChaseAttacker()
    {
        Vector3 direction = (attackerTransform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void StopChasing()
    {
        Debug.Log("Attacker out of range");
        attackerAlive = false;
        attacker=null;
    }

    void UpdateHealth()
    {
        float fillPercent = (float)health / (float)totalHealth;
        healthFill.fillAmount = fillPercent;
    }
    void OnDefeat(){
        creepSpawnManager.CreepDefeated();
        rewardManager.GiveReward(resourceRewards);
        Destroy(gameObject);
    }
}
