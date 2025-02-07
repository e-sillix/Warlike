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
        public bool isDefeated,isReturned;
        public int numbers;


        public BossArmies(int id, bool isDefeated,bool isReturned, int numbers)
        {
            this.id = id;
            this.isDefeated = isDefeated;
            this.isReturned = isReturned;
            this.numbers = numbers;
        }
    }

    [SerializeField] private List<BossArmies> bossArmies = new List<BossArmies>();
    [SerializeField] private int healTime;

    public List<BossArmies> GetActiveAndHomeBossArmies()
    {
        List<BossArmies> activeBossArmies = new List<BossArmies>();
        foreach (var bossArmy in bossArmies)
        {
            if (!bossArmy.isDefeated && bossArmy.isReturned)
            {
                activeBossArmies.Add(bossArmy);
            }
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
    }
    public void ArmyReturned(int id)
    {
        foreach (var bossArmy in bossArmies)
        {
            if (bossArmy.id == id)
            {
                bossArmy.isReturned = true;
                if(bossArmy.isDefeated){
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
                Debug.Log("Healing the defeated army completed.");
                break;
            }
        }
    }

    // Example usage: StartCoroutine(HealDefeatedArmy(armyId, healTime));

    //store all the boss armies status.

    //provide the boss army to march to the target.
}
