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
    // private BossArmy[] bossArmies;//this will be used to store the spawned armies.

    private List<BossArmy> SpawnedbossArmies = new List<BossArmy>();
    // private BossArmy[] SpawnedbossArmies;
    private GameObject currentTarget;
    private BossArmyManager bossArmyManager ;
    void Start()
    {
        // bossArmies = new BossArmy[Armies.Length];
        // Start the Coroutine when the game begins
        armyDetector=GetComponent<ArmyDetector>();
        if(armyDetector==null){
            Debug.Log("ArmyDetector not found.");
        }else{
            StartCoroutine(DetectUnitsEveryInterval());
        }
        bossArmyManager = GetComponent<BossArmyManager>();
        if (bossArmyManager == null)
        {
            Debug.LogError("BossArmyManager not found.");
            return;
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
       
        //army that are already spawned. and are alive.
        foreach(BossArmy bossArmy in SpawnedbossArmies){
            if(bossArmy.IsAlive()){
                bossArmy.TargetLocked(currentTarget);
            }
        }

        List<BossArmyManager.BossArmies> activeArmies = bossArmyManager.GetActiveAndHomeBossArmies();
        //this will give me all the armies that are alive and home
        foreach (BossArmyManager.BossArmies bossArmy in activeArmies)
        {
            BossArmy bossArmyComponent = Instantiate(BossArmyPrefab, SpawnPoint.transform.position, 
            Quaternion.identity).GetComponent<BossArmy>();
            bossArmyComponent.Dependency(KingDom, bossArmy.id,bossArmyManager, SpawnPoint, this);
            bossArmyComponent.TargetLocked(currentTarget);
            SpawnedbossArmies.Add(bossArmyComponent);
            bossArmy.isReturned = false;
        }

        }
    void ReturnArmies(){
        foreach (BossArmy bossArmy in SpawnedbossArmies){
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
        // bossArmies[bossArmy.ArmyID]=null;
        SpawnedbossArmies.Remove(bossArmy);
        bossArmyManager.ArmyReturned(bossArmy.ArmyID);
        Destroy(bossArmy.gameObject);
    }
    public void OnDefeat(){
        Debug.Log("Boss Defeated,Give Rewards");
        foreach (BossArmy bossArmy in SpawnedbossArmies){
            
            if(bossArmy){

                Destroy(bossArmy.gameObject);
            }

        Destroy(gameObject);
    }}
}
