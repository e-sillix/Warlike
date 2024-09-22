using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this one attached to MarchManager
//the selection manager
public class UnitSelectionManager : MonoBehaviour
{
    //tell if a army is selected to MUIM
   
    public UnitSelector selectedObject; // Currently selected object    

    public void selectTheUnit(UnitSelector clickedObject){
        // Select the new clicked object
        selectedObject = clickedObject;
        selectedObject.Highlight(true); // Highlight the newly selected object--------------
    }
    public void DeselectTheUnit(){
        selectedObject.Highlight(false); // Remove highlight from the previously selected object
        selectedObject=null;
    }
    
}
