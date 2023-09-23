using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float velocity = 5f;
    Vector2 direction;

    Rigidbody2D rb;

    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        SelfDestroy();
    }

    private void SelfDestroy()
    {
        GetComponent<PooledObject>().Initialize(2f);
    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    private void Update() {
        Move();
    }

    private void Move()
    {
        rb.velocity = direction * velocity;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(1, IDamageable.DamagerTarget.Enemy, direction); //hardcode
        }
    }
    
    
}
