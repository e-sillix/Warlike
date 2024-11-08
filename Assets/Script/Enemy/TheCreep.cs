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

    // Public variable to select from dropdown in the Inspector
    public enemyType barrackType;
    public Image healthFill;
    public int level;

    //-----------
    private int troopsQuantity=5; //determined by level dynamically.
    [SerializeField]private int health,totalHealth=6;
    public int Damage=2;

    private bool isFighting=false;
    private float timer = 0f;
    [SerializeField]private float RateOfAttack = 1f;

    private Attacking attacker;

    void Start(){
        health=totalHealth;
    }

    public int ReturnCreepNumbers(){
        return troopsQuantity;
    }
    public void TakeDamage(int Damage,Attacking attacking){
        attacker=attacking;
        health-=Damage;
        isFighting=true;
        if(health<=0){
            Debug.Log("Creeps Defeated!!!!");
            Destroy(gameObject);
        }
    }
    void Update(){
        if(isFighting && attacker.ReturnHealth()>0){
            timer += Time.deltaTime;

        // Check if one second has passed
        if (timer >= RateOfAttack)
        {   
            attacker.TakeDamage(Damage);  
            UpdateHealth();
            if(attacker.ReturnHealth()<=0){
                isFighting=false;
            }          

            // Reset the timer
            timer = 0f;
        }
        }
    }
    void UpdateHealth(){
        float fillPercent=(float)health / (float)totalHealth;
        // Debug.Log("health:"+fillPercent);
        healthFill.fillAmount = fillPercent;
    }
}
