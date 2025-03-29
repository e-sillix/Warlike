using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsVisualInstance : MonoBehaviour
{
    private GameObject SingleTroops;
    private int TotalTroops;
    [SerializeField] private GameObject parentTroopsObj;

    private GameObject[] AllTroops;
    private Vector3 currentDirection ; // Default direction

    public void SetTroopsObj(GameObject t, float totalNumberOfTroops)
    {
        SingleTroops = t;
        TotalTroops = (int)totalNumberOfTroops;
        Debug.Log("TotalTroops:"+TotalTroops);
        AllTroops = new GameObject[TotalTroops];

        int troopsPerRow = Mathf.CeilToInt(Mathf.Sqrt(TotalTroops));
        float spacing = 2.0f;

        for (int i = 0; i < TotalTroops; i++)
        {
            Debug.Log("Spawning Single Troop");
            AllTroops[i] = Instantiate(SingleTroops, parentTroopsObj.transform);

            int row = i / troopsPerRow;
            int column = i % troopsPerRow;
            Vector3 newPos = new Vector3(column * spacing, 0, row * spacing);

            AllTroops[i].transform.localPosition = newPos;
        }

        // Set initial direction
        // UpdateTroopsDirection(currentDirection);
    }

  public void UpdateTroopsDirection(Vector3 targetPosition)
{
    foreach (GameObject troop in AllTroops)
    {
        if (troop != null)
        {
            Vector3 direction = (targetPosition - troop.transform.position).normalized; // Calculate direction

            if (direction != Vector3.zero) // Prevent rotation errors
            {
                troop.transform.rotation = Quaternion.LookRotation(new Vector3(
                    direction.x, 0, direction.z)); // Face target, only Y-axis rotation
            }
        }

        // Vector3 direction=(targetPosition - troop.transform.position).normalized;
        // parentTroopsObj.transform.rotation = Quaternion.LookRotation(new Vector3(
        //             direction.x, 0, direction.z)); 
    }
}





}
