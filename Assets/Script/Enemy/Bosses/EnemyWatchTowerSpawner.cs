using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWatchTowerSpawner : MonoBehaviour
{
    public GameObject towerPrefab;                    // Tower to build
    public float checkRadius = 1f,checkRadiusPlayer;                    // Radius to check for existing towers
    public string towerLayerName = "Tower";           // Layer name for existing towers
    public float minDistanceToCenter = 10f
    ,maxDistanceAllowedForTower,TowerToTowerRange;
               // 🟢 Don't place towers closer than this

    private List<Transform> towerPoints = new List<Transform>();
    private int towerLayerMask;
    private Color towerColor;
    [SerializeField]private float TimeGapOnEachTower;
    [SerializeField]private TroopsExpeditionManager troopsExpeditionManager;
    [SerializeField]private TowerPointPlacer towerPointPlacer; // Reference to the TowerPointPlacer script
    private int BossId;

    [SerializeField]private Vector3 playerTower;
    void Start()
    {
        towerLayerMask = 1 << LayerMask.NameToLayer(towerLayerName);
        towerColor = GetComponent<Boss>().BossColor;
        BossId = GetComponent<Boss>().ReturnBossId();
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
        float distToCenter = Vector3.Distance(transform.position, point.position);

        // 🔴 Skip if point is too close to boss
        if (distToCenter < minDistanceToCenter)
        {
            Debug.Log("skip 0");
            continue;
        }

        // 🔐 Beyond max distance? Only allow if near same Boss's tower
        if (distToCenter > maxDistanceAllowedForTower)
        {
            bool foundSameBossNearby = false;

            Collider[] nearby = Physics.OverlapSphere(point.position, TowerToTowerRange, towerLayerMask);
            foreach (Collider coll in nearby)
            {
                TowerInstance t = coll.GetComponentInParent<TowerInstance>();
                if (t != null && t.ReturnBossId() == BossId)
                {
                    foundSameBossNearby = true;
                    break;
                }
            }

            if (!foundSameBossNearby)
            {
                Debug.Log("skip 1 — no nearby tower of same boss");
                continue;
            }
        }

        // 🔴 Skip if a tower already exists at the point
        Collider[] hits = Physics.OverlapSphere(point.position, checkRadius, towerLayerMask);
        if (hits.Length > 0)
        {
            Debug.Log("skip 2 — tower already here");
            continue;
        }

        // 🔴 Skip if nearby tower belongs to player
       Collider[] playerHits = Physics.OverlapBox(transform.position, playerTower, Quaternion.identity, towerLayerMask);
        foreach (Collider coll in playerHits)
        {
            TowerInstance toweri = coll.GetComponentInParent<TowerInstance>();
            if (toweri != null && toweri.IsTowerBelongToPlayer())
            {
                Debug.Log("skip 3 — nearby player tower");
                yield return null;
                continue;
            }
        }

        // ✅ Place tower
        GameObject tower = Instantiate(towerPrefab, point.position, Quaternion.identity);
        tower.transform.SetParent(point);

        tower.GetComponent<TowerInstance>().EnemyTowerDependency(
            GetComponent<Boss>().ReturnBossId(), towerColor
        );

        tower.GetComponent<TowerCombat>().Dependency(troopsExpeditionManager, towerPointPlacer);
        Debug.Log("✅ Tower placed");

        yield return new WaitForSeconds(TimeGapOnEachTower);
    }

    Debug.Log("🏁 Finished placing towers.");
}


}
