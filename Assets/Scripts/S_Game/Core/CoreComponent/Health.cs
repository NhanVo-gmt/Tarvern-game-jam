using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : CoreComponent
{
    [SerializeField] int health; //todo set private

    public Action onTakeDamage;
    public Action onDie;
    public Action<int> onUpdateHealth;

    RecoveryController recoveryController;

    private bool isDie = false;

    #region Set up
    
    public void SetHealth(HealthData data)
    {
        isDie = false;
        health = data.health;
        onUpdateHealth?.Invoke(health);
    }

    #endregion
    
    protected override void Awake() 
    {
        base.Awake();

        recoveryController = GetComponent<RecoveryController>();
    }
    
    public void TakeDamage(int damage)
    {
        if (health <= 0) return;

        health -= damage;

        onUpdateHealth?.Invoke(health);

        TakeDamage();
        
        if (health <= 0)
        {
            Die();
        }
    }

    bool IsInvulnerable() 
    {
        return recoveryController != null && recoveryController.IsInInvulnerabiltyTime();
    }

    void TakeDamage()
    {
        onTakeDamage?.Invoke();
    }

    private void Die()
    {
        StartCoroutine(DieCoroutine());
    }

    IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(1f);
        isDie = true;
        onDie?.Invoke();
    }
}
