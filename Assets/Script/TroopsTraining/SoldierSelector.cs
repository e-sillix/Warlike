using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSelector : MonoBehaviour
{
    [SerializeField] private GameObject TheSoldier;
    private TheArmy Parent;
    private Renderer objectRenderer;
    void Start(){
        Parent  = TheSoldier.GetComponent<TheArmy>();
        objectRenderer = GetComponent<Renderer>();
    }

    public void NotifyParentPosition(Vector3 position){
        Parent.SetTargetPosition(position);
    }

    // Method to highlight the object when selected (e.g., change color)
    public void Highlight(bool isSelected)
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = isSelected ? Color.yellow : Color.white;
        }
    }
}
