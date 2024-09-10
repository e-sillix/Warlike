using UnityEngine.UI; 
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public TextMeshProUGUI woodsCounter;
    
    [SerializeField] private int woodCurrentCurrency=0;
    
    
    private void UpdateUICounter(){
         woodsCounter.text = "Woods: " + woodCurrentCurrency.ToString();
    }


    public void CollectingAllWoods(){
        
        Farm[] allFarms = FindObjectsOfType<Farm>();
        foreach (Farm farm in allFarms)
        {
            woodCurrentCurrency += farm.ReturnWoods();
            farm.triggerConsumingAnimation=true;
            farm.ResetWoods();
        }
         if (woodsCounter != null)
        {
            UpdateUICounter();
        }
           
    }
    public int ReturnCurrentCurrency(){
        return woodCurrentCurrency;
    }

    public void SpendWood(int amount){
        woodCurrentCurrency=woodCurrentCurrency-amount;
        UpdateUICounter();
    }
   
}
