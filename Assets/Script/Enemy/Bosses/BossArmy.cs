using System.Collections;
using System.Collections.Generic;
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
    private bool IsReturnBase;
    
    // void Start()
    // {
    //     currentHealth = totalHealth;
    // }
   
    
    // public void TargetLocked(Attacking theUnit){
    //     if(!theUnit){
    //         MarchingTarget=Spawnpoint;
    //         Target=null;
    //         Debug.Log("This Army should return.");
    //     }
    //     else{
    //         Target=theUnit;
    //         MarchingTarget=theUnit.gameObject;
    //     }
                
    // }
    // void Update()
    // {
    //         if(MarchingTarget==null){
    //             return;
    //         }
    //         // else{
    //         //     // if()
    //         // }
    //         if(Target.GetComponent<TheUnit>()){
    //             if(Target.ReturnHealth()<=0){
    //                 Target=null;
    //                 TheBoss.TriggerDetection();
    //                 return;
    //             }
    //         }
            
    //         float distanceToAttacker = Vector3.Distance(transform.position, MarchingTarget
    //         .transform.position);

    //         // If Distance to attacker is greater than attack range, move towards attacker
    //         if ( distanceToAttacker >= attackRange)
    //         {
    //             Vector3 direction = (MarchingTarget.transform.position - transform.position).normalized;
    //             transform.position += direction * moveSpeed * Time.deltaTime;
    //         }
    //         else{
    //             //Reached the target
    //             if(Target==null){
    //                Debug.Log("Home Reached.");
    //                TheBoss.ArmyReachedBase(this);
    //             }
    //             else{
    //                 // Debug.Log("Attacking the target.");
    //                 timer += Time.deltaTime;

    //                 if (timer >= RateOfAttack)
    //                 {
    //                     // attacker.TakeDamage(Damage);
    //                     if(Target){
    //                         Target.TakeDamage(Damage);
                            
    //                     }
    //                     // else{
    //                     //     // Debug.Log("Target lost attacking");      
    //                     //     }
    //                         timer = 0f;
    //                 }
    //             }
    //         }
            
    // }
    // void UpdateHealth()
    // {
    //     float fillPercent = (float)currentHealth / (float)totalHealth;
    //     healthFill.fillAmount = fillPercent;
    // }
    // void OnDefeat(){
    //     // creepSpawnManager.CreepDefeated();
    //     Destroy(gameObject);
    // }

    public void Dependency(string kingdom, int armyNumber,GameObject spawnpoint,Boss Boss){
        KingDom=kingdom;
        ArmyID=armyNumber;
        Spawnpoint=spawnpoint;
        TheBoss=Boss;
    }
    public void TargetLocked(GameObject target){
        Target=target;
        IsReturnBase=false;
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
