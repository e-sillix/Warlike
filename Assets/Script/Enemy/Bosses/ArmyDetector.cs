using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyDetector : MonoBehaviour
{
    public float detectionRange = 200f;  // 200 units range
    [SerializeField] private LayerMask unitLayer;  // Optional: specify a layer for units to make detection faster

    public float detectionInterval = 5f; // Time interval between each detection (5 seconds)

    private int BossId;


void Start()
{
    BossId=GetComponent<Boss>().ReturnBossId();
    StartCoroutine(RunUnitScanEvery3Seconds());
}

IEnumerator RunUnitScanEvery3Seconds()
{
    while (true)
    {
        GameObject[] units = GetAllUnitsBreaching();

        // Just to confirm it's working (optional)
        // Debug.Log("Found units: " + units.Length);
        // if(units.Length>0){
            GetComponent<Boss>().ReportingArmies(units);
        // }
        
        yield return new WaitForSeconds(3f);
    }
}

GameObject[] GetAllUnitsBreaching()
{
    TheUnit[] allUnits = GameObject.FindObjectsOfType<TheUnit>();

    List<GameObject> matchingUnits = new List<GameObject>();

    foreach (TheUnit unit in allUnits)
    {
        if (unit.GetBossId() == BossId&& !unit.returnIsDefeated())
        {
            matchingUnits.Add(unit.gameObject);
        }
    }

    return matchingUnits.ToArray();
}


public void TriggerDetectionOnce(){
    //when player Armies get defeated.
    GetComponent<Boss>().ReportingArmies(GetAllUnitsBreaching());
}

}
