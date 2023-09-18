using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Combat : CoreComponent, IDamageable
{
    Collider2D col;
    IDamageable.DamagerTarget damagerTarget;
    IDamageable.KnockbackType knockbackType;

    Movement movement {get => _movement ??= core.GetCoreComponent<Movement>(); }
    Movement _movement;

    Health health {get => _heath ??= core.GetCoreComponent<Health>(); }
    Health _heath;

    GameSettings settings;
    MeleeCombat meleeCombat;
    TouchCombat touchCombat;


    Vector2 attackPosition;
    Vector2 hitDirection;


    #region Set up
    
    public void SetUpCombatComponent(IDamageable.DamagerTarget damagerTarget, IDamageable.KnockbackType knockbackType)
    {
        this.damagerTarget = damagerTarget;
        this.knockbackType = knockbackType;
        
        meleeCombat = new MeleeCombat(col, damagerTarget);

        if (damagerTarget == IDamageable.DamagerTarget.Player) return ;
        touchCombat = new TouchCombat(col, damagerTarget);
    }

    protected override void Awake()
    {
        base.Awake();

        col = GetComponent<Collider2D>();
    }

    void Start() 
    {
        settings = FindObjectOfType<GameSettings>();

        AddEvent();
    }

    void AddEvent()
    {
        health.onTakeDamage += Knockback;
    }

    private void OnDisable() {
        RemoveEvent();
    }

    private void RemoveEvent()
    {
        health.onTakeDamage -= Knockback;
    }

    #endregion

    #region Damage Method

    public void MeleeAttack(MeleeAttackData attackData)
    {
        meleeCombat.MeleeAttack(attackData, SetAttackPosition(attackData), movement.faceDirection);
    }

    Vector2 SetAttackPosition(MeleeAttackData attackData)
    {
        if (movement.faceDirection == Vector2.left)
        {
            attackPosition = (Vector2)transform.position + attackData.leftAttackPos;
        }
        else if (movement.faceDirection == Vector2.right)
        {
            attackPosition = (Vector2)transform.position + attackData.rightAttackPos;
        }

        return attackPosition;
    }

    public IDamageable.DamagerTarget GetDamagerType()
    {
        return damagerTarget;
    }

    public void TakeDamage(int damage, IDamageable.DamagerTarget damagerType, Vector2 attackDirection)
    {
        if (this.damagerTarget == damagerType) return ;
        
        if (attackDirection == Vector2.zero)
        {
            hitDirection = -movement.faceDirection;
        }
        else
        {
            hitDirection = attackDirection;
        }

        health.TakeDamage(damage);
    }

    #endregion

    #region Knockback

    public IDamageable.KnockbackType GetKnockbackType()
    {
        return knockbackType;
    }

    void Knockback()
    {
        float knockbackAmount = 0;
        switch(knockbackType)
        {
            case IDamageable.KnockbackType.weak:
                knockbackAmount = settings.WeakKnockbackAmount;
                break;
            case IDamageable.KnockbackType.strong:
                knockbackAmount = settings.StrongKnockbackAmount;
                break;
            case IDamageable.KnockbackType.player:
                knockbackAmount = settings.PlayerKnockbackAmount;
                break;
            
        }
        
        movement.AddForce(hitDirection, knockbackAmount);
    }

    #endregion

    #region Collider

    public void EnableCollider()
    {
        col.enabled = true;
    }

    public void DisableCollider()
    {
        col.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        touchCombat?.TouchAttack(other);
    }

    #endregion
}
