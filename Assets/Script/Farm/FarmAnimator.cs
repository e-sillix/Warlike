using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;

public class FarmAnimator : MonoBehaviour
{
    [SerializeField] private Farm farm;
    private const string IS_CONSUMING="IsConsumed";
    private const string IS_ENOUGH ="IsEnough";
    private const string DO_CONSUME ="Consuming";


   private Animator animator;
   private void Awake(){
    animator=GetComponent<Animator>();
   }

   private void Update(){
    // animator.SetBool(IS_CONSUMING,farm.IsConsumed());
    //this needs to be optimized........... for calling every update..

    if(farm.triggerConsumingAnimation){
    animator.SetTrigger(DO_CONSUME);
    }

  
   
    animator.SetBool(IS_ENOUGH,farm.returnIsEnough());
    
    farm.triggerConsumingAnimation=false;
   }

    
    
}
