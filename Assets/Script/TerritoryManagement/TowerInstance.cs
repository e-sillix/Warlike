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
    private GameObject Boss;

    private float CurrentHealth;
    private String NameOfTheBoss;
    
    private int BossId;
    public bool isPlayerTheOwner;

    public int ReturnBossId(){
        return BossId;
    }
    public void EnemyTowerDependency(int bossId,Color newColor,GameObject boss){
        isPlayerTheOwner=false;
        BossId=bossId;
        AssignColor(newColor);
        Boss=boss;
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
}
