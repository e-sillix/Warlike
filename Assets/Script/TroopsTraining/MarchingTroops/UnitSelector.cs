using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this one is attached to soldier(The Army prefab)/visual child cube
public class UnitSelector : MonoBehaviour
{
    //this act as collider convey to it's parent to move and changed color with it's selection
    //and pass the coordinate to move to.
    [SerializeField] private TheUnit theUnit;
   
    public TheUnit ReturnTheUnit(){
        return theUnit;
    }
}
