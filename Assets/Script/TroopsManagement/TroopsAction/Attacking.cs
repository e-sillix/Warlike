using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    private float timer = 0f;
    [SerializeField]private float RateOfAttack = 1f;
    private TheCreep theCreep;

    public int health=10;

    public int Damage=2;
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
    public void TakeDamage(int Damage){
        health-=Damage;
        if(health<=0){
            Debug.Log("You Lost!!!!!");
        }
    }
}
