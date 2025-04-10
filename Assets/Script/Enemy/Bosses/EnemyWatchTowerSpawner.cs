using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWatchTowerSpawner : MonoBehaviour
{
    public GameObject towerPrefab;                    // Tower to build
    public float checkRadius = 1f,checkRadiusPlayer;                    // Radius to check for existing towers
    public string towerLayerName = "Tower";           // Layer name for existing towers
    public float minDistanceToCenter = 10f
    ,maxDistanceAllowedForTower;           // 🟢 Don't place towers closer than this

    private List<Transform> towerPoints = new List<Transform>();
    private int towerLayerMask;
    private Color towerColor;
    [SerializeField]private float TimeGapOnEachTower;
    [SerializeField]private TroopsExpeditionManager troopsExpeditionManager;
    [SerializeField]private TowerPointPlacer towerPointPlacer; // Reference to the TowerPointPlacer script

    void Start()
    {
        towerLayerMask = 1 << LayerMask.NameToLayer(towerLayerName);
        towerColor = GetComponent<Boss>().BossColor;
    }

    public void GetAllTowerPoint(GameObject[] Tpoints)
    {
        towerPoints = new List<Transform>();
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
        if (Vector3.Distance(transform.position, point.position) < minDistanceToCenter||
            Vector3.Distance(transform.position, point.position) > maxDistanceAllowedForTower)
           {
            Debug.Log("skip 0");
             continue;}
        
        // 🔴 Check for any colliders nearby that are on the Tower layer
        Collider[] hits = Physics.OverlapSphere(point.position, checkRadius, towerLayerMask);
        if (hits.Length > 0)
        {
            // Debug.Log();
            Debug.Log("1 skip");
            // ✅ Something is already there (on Tower layer), skip
            continue;
        }

        // 🔴 Check if any nearby tower belongs to the player
        Collider[] playerHits = Physics.OverlapSphere(point.position, checkRadiusPlayer, towerLayerMask);
        bool belongsToPlayer = false;

        foreach (Collider coll in playerHits)
        {
            TowerInstance toweri = coll.gameObject.GetComponentInParent<TowerInstance>();
            if (toweri != null && toweri.IsTowerBelongToPlayer())
            {
                belongsToPlayer = true;
                Debug.Log("2 skip");
                break; // No need to check further
            }
        }

        if (belongsToPlayer)
        {
            Debug.Log("3 skip");
            continue; // 🚫 Skip this point if player owns nearby tower
        }

        // 🏗️ Spawn tower
        GameObject tower = Instantiate(towerPrefab, point.position, Quaternion.identity);
        tower.transform.SetParent(point);

        // 🎨 Assign color from the boss
        tower.GetComponent<TowerInstance>().AssignColor(towerColor);
        tower.GetComponent<TowerCombat>().Dependency(troopsExpeditionManager,towerPointPlacer);
        Debug.Log("4 skip");
        yield return new WaitForSeconds(TimeGapOnEachTower); // ⏱️ Delay before spawning the next one
    }

    Debug.Log("✅ Finished placing towers.");
}

}
