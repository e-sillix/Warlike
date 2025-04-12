using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private int troopLevel, agressiveLevel,armySize;
    [SerializeField]private int BossId;
    public Color BossColor;
    [SerializeField] private int [] Armies;//Armies stores number of troops 
    //of each type and also how many boss army are there.
    [SerializeField] private string KingDom;
    [SerializeField] private string[] Rewards, Treasure;
    //commander maybe.
    [SerializeField] private GameObject BossArmyPrefab,SpawnPoint;

    [SerializeField]private float LandAdvancingRate;

    public float detectionInterval = 5f;

    private GameObject[] playerArmies=new GameObject[5]; // Array to store the detected units

    private ArmyDetector armyDetector;
    // private BossArmy[] bossArmies;//this will be used to store the spawned armies.

    private List<BossArmy> SpawnedbossArmies = new List<BossArmy>();
    // private BossArmy[] SpawnedbossArmies;
    private GameObject currentTarget;
    private BossArmyManager bossArmyManager ;
    private bool isPatrolling=false,isChasing=false;

    private GameObject[] Towers;
    private bool isTargetArmy;
    // private GameObject[] TresspassingArmy;
    List<GameObject> TresspassingArmy = new List<GameObject>();
    List<GameObject> BreachingArmy = new List<GameObject>();
    List<GameObject> PreBreachingArmy = new List<GameObject>();
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


        StartPatrol();
    }

    public int ReturnBossId(){
        return BossId;
    }

    public void StartPatrol(){
        Debug.Log("Patrolling started.");
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
            // Debug.Log("1");
            playerArmies=new GameObject[5];
            //now fill it with the detected units present.
            playerArmies=armyDetector.FindNearbyUnits(); // Find the nearby units
            if(playerArmies[0]!=null){
                Debug.Log("Found Tresspasser.");
                currentTarget=playerArmies[0];
                // PlayerUnitFound();
                StartCoroutine(DetectCurrentTarget());
                isPatrolling=false;
                yield break; // This stops the coroutine
            }
            else{
                // ReturnArmies();
                // ReturnInjuredArmies();

                //Null all the armies target.
                if(isPatrolling==false){
                    isPatrolling=true;

                    BackToPatrol();
                }
                // TargetNulledForArmies();
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
                // Debug.Log("Target not found in the range.");
                // Debug.Log("Starting normal search");
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
                // bossArmy.TargetAArmy(true);
            }
        }

        // List<BossArmyManager.BossArmies> activeArmies = bossArmyManager.GetActiveAndHomeBossArmies();
        // //this will give me all the armies that are alive and home
        // foreach (BossArmyManager.BossArmies bossArmy in activeArmies)
        // {
        //     BossArmy bossArmyComponent = Instantiate(BossArmyPrefab, SpawnPoint.transform.position, 
        //     Quaternion.identity).GetComponent<BossArmy>();
        //     bossArmyComponent.Dependency(KingDom, bossArmy.id,bossArmyManager, SpawnPoint, this);
        //     bossArmyComponent.TargetLocked(currentTarget);
        //     SpawnedbossArmies.Add(bossArmyComponent);
        //     bossArmy.isReturned = false;
        // }

        }
    // void ReturnArmies(){
    //     foreach (BossArmy bossArmy in SpawnedbossArmies){
    //         // if (bossArmy.KingDom == KingDom)
    //         // {
    //         if(bossArmy){

    //             bossArmy.ReturnBase();
    //         }
    //         // }
    //     }    
    // }
    // void ReturnInjuredArmies(){
    //      foreach (BossArmy bossArmy in SpawnedbossArmies){
    //         // if (bossArmy.KingDom == KingDom)
    //         // {
    //         if(bossArmy){
    //             if(bossArmy.ReturnIsInjured()){
    //                 bossArmy.ReturnBase();
    //             }
    //             // bossArmy.ReturnBase();
    //         }
    //         // }
    //     }  
    // }

    void TargetNulledForArmies(){
         foreach(BossArmy bossArmy in SpawnedbossArmies){
            if(bossArmy.IsAlive()){
                bossArmy.TargetLocked(null);
            }
        }
    }

    void BackToPatrol(){
        foreach(BossArmy bossArmy in SpawnedbossArmies){
            if(bossArmy.IsAlive()){
                bossArmy.TargetLeft();
            }
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

    // public void ArmyTresspassing(GameObject army){
    //     if (!TresspassingArmy.Contains(army))
    // {
    //     TresspassingArmy.Add(army);
    // }

    // // Check if currentTarget is still breaching this base
    // if (currentTarget != null)
    // {
    //     int armyBossId = currentTarget.GetComponent<TheUnit>().GetBossId(); 
    //     // you should have this method
    //     if (armyBossId != BossId)
    //     {
    //         TresspassingArmy.Remove(currentTarget);
    //         currentTarget = null;
    //     }
    // }

    // // If no valid target, assign new one from list
    // if (currentTarget == null)
    // {
    //    currentTarget=TresspassingArmy[0];
    //    DirectingArmies();
    // }

    // }

    // IEnumerator RefreshTresspassing(){
    //     while(true){
    //         yield return new WaitForSeconds(5f);
    //         if(currentTarget!=null){
    //             if(BossId==currentTarget.GetComponent<TheUnit>().GetBossId()){
                    
    //             }
    //         }
    //     }
    // }

    void DirectingArmies(){
        // currentTarget=TresspassingArmy[0];
         foreach(BossArmy bossArmy in SpawnedbossArmies){
            if(bossArmy.IsAlive()){
                bossArmy.TargetLocked(currentTarget);
                // bossArmy.TargetAArmy(true);
            }
        }
    }

    public void ReportingArmies(GameObject[] breachingArmies)
{
    // If the current target is still breaching, keep targeting it
    if (currentTarget != null && System.Array.Exists(breachingArmies, a => a == currentTarget))
    {
        // Target is still in the list, do nothing
        return;
    }

    // If target is gone or null, pick a new one
    if (breachingArmies.Length > 0)
    {
        currentTarget = breachingArmies[0]; // pick the first one, or use custom logic to pick
        DirectingAllArmies(); // start attacking or pursuing new target
    }
    else
    {
        // No armies breaching anymore
        currentTarget = null;
        // You can call a patrol function or idle logic here
        BackToPatrol();
    }
}

}
