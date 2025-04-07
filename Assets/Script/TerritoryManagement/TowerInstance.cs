using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInstance : MonoBehaviour
{
    [SerializeField]private GameObject towerRendererObj;

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
}
