using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerCombat : MonoBehaviour
{
    private TroopsExpeditionManager troopsExpeditionManager;
    [SerializeField]private float TotalHealth,RecoveringRate,Damage;
    private TowerPointPlacer towerPointPlacer;
    private float Health;
    private Attacking attacking;
    public Image healthFill;

    void Start()
    {
        Health=TotalHealth;   
    }
    public void Dependency(TroopsExpeditionManager TroopsExpeditionManager
    ,TowerPointPlacer TowerPointPlacer){
        troopsExpeditionManager=TroopsExpeditionManager;
        towerPointPlacer=TowerPointPlacer;
    }
    public void AttackIsClick(){
        troopsExpeditionManager.CombatTargetClicked(gameObject);
    }
    public float ReturnHealth(){
        return Health;
    }

    public void TakeDamage(float damage,Attacking Attacking){
        attacking = Attacking;
        Health -= damage;
        Debug.Log("Tower health"+Health);
        UpdateHealth();
        // CounterDamage();
        if (Health <= 0)
        {
            OnDefeat();
        }
    }
    void UpdateHealth()
    {
        float fillPercent = (float)Health / (float)TotalHealth;
        healthFill.fillAmount = fillPercent;
        // Debug.Log("Boss health updated.:"+fillPercent);
    }
    void OnDefeat(){
        Debug.Log("Tower is destroyed");
        //for enemy only
        GetComponent<TowerInstance>().RemoveFromTheTowerList();
        towerPointPlacer.TowerIsDestroyed(this.gameObject);
        // Destroy(gameObject);
    }
}
