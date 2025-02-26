using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class ProflieTroopUI : MonoBehaviour
{
    [SerializeField]private TroopsCountManager troopsCountManager;
    [SerializeField] private TextMeshProUGUI TroopsText;
    private int [][] troops;

    public void DisplayTroopsInfo()
    {
        //called by ProfileUIManager when clicked on troops.
        // Display troops info here

        troops=troopsCountManager.ReturnAllTroops();//order: cavalry, infantry, archer, mage

        string text="";
        
        text += "Cavalry: ";
        for (int i = 0; i < troops[0].Length; i++)
        {
            if(troops[0][i]!=0){
                text += "Level "+ (i+1)+"-" +troops[0][i] + ", ";
            }                
        }
        text += "\n";

        text += "Infantry: ";
        for (int i = 0; i < troops[1].Length; i++)
        {
            if(troops[1][i]!=0){
                text += "Level "+ (i+1)+"-" +troops[1][i] + ", ";

            }                
        }
        text += "\n";

        text += "Archer: ";
        for (int i = 0; i < troops[2].Length; i++)
        {
            if(troops[2][i]!=0){
                text += "Level "+ (i+1)+"-" +troops[2][i] + ", ";

            }                
        }
        text += "\n";

        text += "Mage: ";
        for (int i = 0; i < troops[3].Length; i++)
        {
            if(troops[3][i]!=0){
                text += "Level "+ (i+1)+"-" +troops[3][i] + ", ";

            }                
        }

        TroopsText.text = text;


    }

    
}
