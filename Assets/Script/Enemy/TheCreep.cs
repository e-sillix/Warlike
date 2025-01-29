using System.Collections;
using System.Collections.Generic;
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

    private int troopsQuantity = 5;
    [SerializeField] private int health, totalHealth = 6, moveSpeed = 4, attackRange = 10, chasingRange = 15;
    public int Damage = 2, level = 1;

    private bool attackerAlive=false;
    private float timer = 0f;
    [SerializeField] private float RateOfAttack = 1f;

    private Attacking attacker;
    private Transform attackerTransform;
    private CreepSpawnManager creepSpawnManager;
    

    void Start()
    {
        health = totalHealth;
    }
    public void Dependency(CreepSpawnManager CreepSpawnManager){
        creepSpawnManager=CreepSpawnManager;
    }

    public int ReturnCreepNumbers()
    {
        return troopsQuantity;
    }

    public void TakeDamage(int Damage, Attacking attacking)
    {
        if(attacker==null || !attackerAlive){
            //this is reselecting target after a target goes beyond range
            attacker = attacking;
            Debug.Log("New Target Acquired.");
        }
        attackerTransform = attacker.transform;  // Capture the attacker's transform
        health -= Damage;
        attackerAlive = true;

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
            }
            else if (distanceToAttacker <= attackRange)
            {
                // Engage in attack
                if (attackerAlive && attacker.ReturnHealth() > 0)
                {
                    timer += Time.deltaTime;

                    if (timer >= RateOfAttack)
                    {
                        attacker.TakeDamage(Damage);
                        UpdateHealth();

                        if (attacker.ReturnHealth() <= 0)
                        {
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
            }
        }
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
        Destroy(gameObject);
    }
}
