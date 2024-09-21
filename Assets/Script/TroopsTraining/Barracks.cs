using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{
   //this will be responsible for returning capacity for the barracks .
   
   [SerializeField] private int soldierCapacity=20;//this will be upgraded.
   // [SerializeField] private int rateOfTraining=1;// this will be upgraded.

   [SerializeField] private TrainingManager trainingManager;
   void Start(){
      //call isTraining,for check if there is any current training going on .

      GameObject troopsTraining = GameObject.Find("TrainingManager");//finding TroopsTrainingManager parent-based search
      if(troopsTraining){

      trainingManager= troopsTraining.GetComponent<TrainingManager>();
      if(trainingManager!=null){
         trainingManager.checkMidTrainingCreation();
      }     
      else{
         Debug.Log("1");
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
