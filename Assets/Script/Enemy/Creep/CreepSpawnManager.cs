using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreepSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject SpawnPoint, CreepPrefab, ParentObject;
    // [SerializeField] private TroopsExpeditionManager troopsExpeditionManager;
    [SerializeField]private TroopsStatsManager troopsStatsManager;

    [SerializeField]private RewardManager rewardManager;
    // Start is called before the first frame update
    public float minRadius, maxRadius;       // Maximum radius for spawning
    public int numberOfPrefabs = 10;     // Number of prefabs to spawn
    public Quaternion spawnRotation = Quaternion.identity; // Desired rotation for spawned prefabs
    
    private int creepBatch=0;
    
    void Start()
    {
        // PlayerPrefs.DeleteKey("creepBatch");

        creepBatch=PlayerPrefs.GetInt("creepBatch");
        // Ensure the spawn point object is assigned
        if (SpawnPoint == null)
        {
            Debug.LogError("Spawn Point Object is not assigned!");
            return;
        }
         if (ParentObject == null)
        {
            Debug.LogError("Parent Object is not assigned!");
            return;
        }

        MassSpawnAroundSpawnPoint();
    }

    void MassSpawnAroundSpawnPoint()
    {
        Vector3 centerPoint = SpawnPoint.transform.position; // Get the spawn point position

        for (int i = 0; i < numberOfPrefabs; i++)
        {
            InstantiateCreep(centerPoint);
        }
    }

    void SpawnAroundSpawnPoint(){
       Vector3 centerPoint = SpawnPoint.transform.position; // Get the spawn point position
       InstantiateCreep(centerPoint);
    }
    void InstantiateCreep(Vector3 centerPoint){        
        Vector3 spawnPosition = GenerateSpawnPosition(centerPoint);
        GameObject creep=Instantiate(CreepPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Limit:"+(creepBatch*10+1)+"/"+((creepBatch+1)*10+1));
        creep.GetComponent<TheCreep>().Dependency(this,rewardManager, Random.Range(creepBatch*10+1,
         (creepBatch+1)*10+1),troopsStatsManager);
        // creep
        creep.transform.SetParent(ParentObject.transform);
        creep.transform.localRotation = Quaternion.identity;
        // Debug.Log("New Creep Instantiated!!!!");
    }
    

    Vector3 GenerateSpawnPosition(Vector3 centerPoint)
{
    int maxAttempts = 10;
    float checkRadius = 10f;

    // Ignore layer "Ground"
    int groundLayer = LayerMask.NameToLayer("Ground");
    int ignoreGroundMask = ~(1 << groundLayer); // everything except Ground

    for (int i = 0; i < maxAttempts; i++)
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float distance = Random.Range(minRadius, maxRadius);

        float x = centerPoint.x + distance * Mathf.Cos(angle);
        float z = centerPoint.z + distance * Mathf.Sin(angle);
        float y = centerPoint.y;

        Vector3 candidate = new Vector3(x, y, z);

        // Only check for colliders that are NOT on the Ground layer
        Collider[] colliders = Physics.OverlapSphere(candidate, checkRadius, ignoreGroundMask);

        bool isClear = true;
        foreach (Collider col in colliders)
        {
            if (!col.isTrigger)
            {
                isClear = false;
                break;
            }
        }

        if (isClear)
        {
            return candidate;
        }
    }

    Debug.LogWarning("❌ Could not find clear spawn spot. Returning center point.");
    return centerPoint;
}


    public void CreepDefeated(int level){
        //called by a creep when defeated
        // Debug.Log("New Creeps need to be Spawned!!!!!");
        SpawnAroundSpawnPoint();

        if(level%10==0){
            creepBatch=level/10;
            PlayerPrefs.SetInt("creepBatch", level/10);
            PlayerPrefs.Save();
            MassDespawnTroops();
            // SpawnAroundSpawnPoint();
            MassSpawnAroundSpawnPoint();
        }
    }

   void MassDespawnTroops()
{
    TheCreep[] allCreeps = FindObjectsOfType<TheCreep>(); // Find all objects with TheCreep component

    foreach (TheCreep creep in allCreeps)
    {
        Destroy(creep.gameObject); // Destroy each GameObject
    }
}

}
