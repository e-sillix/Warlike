using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BossArmyManager : MonoBehaviour
{

    [System.Serializable]
    public class BossArmies
    {
        
        public int id;
        public bool isDefeated,isReturned,isInjured;
        public int numbers;


        public BossArmies(int id, bool isDefeated,bool isReturned, int numbers,bool isInjured)
        {
            this.id = id;
            this.isDefeated = isDefeated;
            this.isReturned = isReturned;
            this.numbers = numbers;
            this.isInjured = isInjured;
        }
    }

    [SerializeField] private List<BossArmies> bossArmies = new List<BossArmies>();
    [SerializeField] private int healTime;
    [SerializeField] private List<Transform> patrolPoints;

    public List<BossArmies> GetActiveAndHomeBossArmies()
    {
        List<BossArmies> activeBossArmies = new List<BossArmies>();
        foreach (var bossArmy in bossArmies)
        {
            if (!bossArmy.isDefeated && bossArmy.isReturned && !bossArmy.isInjured)
            {
                activeBossArmies.Add(bossArmy);
            }
            // {
            //     activeBossArmies.Add(bossArmy);
            // }
        }
        return activeBossArmies;
    }
    public void ArmyDefeated(int id)
    {
        foreach (var bossArmy in bossArmies)
        {
            if (bossArmy.id == id)
            {
                bossArmy.isDefeated = true;
            }
        }

        foreach(var barmy in bossArmies){
            if(barmy.isDefeated==false){
                return;
            }
        }
        GetComponent<Boss>().BackToPatrol();
    }
    public void ArmyInjured(int id)
    {
        foreach (var bossArmy in bossArmies)
        {
            if (bossArmy.id == id)
            {
                bossArmy.isInjured = true;
            }
        }
    }
    public void ArmyReturned(int id)
    {
        Debug.Log("Army returned.");
        foreach (var bossArmy in bossArmies)
        {
            if (bossArmy.id == id)
            {
                bossArmy.isReturned = true;
                if(bossArmy.isDefeated|| bossArmy.isInjured){
                    StartCoroutine(HealDefeatedArmy(id));
                }
            }
        }
    }
    //corotine to heal the defeated boss army.

    private IEnumerator HealDefeatedArmy(int id)
    {
        Debug.Log("Healing the defeated army started.");
        yield return new WaitForSeconds(healTime);
        foreach (var bossArmy in bossArmies)
        {
            if (bossArmy.id == id )
            {
                bossArmy.isDefeated = false;
                bossArmy.isInjured =false;
                Debug.Log("Healing the defeated army completed.");
                GetComponent<Boss>().StartPatrol();
                break;
            }
        }
    }
    // public Transform GetPatrolPoint(){
    //     // [SerializeField] private List<Transform> patrolPoints;

public Transform GetPatrolPoint()
{
    if (patrolPoints == null || patrolPoints.Count == 0)
        return null;

    int index = Random.Range(0, patrolPoints.Count);
    return patrolPoints[index];
}

    
    // Example usage: StartCoroutine(HealDefeatedArmy(armyId, healTime));

    //store all the boss armies status.

    //provide the boss army to march to the target.
}
