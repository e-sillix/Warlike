using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this one is attached to soldier(The Army prefab)/visual child cube
public class UnitSelector : MonoBehaviour
{
    //this act as collider convey to it's parent to move and changed color with it's selection
    //and pass the coordinate to move to.
    [SerializeField] private GameObject TheSoldier;
    [SerializeField] private GameObject SelectorIcon;
    private TheUnit Parent;

    void Start(){
        Parent  = TheSoldier.GetComponent<TheUnit>();

    }

    public void NotifyParentPosition(Vector3 position){
        //takes position from manager and pass it to parent.
        Parent.SetTargetPosition(position);
    }

    // Method to highlight the object when selected (e.g., change color)
    public void Highlight(bool isSelected)
    {    
        SelectorIcon.SetActive(isSelected);
    }
}
