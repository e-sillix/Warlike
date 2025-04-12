using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class TroopsVisualInstance : MonoBehaviour
{
    private GameObject SingleTroops;
    private int TotalTroops;
    [SerializeField] private GameObject parentTroopsObj;
    // [SerializeField]private int lowSpawnLimit=5;
    // [SerializeField]private int TroopSpawnLimit=15;
    // [SerializeField]private int TroopsForHighestLimit=500;
    private GameObject[] AllTroops;
    // private Vector3 currentDirection ; // Default direction
    private bool isAllTroopsSpawned=false;
    // public void SetTroopsObj(GameObject t, float totalNumberOfTroops)
    // {
        public void SetTroopsObj(GameObject t, float totalNumberOfTroops)
{
    SingleTroops = t;
    TotalTroops = (int)totalNumberOfTroops;
    float spacing = 2.0f;

    // ✅ Initialize `AllTroops` only once, outside the loop
    int troopsToSpawn = Mathf.Clamp(TotalTroops / 10, 1, 15);
    AllTroops = new GameObject[troopsToSpawn];

    int troopsPerRow = Mathf.CeilToInt(Mathf.Sqrt(troopsToSpawn));

    for (int i = 0; i < troopsToSpawn; i++)
    {
        AllTroops[i] = Instantiate(SingleTroops, parentTroopsObj.transform);
        int row = i / troopsPerRow;
        int column = i % troopsPerRow;
        Vector3 newPos = new Vector3(column * spacing, 0, row * spacing);
        AllTroops[i].transform.localPosition = newPos;
    }

    isAllTroopsSpawned = true;
// }

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
public void TriggerWalking(){
    StartCoroutine(WaitForConditionWalking(isAllTroopsSpawned));

     
    }
    private IEnumerator WaitForConditionWalking(bool condition)
{
    // Wait until the condition is true
    yield return new WaitUntil(() => condition);
    foreach (GameObject troop in AllTroops)
    {
        // troop.GetComponent<Animator>().SetBool("IsWalking", true);
            if (troop.GetComponent<Animator>())
            {
        Animator animator = troop.GetComponent<Animator>();
                animator.SetBool("IsWalking", true); // Stop walking animation
            }
            else{
                Debug.Log("Can't find animator");
            }
    }
    }

    public void TriggerIdle(){
         StartCoroutine(WaitForCondition(isAllTroopsSpawned));
        
    }
    private IEnumerator WaitForCondition(bool condition)
{
    // Wait until the condition is true
    yield return new WaitUntil(() => condition);

    // Now trigger the idle animation
    foreach (GameObject troop in AllTroops)
    {
        if (troop.GetComponent<Animator>())
        {
        Animator anim = troop.GetComponent<Animator>();
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsAttacking", false);
        }
    }
}

    public void TriggerAttacking(){
    foreach (GameObject troop in AllTroops)
    {
        troop.GetComponent<Animator>().SetBool("IsAttacking", true);
    }
    }
    






}
