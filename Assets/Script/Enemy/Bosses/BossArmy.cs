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
    public int Damage = 2, level = 1;

    
    [SerializeField] private float RateOfAttack = 1f;
     private float timer = 0f;

    private int currentHealth;
    private GameObject Target;
    public string KingDom;
    public int ArmyID;
    private Boss TheBoss;
    private GameObject MarchingTarget,Spawnpoint;
    private BossArmyManager bossArmyManager;
    private bool IsReturnBase,isAlive=true;
    
    void Start()
    {
        currentHealth = totalHealth;
        UpdateHealth();
    }
    public int ReturnHealth(){
        return currentHealth;
    }
    public bool IsAlive(){
        return isAlive;
    }
    public void TakeDamage(int damage)
    {
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
        Debug.Log("Boss Army Defeated.");
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
        if(isAlive){

        Target=target;
        IsReturnBase=false;
        }
    }
    public void ReturnBase(){
        //trigger by Boss when tresspasser can't be located.
        Target=Spawnpoint;
        IsReturnBase=true;
    }
    void Update(){
        float distanceToAttacker = Vector3.Distance(transform.position, Target.transform.position);

            // If Distance to attacker is greater than attack range, move towards attacker
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
            }
            else{
                //when we reach the target

                //if the target is base.
                if(IsReturnBase){
                    TheBoss.ArmyReachedBase(this);
                }

                //if the target is enemy.
                else{
                    timer += Time.deltaTime;

                    if (timer >= RateOfAttack)
                    {
                        if(Target){
                            Target.GetComponent<Attacking>().TakeDamage(Damage);                            
                        }                        
                        timer = 0f;
                    }
                }
            }
    }
}
