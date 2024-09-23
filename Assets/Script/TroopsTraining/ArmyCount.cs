using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyCount : MonoBehaviour
{
    //this will only return soldier count and manage their counting

    [SerializeField] private int SoldierCount;//this one will have total troops
    private int SoldierInTheBase;

    void Start(){
        SoldierInTheBase=SoldierCount;
    }
    public int ReturnSoldierCountInTheBase(){
        return SoldierInTheBase;
    }

    public void AddSoldiers(int Amount){
        //this called bt AcceptNewTroops of troopstraininglogic
        SoldierCount=SoldierCount+Amount;
        Debug.Log("Added Troops:"+Amount);
        Debug.Log("Troops:"+SoldierCount);
        SoldierInTheBase+=Amount;
        
    }
    public void DepleteSoldiers(int Amount){
        SoldierCount=SoldierCount-Amount;
    }
    public void WithDrawingTroopsFromBase(int Amount){
        SoldierInTheBase-=Amount;
        Debug.Log("troops left in the base:"+SoldierInTheBase);
    }
    public void AddTroopsToBase(int Amount){
        SoldierInTheBase+=Amount;
    }

    //this may be updated for injured soldiers.

}
