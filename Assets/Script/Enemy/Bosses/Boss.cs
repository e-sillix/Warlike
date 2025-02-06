using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private int troopLevel, agressiveLevel,armySize;
    [SerializeField] private int [] Armies;//Armies stores number of troops 
    //of each type and also how many boss army are there.
    [SerializeField] private string KingDom;
    [SerializeField] private string[] Rewards, Treasure;
    //commander maybe.
    [SerializeField] private GameObject BossArmyPrefab,SpawnPoint;

    public float detectionInterval = 5f;

    private GameObject[] playerArmies=new GameObject[5]; // Array to store the detected units

    private ArmyDetector armyDetector;
    private BossArmy[] bossArmies;//this will be used to store the spawned armies.

    private GameObject currentTarget;
    void Start()
    {
        bossArmies = new BossArmy[Armies.Length];
        // Start the Coroutine when the game begins
        armyDetector=GetComponent<ArmyDetector>();
        if(armyDetector==null){
            Debug.Log("ArmyDetector not found.");
        }else{
            StartCoroutine(DetectUnitsEveryInterval());
        }

    }
    public void TriggerDetection(){
        //triggered by bossArmy when it defeats the player army.
        StopCoroutine(DetectCurrentTarget());
        Debug.Log("Target defeated.");
        StartCoroutine(DetectUnitsEveryInterval());
    }
    IEnumerator DetectUnitsEveryInterval()
    {
        while (true) // Keep running the detection indefinitely
        {
            //this will reinstantiate the array 
            Debug.Log("1");
            playerArmies=new GameObject[5];
            //now fill it with the detected units present.
            playerArmies=armyDetector.FindNearbyUnits(); // Find the nearby units
            if(playerArmies[0]!=null){
                Debug.Log("Found Tresspasser.");
                currentTarget=playerArmies[0];
                // PlayerUnitFound();
                StartCoroutine(DetectCurrentTarget());
                yield break; // This stops the coroutine
            }
            else{
                ReturnArmies();
            }
            yield return new WaitForSeconds(detectionInterval); // Wait for the specified interval
        }
    }

    IEnumerator DetectCurrentTarget(){
        while (true) // Keep running the detection indefinitely
        {           
            if(armyDetector.FindCurrentTarget(currentTarget)){//this will return true if target is 
            //still in range
                DirectingAllArmies();
                Debug.Log("Target is Still in the range");
                yield return new WaitForSeconds(detectionInterval); 
            } // Find the nearby units
            else{
                Debug.Log("Target not found in the range.");
                Debug.Log("Starting normal search");
                currentTarget=null;
                // NullArmiesTarget();
                StartCoroutine(DetectUnitsEveryInterval());
                yield break;
            }
            // Wait for the specified interval
        }
    }

    void DirectingAllArmies(){
       //this will be called when a player unit is found.

        //first we need to find all the armies and spawn 

        //so we don't repeation
                   
        
        for (int i = 0; i < Armies.Length; i++)
        {
            if (bossArmies[i] != null)
            {
                bossArmies[i].TargetLocked(currentTarget);
                continue; // Skip this iteration if bossArmy is already instantiated
            }
            // This will spawn the boss army.
            GameObject bossArmyObject = Instantiate(BossArmyPrefab, SpawnPoint.transform.position, 
            Quaternion.identity);
            BossArmy bossArmy = bossArmyObject.GetComponent<BossArmy>();
            bossArmy.Dependency(KingDom, i,SpawnPoint,this); // this will set the kingdom and id of that army.
            bossArmy.TargetLocked(currentTarget);
            bossArmies[i] = bossArmy;
        }
        }
    void ReturnArmies(){
        foreach (BossArmy bossArmy in bossArmies){
            // if (bossArmy.KingDom == KingDom)
            // {
            if(bossArmy){

                bossArmy.ReturnBase();
            }
            // }
        }    
    }
    public void ArmyReachedBase(BossArmy bossArmy){
        //this called by the boss army when it reaches the base.
        bossArmies[bossArmy.ArmyID]=null;
        Destroy(bossArmy.gameObject);
    }
    public void OnDefeat(){
        Debug.Log("Boss Defeated,Give Rewards");
        foreach (BossArmy bossArmy in bossArmies){
            
            if(bossArmy){

                Destroy(bossArmy.gameObject);
            }

        Destroy(gameObject);
    }}
}
