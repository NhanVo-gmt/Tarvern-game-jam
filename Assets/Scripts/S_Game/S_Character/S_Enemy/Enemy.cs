using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Core core;

    Collider2D col;
    Health health;
    Combat combat;

    void Awake() {
        core = GetComponentInChildren<Core>();
    }

    void Start() 
    {
        combat = core.GetCoreComponent<Combat>();

        combat.SetUpCombatComponent(IDamageable.DamagerTarget.Enemy, IDamageable.KnockbackType.weak);
    }
}
