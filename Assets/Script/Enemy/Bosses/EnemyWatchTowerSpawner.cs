using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWatchTowerSpawner : MonoBehaviour
{
    public GameObject towerPrefab;                    // Tower to build
    public float checkRadius = 1f;                    // How far to check for existing tower
    public string towerLayerName = "Tower";           // Name of the tower layer

    private List<Transform> towerPoints = new List<Transform>();
    private int towerLayerMask;

    void Start()
    {
        towerLayerMask = 1 << LayerMask.NameToLayer(towerLayerName);
    }

    public void GetAllTowerPoint(GameObject[] Tpoints)
    {
        foreach (GameObject point in Tpoints)
        {
            towerPoints.Add(point.transform);
        }

        // Sort points by distance to this enemy (center)
        towerPoints.Sort((a, b) =>
            Vector3.Distance(transform.position, a.position)
            .CompareTo(Vector3.Distance(transform.position, b.position))
        );

        StartCoroutine(BuildTowersOneByOne());
    }

    IEnumerator BuildTowersOneByOne()
    {
        foreach (Transform point in towerPoints)
        {
            // Check for existing tower at this point
            Collider[] hits = Physics.OverlapSphere(point.position, checkRadius, towerLayerMask);
            if (hits.Length == 0) // No tower nearby
            {
                GameObject tower = Instantiate(towerPrefab, point.position, Quaternion.identity);
                tower.transform.SetParent(point);
                Debug.Log("Spawned tower at: " + point.position);

                yield return new WaitForSeconds(1f); // Wait before building next tower
            }
        }

        Debug.Log("Finished placing towers.");
    }
}
