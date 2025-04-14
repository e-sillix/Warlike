using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPointPlacer : MonoBehaviour
{
    public GameObject towerPointPrefab; // Assign an empty prefab or marker
    public int totalPoints = 30;
    public float gridSpacing = 2f; // distance between grid points
    public Vector3 center = Vector3.zero;

    private GameObject[] towerPoints; // Now public and will store spawned points

    private Coroutine TowerExpansion;
    void Start()
    {
        towerPoints = new GameObject[totalPoints]; // initialize array
        PlacePointsInGrid();
        // ProvidePointsToEnemySpawner(); // you already had this call
        TowerExpansion=StartCoroutine(TriggerTowerExpansion());
    }

    void PlacePointsInGrid()
    {
        int spawned = 0;

        // Calculate grid dimensions as square (√N x √N)
        int gridSize = Mathf.CeilToInt(Mathf.Sqrt(totalPoints));
        float startX = center.x - (gridSize - 1) * gridSpacing * 0.5f;
        float startZ = center.z - (gridSize - 1) * gridSpacing * 0.5f;

        for (int x = 0; x < gridSize && spawned < totalPoints; x++)
        {
            for (int z = 0; z < gridSize && spawned < totalPoints; z++)
            {
                Vector3 spawnPos = new Vector3(
                    startX + x * gridSpacing,
                    center.y,
                    startZ + z * gridSpacing
                );

                GameObject point = Instantiate(towerPointPrefab, spawnPos, Quaternion.identity, transform);
                towerPoints[spawned] = point; // ✅ Store the point
                spawned++;
            }
        }
    }

    // public void ProvidePointsToEnemySpawner()
    // {
    //     EnemyWatchTowerSpawner[] allSpawners = FindObjectsOfType<EnemyWatchTowerSpawner>();
    //     foreach (EnemyWatchTowerSpawner spawner in allSpawners)
    //     {
    //         spawner.GetAllTowerPoint(towerPoints);
    //     }
    // }
    
    public void TowerIsDestroyed(GameObject tower){
        
        Destroy(tower);
        if(TowerExpansion!=null){
            StopCoroutine(TowerExpansion);
            TowerExpansion=null;
        }
        TowerExpansion=StartCoroutine(TriggerTowerExpansion());
        // EnemyWatchTowerSpawner[] allSpawners = FindObjectsOfType<EnemyWatchTowerSpawner>();
        // foreach (EnemyWatchTowerSpawner spawner in allSpawners)
        // {
        //     spawner.GetAllTowerPoint(towerPoints);
        // }
    }
    IEnumerator TriggerTowerExpansion(){
        yield return new WaitForSeconds(3f); 
         EnemyWatchTowerSpawner[] allSpawners = FindObjectsOfType<EnemyWatchTowerSpawner>();
        foreach (EnemyWatchTowerSpawner spawner in allSpawners)
        {
            spawner.GetAllTowerPoint(towerPoints);
        }
    }
}
