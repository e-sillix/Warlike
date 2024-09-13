using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{
   //this will be responsible for returning capacity for the barracks .
   
   [SerializeField] private int soldierCapacity=20;//this will be upgraded.
   // [SerializeField] private int rateOfTraining=1;// this will be upgraded.

   private TroopsTrainingLogic trainer;
   void Start(){
      //call isTraining,for check if there is any current training going on .

      GameObject troopsTraining = GameObject.Find("TroopsTrainingManager");//finding TroopsTrainingManager parent-based search
      if(troopsTraining){
      trainer= troopsTraining.GetComponent<TroopsTrainingLogic>();
      if(trainer!=null){
         trainer.IsTraining();
      }     
      }
   }
   public int ReturnTroopsCapacity(){
      return soldierCapacity;
   }

   public void TriggerIcon(){
      //trigger icon for barracks soldiers to be collected.


   }

}
