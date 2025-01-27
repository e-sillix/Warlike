using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    [SerializeField] private GameObject woodMinePrefab,grainMinePrefab,stoneMinePrefab;
    // // Update is called once per frame
    // void Update()
    // {
    //  if(Input.GetKeyDown(KeyCode.R)){
    //     SpawnMine();
    //  }   
    // }
    // void SpawnMine(){
    //     TheMine theMine=Instantiate(woodMinePrefab,spawnPoint.transform.position, Quaternion.identity).
    //     GetComponent<TheMine>();
    //     theMine.InitializeMineStats(2);
    // }
    public GameObject spawnPointObject, parentObject;      // The parent GameObject to hold spawned objects
    public float minRadius, maxRadius;       // Maximum radius for spawning
    public int numberOfSpawnPoints = 10; // Number of spawn points
    // public int prefabsPerPoint = 3;      // Number of prefabs to spawn per point
    // public float prefabSpacing = 10f;    // Spacing between prefabs

    void Start()
    {
        // Check if the necessary objects are assigned
        if (spawnPointObject == null)
        {
            Debug.LogError("Spawn Point Object is not assigned!");
            return;
        }

        if (parentObject == null)
        {
            Debug.LogError("Parent Object is not assigned!");
            return;
        }

        SpawnAroundSpawnPoint();
    }

    void SpawnAroundSpawnPoint()
    {
        Vector3 centerPoint = spawnPointObject.transform.position; // Get the spawn point position

        for (int i = 0; i < numberOfSpawnPoints; i++)
        {            
            SpawnPrefabsAtPoint(centerPoint);
        }
    }

    void SpawnPrefabsAtPoint(Vector3 centerPoint)
    {
        // Offset for each prefab
        Vector3 woodPosition = GenerateSpawnPosition(centerPoint);
        Vector3 grainPosition = GenerateSpawnPosition(centerPoint); // 10 units on x-axis
        Vector3 stonePosition = GenerateSpawnPosition(centerPoint); // 20 units on x-axis

        // Spawn Wood Mine
        GameObject woodMine = Instantiate(woodMinePrefab, woodPosition, Quaternion.identity);
        woodMine.transform.SetParent(parentObject.transform, true);

        // Spawn Grain Mine
        GameObject grainMine = Instantiate(grainMinePrefab, grainPosition, Quaternion.identity);
        grainMine.transform.SetParent(parentObject.transform, true);

        // Spawn Stone Mine
        GameObject stoneMine = Instantiate(stoneMinePrefab, stonePosition, Quaternion.identity);
        stoneMine.transform.SetParent(parentObject.transform, true);

        woodMine.GetComponent<TheMine>().MineDependency(this);
        grainMine.GetComponent<TheMine>().MineDependency(this);
        stoneMine.GetComponent<TheMine>().MineDependency(this);
        
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
    public void AMineIsFinsihed(string mineType){
        Debug.Log("Mine Type "+mineType+" Is Finished.");
        if(mineType == "wood"){
            SpawnAMineType(woodMinePrefab);
        }
        else if(mineType == "grain"){
            SpawnAMineType(grainMinePrefab);
        }
        else if(mineType == "stone"){
            SpawnAMineType(stoneMinePrefab);
        }
    }
    void SpawnAMineType(GameObject ChoosenMinePrefab){
        Vector3 centerPoint = spawnPointObject.transform.position;
        Vector3 SpawnPosition = GenerateSpawnPosition(centerPoint);
        GameObject ChoosenMine = Instantiate(ChoosenMinePrefab, SpawnPosition, Quaternion.identity);
        ChoosenMine.transform.SetParent(parentObject.transform, true);
        ChoosenMine.GetComponent<TheMine>().MineDependency(this);
    }
}
