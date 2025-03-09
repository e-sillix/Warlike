using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAttacking : MonoBehaviour
{
    public int totalHealth;
    private float currentHealth;
    public Image healthFill;
    private Attacking attacking;
    public string[] Rewards;
    [SerializeField] private float Damage;
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
    public float ReturnHealth(){
        return currentHealth;
    }
    void OnDefeat()
    {
        GetComponent<Boss>().OnDefeat();
    }
    public void TakeDamage(float damage,Attacking Attacking)
    {
        attacking = Attacking;
        currentHealth -= damage;
        Debug.Log("Boss current health"+currentHealth);
        UpdateHealth();
        CounterDamage();
        if (currentHealth <= 0)
        {
            OnDefeat();
        }
    }
    void CounterDamage(){
        // Debug.Log("Boss Counter Attacking Damage:"+Damage);
        float ActualDamage=Damage*(currentHealth/(float)totalHealth);
        attacking.TakeDamage(ActualDamage);
    }
}
