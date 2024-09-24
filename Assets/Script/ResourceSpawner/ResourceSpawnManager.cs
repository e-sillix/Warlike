using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceSpawnManager : MonoBehaviour
{
    //dynamic respective spawning for both original and blueprint,trigger purchase 
    //trigger messages in resourceUI function.   
   
    // [SerializeField] private float farmRadius = 2f;           // The radius around the farm to check for collisions
    
    [SerializeField] private ResourceUI resourceUI;

    private GameObject currentBlueprint;    // The current blueprint instance
  
    public GameObject chosenBlueprint;
    public GameObject chosenResource;
    private Dictionary<ResourceType, int> allResources;
   
 
    //1.taking input from ui
    public void TriggerBluePrint(){     
            
        if (currentBlueprint == null)
            {
                //conditioning with current money
                SpawnBlueprint();                
            }       
            else{
                Destroy(currentBlueprint);
                currentBlueprint=null;
                SpawnBlueprint();
            } 
    }

//2.blueprint related
    void SpawnBlueprint()
    {
        // Instantiate the blueprint at the center of the screen
        currentBlueprint = Instantiate(chosenBlueprint);
    }

//4.check currency and spawn
    
    //2.intiating
    public void PlaceResources()
    {
        if (currentBlueprint != null)
        {
            // Check if the blueprint is in a valid location
            if (!currentBlueprint.GetComponent<BluePrint>().ReturnIsColliding())
            {
                // Instantiate the actual farm and destroy the blueprint
                Instantiate(chosenResource, currentBlueprint.transform.position, Quaternion.identity);
                Destroy(currentBlueprint);
                currentBlueprint = null;
                //cut the cost               
                resourceUI.TheBuildingPlaced();                               
            }
            else{
                resourceUI.MessageForNotEnoughSpace();
            }
        }
    }
    public void DestroyTheBlueprint(){
        Destroy(currentBlueprint);
    }
}
