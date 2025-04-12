using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTerritoryCollider : MonoBehaviour
{

    private int BossId;
    private TheUnit theUnit;
    void Start()
    {
        StartCoroutine(NotifyingEnemyAboutPresence());
        theUnit=GetComponentInParent<TheUnit>();
        theUnit.SetTerritoryCollider(gameObject);
    }
    IEnumerator NotifyingEnemyAboutPresence(){
        while(true){
        Collider[] c=Physics.OverlapSphere(transform.position, 1f, LayerMask.GetMask("Tower"));
        if(c.Length>0){
            Debug.Log("Tower Detected!");
            foreach(Collider tower in c){
                if(tower.GetComponentInParent<TowerInstance>()&&
                !tower.GetComponentInParent<TowerInstance>().IsTowerBelongToPlayer()){
                    // Debug.Log("Tower Detected!"+tower.name);
                    // tower.GetComponentInParent<TowerInstance>().TowerParameterBreached(gameObject);
                    BossId=tower.GetComponentInParent<TowerInstance>().ReturnBossId();
                }
            }
        }
        else{
            Debug.Log("No Tower Detected!");
            BossId=0;
        }
        yield return new WaitForSeconds(3f);}
    }

    public int ReturnBreachingBossId(){
        return BossId;
    }

}
