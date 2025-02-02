using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private int troopLevel, agressiveLevel,armySize;
    [SerializeField] private int [] expertiseTroopType,noOfTroops;//noofTroops stores number of troops 
    //of each type and also how many boss army are there.
    [SerializeField] private string bossName;
    [SerializeField] private string[] Rewards, Treasure;
    //commander maybe.
    [SerializeField] private GameObject BossArmyPrefab,SpawnPoint;

    public float detectionInterval = 5f;

    private TheUnit[] playerArmies=new TheUnit[5]; // Array to store the detected units

    private ArmyDetector armyDetector;
    private BossArmy[] bossArmies;//this will be used to store the spawned armies.
    void Start()
    {
        // Start the Coroutine when the game begins
        armyDetector=GetComponent<ArmyDetector>();
        if(armyDetector==null){
            Debug.Log("ArmyDetector not found.");
        }else{
            StartCoroutine(DetectUnitsEveryInterval());
        }

    }
    IEnumerator DetectUnitsEveryInterval()
    {
        while (true) // Keep running the detection indefinitely
        {
            //this will reinstantiate the array 
            playerArmies=new TheUnit[5];
            //now fill it with the detected units present.
            playerArmies=armyDetector.FindNearbyUnits(); // Find the nearby units
            if(playerArmies[0]!=null){
                PlayerUnitFound();
            }
            yield return new WaitForSeconds(detectionInterval); // Wait for the specified interval
        }
    }

    void PlayerUnitFound(){
       //this will be called when a player unit is found.
        for(int i=0;i<noOfTroops.Length;i++){
            //this will spawn the boss army.
            bossArmies[i]=Instantiate(BossArmyPrefab,SpawnPoint.transform.position,Quaternion.identity).
            GetComponent<BossArmy>();
        }
    }
}
