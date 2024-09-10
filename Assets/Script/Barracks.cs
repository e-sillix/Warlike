using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{
   //this will be responsible for returning capacity for the barracks .
   
   [SerializeField] private int soldierCapacity=20;//this will be upgraded.
   [SerializeField] private int rateOfTraining=1;// this will be upgraded.

   [SerializeField] private GameObject TroopsTraining;//attach training manager ,might not work.
   private TroopsTraining trainer;
   void Start(){
      //call isTraining,for check if there is any current training going on .

      trainer= TroopsTraining.GetComponent<TroopsTraining>();    
      if(trainer!=null){
         trainer.IsTraining();
      }  
      else{
         Debug.Log("Barrack prefab needs calling change.");
      }
   }
   public int ReturnTroopsCapacity(){
      return soldierCapacity;
   }

   public void TriggerIcon(){
      //trigger icon for barracks soldiers to be collected.


   }

}
