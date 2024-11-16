using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsInstanceStatsManager : MonoBehaviour
{
    private TroopsStatsManager troopsStatsManager;
    private TheUnit theUnit;
    private Attacking attacking;

    private AttackStatPayload attackStatPayload;
    private float armor,moveSpeed,totalNumberOfTroops;
    private int health,damage;
    private string troopsType;
    private int[] troopsNumber;

    void Assigner(){
        theUnit=GetComponent<TheUnit>();
        attacking=GetComponent<Attacking>();

        troopsStatsManager=FindAnyObjectByType<TroopsStatsManager>();
        troopsType=theUnit.troopsType;   
        troopsNumber=theUnit.troopsStats;     
    }
    public void SetFightingStats(){
        if(!troopsStatsManager){
            Assigner();
        }
        totalNumberOfTroops=troopsNumber[0]+troopsNumber[1]+troopsNumber[2]+troopsNumber[3]
        +troopsNumber[4];
        attackStatPayload=troopsStatsManager.GetFightData(troopsType);
        health=attackStatPayload.health[0]*troopsNumber[0]+attackStatPayload.health[1]*troopsNumber[1]+
        attackStatPayload.health[2]*troopsNumber[2]+attackStatPayload.health[3]*troopsNumber[3]+
        attackStatPayload.health[4]*troopsNumber[4];

        damage=attackStatPayload.damage[0]*troopsNumber[0]+attackStatPayload.damage[1]*troopsNumber[1]+
        attackStatPayload.damage[2]*troopsNumber[2]+attackStatPayload.damage[3]*troopsNumber[3]+
        attackStatPayload.damage[4]*troopsNumber[4];

        armor=(attackStatPayload.armor[0]*(float)troopsNumber[0]+attackStatPayload.armor[1]*(float)troopsNumber[1]+
        attackStatPayload.armor[2]*(float)troopsNumber[2]+attackStatPayload.armor[3]*(float)troopsNumber[3]+
        attackStatPayload.armor[4]*(float)troopsNumber[4])/totalNumberOfTroops;

        Debug.Log("Heath:"+health+"Damage:"+damage+"armor:"+armor);

        //assigning

        attacking.StatsAssigning(health,damage,(int)armor);


    }
}
