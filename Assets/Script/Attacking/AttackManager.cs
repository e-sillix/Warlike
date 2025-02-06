using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private Attacking attacking;

    public void InitiateAttack(GameObject TheUnit,GameObject target){
        // TheUnit theUnit=TheUnit.GetComponent<TheUnit>();
        attacking=TheUnit.GetComponent<Attacking>();
        attacking.StartAttacking(target);
        Refresh();
    }  
    void Refresh(){
        attacking=null;
    }
}
