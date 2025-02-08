using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBuildingUIManager : MonoBehaviour
{
    [SerializeField]private CameraSystem cameraSystem;

   public void BuildingUIIsActive(){
//indirectly by buildingUI
        cameraSystem.SetException(true);
   }
   public void BuildingUIIsClosed(){
    //triggered directly by button
        cameraSystem.SetException(false);

   }
}
