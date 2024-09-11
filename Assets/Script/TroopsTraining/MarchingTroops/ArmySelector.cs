using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this one is attached to soldier(The Army prefab)/visual child cube
public class ArmySelector : MonoBehaviour
{
    //this act as collider convey to it's parent to move and changed color with it's selection
    //and pass the coordinate to move to.
    [SerializeField] private GameObject TheSoldier;
    private ArmyMarcher Parent;
    private Renderer objectRenderer;
    void Start(){
        Parent  = TheSoldier.GetComponent<ArmyMarcher>();
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
