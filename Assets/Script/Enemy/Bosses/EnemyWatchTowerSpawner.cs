using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyWatchTowerSpawner : MonoBehaviour
{
    public GameObject towerPrefab;                    // Tower to build
    public float checkRadius = 1f,checkRadiusPlayer;                    // Radius to check for existing towers
    // public string towerLayerName = "Tower";           // Layer name for existing towers
    public float minDistanceToCenter = 10f
    ,maxDistanceAllowedForTower,TowerToTowerRange;
               // 🟢 Don't place towers closer than this

    private List<Transform> towerPoints = new List<Transform>();
    private int towerLayerMask;
    private Color towerColor;
    [SerializeField]private float TimeGapOnEachTower;
    [SerializeField]private TroopsExpeditionManager troopsExpeditionManager;
    [SerializeField]private TowerPointPlacer towerPointPlacer; // Reference to the TowerPointPlacer script
    
    [SerializeField]private GameObject TowerParent;
    private int BossId;
    private GameObject[] Towers;
    // private List<GameObject> towerList = new List<GameObject>(); // 🧱 Track built towers

    [SerializeField]private Vector3 playerTower;
    private Coroutine towerBuilding;

    // [SerializeField]private GameObject notifier;
    void Start()
    {
        towerLayerMask = 1 << LayerMask.NameToLayer("Tower");
        towerColor = GetComponent<Boss>().BossColor;
        BossId = GetComponent<Boss>().ReturnBossId();
    }

    public void GetAllTowerPoint(GameObject[] Tpoints)
    {
        //by pointplacer
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
        if(towerBuilding!=null){
            StopCoroutine(towerBuilding);
            towerBuilding=null;
        }
        towerBuilding=StartCoroutine(BuildTowersOneByOne());
    }

   IEnumerator BuildTowersOneByOne()
{
    foreach (Transform point in towerPoints)
    {
    
        float distToCenter = Vector3.Distance(transform.position, point.position);

        // 🔴 Skip if point is too close to boss
        // if (distToCenter < minDistanceToCenter)
        // {
        //     Debug.Log("skip 0");
        //     continue;
        // }
        if(!CheckMinimumLimitDistance(distToCenter)){
            // Debug.Log("tower too close");
            continue;
            }
        if( !MaxDistanceAndTower(distToCenter,point)){
            // Debug.Log("tower too far or not same boss tower nearby");
            continue;
        }
        if (!CheckBotTowerOnThatPoint(point)){
            // Debug.Log("a bot tower already here");
            continue;
        }

        if(!CheckForPlayerTowerOnThatPointAndRange(point)){
            // Debug.Log("tower already here");
            continue;
        }

       

       
        yield return new WaitForSeconds(TimeGapOnEachTower);
        // ✅ Place tower
        GameObject tower = Instantiate(towerPrefab, point.position, Quaternion.identity);
        tower.transform.SetParent(TowerParent.transform);

        tower.GetComponent<TowerInstance>().EnemyTowerDependency(
            GetComponent<Boss>().ReturnBossId(), towerColor,gameObject
        );

        tower.GetComponent<TowerCombat>().Dependency(troopsExpeditionManager, towerPointPlacer);
        // Debug.Log("✅ Tower placed");

        // towerList.Add(tower); // ✅ Add to list

        Debug.Log("✅ Tower placed");
        
    }
    // Towers = towerList.ToArray(); // ✅ Finalize list to array

    Debug.Log("🏁 Finished placing towers.");
    CheckAllTheTowerPointForExpansion();
}

void CheckAllTheTowerPointForExpansion()
{
    foreach (Transform point in towerPoints)
    {
        GameObject otherTower = CheckForOtherTowerInRange(point);
        if (otherTower != null)
        {
            Debug.Log("Expansion possible.");
            // Instantiate(notifier, otherTower.transform.position, Quaternion.identity);
            GetComponent<Boss>().TriggerExpansionOnEnemyTower();
            return;
        }
    }

    Debug.Log("Expansion isn't possible.");
}

public GameObject TowerToDestroy(){
    //called by boss to get tower to destroy
     foreach (Transform point in towerPoints)
    {
        GameObject otherTower = CheckForOtherTowerInRange(point);
        if (otherTower != null)
        {
            Debug.Log("Expansion possible.");
            // Instantiate(notifier, otherTower.transform.position, Quaternion.identity);
            
            return otherTower;
        }
    }
    return null;
}
GameObject CheckForOtherTowerInRange(Transform point)
{
    bool collidingBase = false;
    bool collidingEnemyTower = false;
    GameObject enemyTower = null;

    // Check what's in range
    Collider[] hits = Physics.OverlapBox(point.position, playerTower, Quaternion.identity, towerLayerMask);

    foreach (Collider coll in hits)
    {
        TowerInstance towerInstance = coll.GetComponentInParent<TowerInstance>();

        if (towerInstance == null)
        {
            // Likely a player base (has collider but no tower script)
            collidingBase = true;
        }
        else if (towerInstance.ReturnBossId() != BossId)
        {
            // Found enemy tower
            collidingEnemyTower = true;
            enemyTower = towerInstance.gameObject;
        }
    }

    // Only allow expansion if no base but enemy tower exists
    if (!collidingBase && collidingEnemyTower)
    {
        return enemyTower;
    }

    return null;
}



bool CheckMinimumLimitDistance(float distToCenter){
    
    return distToCenter>minDistanceToCenter;
}

bool MaxDistanceAndTower(float distToCenter,Transform point){
    if (distToCenter > maxDistanceAllowedForTower)
        {
            // bool foundSameBossNearby = false;
//this one searching for nearby tower.
            Collider[] nearby = Physics.OverlapSphere(point.position, TowerToTowerRange,
            towerLayerMask);
            foreach (Collider coll in nearby)
            {
                TowerInstance t = coll.GetComponentInParent<TowerInstance>();
                if (t != null && t.ReturnBossId() == BossId)
                {
                    // foundSameBossNearby = true;
                    return true;
                }
            }
            return false;

            // if (!foundSameBossNearby)
            // {
            //     Debug.Log("skip 1 — no nearby tower of same boss");
            //     continue;
            // }
        }
        else{
            ///+++++++++??????
            return true;
        }
}
bool CheckBotTowerOnThatPoint(Transform point){
    Collider[] hits=Physics.OverlapSphere(point.position, 10f, towerLayerMask);

        if (hits.Length > 0)
        {
            // Debug.Log("skip 2 — a Bot tower already here");
            return false;
        }
        else
        {
            // Debug.Log("no tower here");
            return true;
        }

}
bool CheckForPlayerTowerOnThatPointAndRange(Transform point){
    Collider[] playerHits = Physics.OverlapBox(point.position, playerTower, 
        Quaternion.identity, towerLayerMask);
        if(playerHits.Length<=0){
            // Debug.Log("nothing");
            return true;
        }
        foreach (Collider coll in playerHits)
        {
            // Debug.Log("hmm");
            TowerInstance toweri = coll.GetComponentInParent<TowerInstance>();
            if(toweri){
                // Debug.Log("Tower near");
            }
            else{
                //for player base ,which has layer tower on bottom.
                return false;
            }
            if (toweri != null &&toweri.IsTowerBelongToPlayer())
            {
                // Debug.Log("skip 3 — nearby player tower");
                // yield return null;
                return false;
            }
            
        }
        return true;
}


// public void RemoveTowerFromList(GameObject tower)
//     {
//         if (towerList.Contains(tower))
//         {
//             towerList.Remove(tower);
//             Towers = towerList.ToArray(); // 🔁 Keep array in sync
//             Debug.Log("🚫 Tower removed from list.");
//         }
//         else
//         {
//             Debug.LogWarning("⚠️ Tried to remove a tower that isn't in the list.");
//         }
//     }
    // public GameObject[] GetTowers()
    // {
    //     return Towers;
    // }
}
