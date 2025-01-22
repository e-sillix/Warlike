using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepUI : MonoBehaviour
{
    public GameObject SelectedVisual,ToMarchVisual;
    public void CreepSelected()
    {
        Debug.Log("Creep selected");
        SelectedVisual.SetActive(true);
    }
    public void CreepDeselected()
    {
        SelectedVisual.SetActive(false);
    }
    public void CreepIsMarchedTowards()
    {
        Debug.Log("Creep Is Being marched towards");
        SelectedVisual.SetActive(false);
        ToMarchVisual.SetActive(true);
    }
    public void ArmyReached()
    {
        Debug.Log("Army reached");
        ToMarchVisual.SetActive(false);
    }
}
