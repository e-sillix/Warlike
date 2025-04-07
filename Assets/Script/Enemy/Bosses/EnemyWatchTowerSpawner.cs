using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWatchTowerSpawner : MonoBehaviour
{
    public GameObject towerPrefab;                    // Tower to build
    public float checkRadius = 1f;                    // Radius to check for existing towers
    public string towerLayerName = "Tower";           // Layer name for existing towers
    public float minDistanceToCenter = 10f;           // 🟢 Don't place towers closer than this

    private List<Transform> towerPoints = new List<Transform>();
    private int towerLayerMask;
    private Color towerColor;

    void Start()
    {
        towerLayerMask = 1 << LayerMask.NameToLayer(towerLayerName);
        towerColor = GetComponent<Boss>().BossColor;
    }

    public void GetAllTowerPoint(GameObject[] Tpoints)
    {
        foreach (GameObject point in Tpoints)
        {
            towerPoints.Add(point.transform);
        }

        // Sort points by distance from this enemy (center)
        towerPoints.Sort((a, b) =>
            Vector3.Distance(transform.position, a.position)
            .CompareTo(Vector3.Distance(transform.position, b.position))
        );
        towerColor = GetComponent<Boss>().BossColor;
        StartCoroutine(BuildTowersOneByOne());
    }

    IEnumerator BuildTowersOneByOne()
    {
        foreach (Transform point in towerPoints)
        {
            // 🔴 Skip if point is too close to center
            if (Vector3.Distance(transform.position, point.position) < minDistanceToCenter)
                continue;

            // 🔴 Skip if tower already nearby
            Collider[] hits = Physics.OverlapSphere(point.position, checkRadius, towerLayerMask);
            if (hits.Length > 0) continue;

            // 🏗️ Spawn tower
            GameObject tower = Instantiate(towerPrefab, point.position, Quaternion.identity);
            tower.transform.SetParent(point);
            
            // Debug.Log(towerColor);
            // 🎨 Set tower color based on boss
            tower.GetComponent<TowerInstance>().AssignColor(towerColor);

            yield return new WaitForSeconds(1f); // ⏱️ Delay before next spawn
        }

        Debug.Log("Finished placing towers.");
    }
}
