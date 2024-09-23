using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyCount : MonoBehaviour
{
    //this will only return soldier count and manage their counting

    [SerializeField] private int SoldierCount;

    public int ReturnSoldierCount(){
        return SoldierCount;
    }

    public void AddSoldiers(int Amount){
        //this called bt AcceptNewTroops of troopstraininglogic
        SoldierCount=SoldierCount+Amount;
        Debug.Log("Added Troops:"+Amount);
        Debug.Log("Troops:"+SoldierCount);
        
    }
    public void DepleteSoldiers(int Amount){
        SoldierCount=SoldierCount-Amount;
    }

    //this may be updated for injured soldiers.

}
