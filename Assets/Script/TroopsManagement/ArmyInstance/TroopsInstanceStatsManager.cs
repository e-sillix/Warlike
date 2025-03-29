using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsInstanceStatsManager : MonoBehaviour
{
    private TroopsStatsManager troopsStatsManager;
    private TheUnit theUnit;
    private Attacking attacking;
    private Mining mining;

    private TroopsVisualInstance troopsVisualInstance;

    private AttackStatPayload attackStatPayload;
    private GameObject SingleGameObject;

    //attacking
    private float armor,moveSpeed,totalNumberOfTroops,attackRange,marchSpeed;
    private int health,damage ;
    private string troopsType;
    private int[] troopsNumber,eachLvlLoad;


    //mining
    private int totalResourceCapacity=0;


    void Start(){
        theUnit=GetComponent<TheUnit>();
        attacking=GetComponent<Attacking>();
        mining = GetComponent<Mining>();
        troopsVisualInstance=GetComponent<TroopsVisualInstance>();

        troopsType=theUnit.troopsType;   
        troopsNumber=theUnit.troopsStats;//each lvl

        StartCoroutine(InitializeWhenReady());

        SetFightingStats(); 

        SetLoadData();
        SetBasicData();
    }


//attacking 
    IEnumerator InitializeWhenReady()
    {
    while (troopsStatsManager == null)
    {
        troopsStatsManager = FindAnyObjectByType<TroopsStatsManager>();
        if (troopsStatsManager == null)
        {
            yield return null; // Wait for the next frame and try again
        }
    }  

    eachLvlLoad = troopsStatsManager.GetTroopsLoadData(troopsType).load;
    attackStatPayload=troopsStatsManager.GetFightData(troopsType);    

    // SetFightingStats();
    }

    void SetFightingStats(){
              
        totalNumberOfTroops=troopsNumber[0]+troopsNumber[1]+troopsNumber[2]+troopsNumber[3]
        +troopsNumber[4];
        // attackStatPayload=troopsStatsManager.GetFightData(troopsType);
        health=attackStatPayload.health[0]*troopsNumber[0]+attackStatPayload.health[1]*troopsNumber[1]+
        attackStatPayload.health[2]*troopsNumber[2]+attackStatPayload.health[3]*troopsNumber[3]+
        attackStatPayload.health[4]*troopsNumber[4];

        damage=attackStatPayload.damage[0]*troopsNumber[0]+attackStatPayload.damage[1]*troopsNumber[1]+
        attackStatPayload.damage[2]*troopsNumber[2]+attackStatPayload.damage[3]*troopsNumber[3]+
        attackStatPayload.damage[4]*troopsNumber[4];

        armor=(attackStatPayload.armor[0]*(float)troopsNumber[0]+attackStatPayload.armor[1]*(float)troopsNumber[1]+
        attackStatPayload.armor[2]*(float)troopsNumber[2]+attackStatPayload.armor[3]*(float)troopsNumber[3]+
        attackStatPayload.armor[4]*(float)troopsNumber[4])/totalNumberOfTroops;

        attackRange=(attackStatPayload.attackRange[0]*(float)troopsNumber[0]+attackStatPayload
        .attackRange[1]*(float)troopsNumber[1]+attackStatPayload.attackRange[2]*(float)troopsNumber[2]+
        attackStatPayload.attackRange[3]*(float)troopsNumber[3]+attackStatPayload.attackRange[4]
        *(float)troopsNumber[4])/totalNumberOfTroops;

        Debug.Log("Heath:"+health+"Damage:"+damage+"armor:"+armor+"attackRange:"+attackRange);
        GameObject troop=attackStatPayload.SingleTroop;
        //assigning
        troopsVisualInstance.SetTroopsObj(troop,totalNumberOfTroops);
        theUnit.AssignAttackRange(attackRange);
        attacking.StatsAssigning(health,damage,(int)armor,(int)attackRange);
    }
    void SetBasicData(){
        //march speed
        // attackStatPayload=troopsStatsManager.GetFightData(troopsType);
        marchSpeed=(attackStatPayload.moveSpeed[0]*(float)troopsNumber[0]+attackStatPayload.moveSpeed[1]*
        (float)troopsNumber[1]+attackStatPayload.moveSpeed[2]*(float)troopsNumber[2]+
        attackStatPayload.moveSpeed[3]*(float)troopsNumber[3]+
        attackStatPayload.moveSpeed[4]*(float)troopsNumber[4])/totalNumberOfTroops;
        theUnit.SetMoveSpeed((int)marchSpeed);
    }

    //mininig

    void SetLoadData(){
        //this might be called after fighting.
        totalResourceCapacity=troopsNumber[0]*eachLvlLoad[0]+troopsNumber[1]*eachLvlLoad[1]+
        troopsNumber[2]*eachLvlLoad[2]+troopsNumber[3]*eachLvlLoad[3]+troopsNumber[4]*eachLvlLoad[4];
        
        mining.SetMiningStats(totalResourceCapacity);
    }
}
