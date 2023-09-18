using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCombat
{
    Collider2D col;
    IDamageable.DamagerTarget damagerTarget;

    public TouchCombat(Collider2D col, IDamageable.DamagerTarget damagerTarget) {
        this.col = col;
        this.damagerTarget = damagerTarget;
    }

    public void TouchAttack(Collider2D other) {
        if (other == col) return;

        if (other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(1, damagerTarget, Vector2.zero); // todo
        }
    }
}
