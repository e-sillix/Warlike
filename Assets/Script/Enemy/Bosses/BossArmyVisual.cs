using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArmyVisual : MonoBehaviour
{
   [SerializeField]private GameObject SingleTroop;
    [SerializeField]private GameObject ParentObj;
    [SerializeField]private int TotalTroops;
    private GameObject[] AllTroops;

    void Start()
    {
        SpawnTroops();
        TriggerIdle();
    }
    void SpawnTroops(){
        AllTroops = new GameObject[TotalTroops];
        int troopsPerRow = Mathf.CeilToInt(Mathf.Sqrt(TotalTroops));
        float spacing = 2.0f;

        for (int i = 0; i < TotalTroops; i++)
        {
            // Debug.Log("Spawning Single Troop");
            AllTroops[i] = Instantiate(SingleTroop, ParentObj.transform);

            int row = i / troopsPerRow;
            int column = i % troopsPerRow;
            Vector3 newPos = new Vector3(column * spacing, 0, row * spacing);

            AllTroops[i].transform.localPosition = newPos;
        }

    }
    public void FaceTheTarget(Vector3 ToLookAt){
        //when getting Attacked
        foreach (GameObject troop in AllTroops)
    {
        if (troop != null)
        {
            Vector3 direction = (ToLookAt - troop.transform.position).normalized; // Calculate direction

            if (direction != Vector3.zero) // Prevent rotation errors
            {
                troop.transform.rotation = Quaternion.LookRotation(new Vector3(
                    direction.x, 0, direction.z)); // Face target, only Y-axis rotation
            }
        }
    }
    }

    public void TriggerIdle(){
         Debug.Log("Idle is triggered");
         foreach (GameObject troop in AllTroops)
    {
        troop.GetComponent<Animator>().SetBool("IsWalking", false);
        troop.GetComponent<Animator>().SetBool("IsAttacking", false);
    }
    }
    public void TriggerAttack(){
                Debug.Log("Attack is triggered");

        foreach (GameObject troop in AllTroops)
    {
        troop.GetComponent<Animator>().SetBool("IsAttacking", true);
    }
    }
    public void TriggerWalk(){
        Debug.Log("walk is triggered");
        foreach (GameObject troop in AllTroops)
    {
        // troop.GetComponent<Animator>().SetBool("IsWalking", true);
        Animator animator = troop.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("IsWalking", true); // Stop walking animation
            }
            else{
                Debug.Log("Can't find animator");
            }
    }
    }
}
