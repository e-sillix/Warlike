using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreepSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject SpawnPoint, CreepPrefab, ParentObject;
    [SerializeField] private TroopsExpeditionManager troopsExpeditionManager;
    // Start is called before the first frame update
    public float minRadius, maxRadius;       // Maximum radius for spawning
    public int numberOfPrefabs = 10;     // Number of prefabs to spawn
    public Quaternion spawnRotation = Quaternion.identity; // Desired rotation for spawned prefabs
    void Start()
    {
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
        creep.GetComponent<TheCreep>().Dependency(this);
        creep.transform.SetParent(ParentObject.transform);
        creep.transform.localRotation = Quaternion.identity;
        // Debug.Log("New Creep Instantiated!!!!");
    }
    

    Vector3 GenerateSpawnPosition(Vector3 centerPoint)
    {
        // Random angle in radians
        float angle = Random.Range(0f, Mathf.PI * 2f);

        // Random distance within the allowed range
        float distance = Random.Range(minRadius, maxRadius);

        // Calculate x and z using polar coordinates
        float x = centerPoint.x + distance * Mathf.Cos(angle);
        float z = centerPoint.z + distance * Mathf.Sin(angle);

        // Keep y the same as the spawn point's y-coordinate
        float y = centerPoint.y;

        return new Vector3(x, y, z);
    }
    public void CreepDefeated(){
        //called by a creep when defeated
        // Debug.Log("New Creeps need to be Spawned!!!!!");
        SpawnAroundSpawnPoint();
    }
}
