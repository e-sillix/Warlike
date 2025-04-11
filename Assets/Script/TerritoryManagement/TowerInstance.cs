using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInstance : MonoBehaviour
{
    [SerializeField]private GameObject towerRendererObj;

    [SerializeField]private GameObject UIComponent;

    [SerializeField]private GameObject UIButtonForAttack;

    [SerializeField]private float TotalHealth,RecoveringRate;

    [SerializeField]private Vector3 ScanningVolume;
    [SerializeField]private LayerMask unitLayer;
    private GameObject Boss;

    private float CurrentHealth;
    private String NameOfTheBoss;
    
    private int BossId;
    private bool isPlayerTheOwner;
    private Coroutine IndividualScanning;

    public int ReturnBossId(){
        return BossId;
    }
    public void EnemyTowerDependency(int bossId,Color newColor,GameObject boss){
        isPlayerTheOwner=false;
        BossId=bossId;
        AssignColor(newColor);
        Boss=boss;
        IndividualScanning=StartCoroutine(ScanningTerritory());
    }
    public void SetOwnerShip(bool isOwner){
        Debug.Log("SetOwnerShip "+isOwner);
        isPlayerTheOwner=isOwner;
    }
    public bool IsTowerBelongToPlayer(){
        if(isPlayerTheOwner){

        Debug.Log("IsTowerBelongToPlayer "+isPlayerTheOwner);
        }
        return isPlayerTheOwner;
    }
    public void AssignColor(Color newColor){
        Renderer renderer = towerRendererObj.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Use sharedMaterial if you want all using same material to change
            renderer.material.color = newColor;
        }
        else
        {
            Debug.LogWarning("No Renderer found on this GameObject!");
        }
    }

    public void RemoveFromTheTowerList(){
        if(Boss!=null){
            Boss.GetComponent<EnemyWatchTowerSpawner>().RemoveTowerFromList(this.gameObject);
        }
    }
    public void OnClickOnCollider(){
        //indirectly
        UIComponent.SetActive(true);
        if(!isPlayerTheOwner){
            UIButtonForAttack.SetActive(true);
        }
        else{
            UIButtonForAttack.SetActive(false);
        }
    }

    public void DeSelectTower(){
        UIComponent.SetActive(false);
    }
    public void AttackClicked(){
        GetComponent<TowerCombat>().AttackIsClick();;
    }
    public void InfoClicked(){

    }

    IEnumerator ScanningTerritory()
{
    while (true)
    {
        // Debug.Log("🔍 Scanning territory...");
        if(ReturnPlayerArmies()[0]){
            Debug.Log("Found player armies in the area!");
        }
        // 🧠 Put your scanning logic here...

        yield return new WaitForSeconds(3f); // ⏱️ Wait 3 seconds before the next scan
    }
}
GameObject[] ReturnPlayerArmies(){
    GameObject[] playerArmies=new GameObject[5];
        // Find all colliders within a 200 unit radius
        Collider[] colliders = Physics.OverlapBox(transform.position, ScanningVolume, 
        Quaternion.identity, unitLayer);
        int  i=0;
        foreach (Collider collider in colliders)
        {
            // Check if the object has TheUnit script attached
            Attacking unit = collider.GetComponentInParent<Attacking>();
            if (unit != null)
            {
            Debug.Log(Vector3.Distance(transform.position, collider.transform.position));
                // Unit found within the detection range
                // You can do something with the unit here, like targeting or attacking
                playerArmies[i]=unit.gameObject;
                i++;
            }
        }
        return playerArmies;
}

}
