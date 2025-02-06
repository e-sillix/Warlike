using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAttacking : MonoBehaviour
{
    public int totalHealth;
    private int currentHealth;
    public Image healthFill;
    public string[] Rewards;
    void Start()
    {
        currentHealth = totalHealth;
        UpdateHealth();
    }
    void UpdateHealth()
    {
        float fillPercent = (float)currentHealth / (float)totalHealth;
        healthFill.fillAmount = fillPercent;
        // Debug.Log("Boss health updated.:"+fillPercent);
    }
    public int ReturnHealth(){
        return currentHealth;
    }
    void OnDefeat()
    {
        GetComponent<Boss>().OnDefeat();
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Boss current health"+currentHealth);
        UpdateHealth();
        if (currentHealth <= 0)
        {
            OnDefeat();
        }
    }
}
