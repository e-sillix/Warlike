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


    private ArmyDetector armyDetector;

    private List<BossArmy> SpawnedbossArmies = new List<BossArmy>();
    private GameObject currentTarget;
    private BossArmyManager bossArmyManager ;

   
    void Start()
    {
       
        // Start the Coroutine when the game begins
        armyDetector=GetComponent<ArmyDetector>();
       
        bossArmyManager = GetComponent<BossArmyManager>();
        
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

        }
    

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

    public void TriggerDetection(){
        //when player Armies get defeated.directly by TheUnit
        armyDetector.TriggerDetectionOnce();
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
