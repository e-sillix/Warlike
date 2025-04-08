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
    private float CurrentHealth;
    private String NameOfTheBoss;

    private bool isPlayerTheOwner=false;

    public void SetOwnerShip(bool isOwner){
        isPlayerTheOwner=isOwner;
    }
    public bool IsTowerBelongToPlayer(){
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

    }
    public void InfoClicked(){

    }
}
