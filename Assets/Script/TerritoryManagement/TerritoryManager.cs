using System.Collections;
using System.Collections.Generic;
using UnityEngine;


   public class TowerPointPlacer : MonoBehaviour
{
    public GameObject towerPointPrefab; // Assign an empty prefab or marker
    public int totalPoints = 30;
    public float initialRadius = 5f;
    public float radiusStep = 5f;
    public float minSpacing = 2f; // spacing between points on same ring
    public Vector3 center = Vector3.zero;

    private GameObject[] towerPoints; // Now public and will store spawned points

    void Start()
    {
        towerPoints = new GameObject[totalPoints]; // initialize array
        PlacePointsInCircle();
        ProvidePointsToEnemySpawner(); // you already had this call
    }

    void PlacePointsInCircle()
    {
        int spawned = 0;
        int currentRing = 0;

        while (spawned < totalPoints)
        {
            float radius = initialRadius + currentRing * radiusStep;
            float circumference = 2 * Mathf.PI * radius;
            int pointsInThisRing = Mathf.FloorToInt(circumference / minSpacing);

            // Don't spawn more than needed
            pointsInThisRing = Mathf.Min(pointsInThisRing, totalPoints - spawned);

            float angleStep = 360f / pointsInThisRing;

            for (int i = 0; i < pointsInThisRing && spawned < totalPoints; i++)
            {
                float angle = i * angleStep * Mathf.Deg2Rad;

                float x = center.x + radius * Mathf.Cos(angle);
                float z = center.z + radius * Mathf.Sin(angle);
                Vector3 spawnPos = new Vector3(x, center.y, z);

                GameObject point = Instantiate(towerPointPrefab, spawnPos, Quaternion.identity, transform);
                towerPoints[spawned] = point; // ✅ Store the point
                spawned++;
            }

            currentRing++;
        }
    }

    


    void ProvidePointsToEnemySpawner(){
        EnemyWatchTowerSpawner[] allSpawners = FindObjectsOfType<EnemyWatchTowerSpawner>();
        foreach (EnemyWatchTowerSpawner spawner in allSpawners)
        {
            spawner.GetAllTowerPoint(towerPoints);
        // GameObject obj = spawner.gameObject;
        // Debug.Log("Found EnemySpawner on: " + obj.name);
        }
    }
}
