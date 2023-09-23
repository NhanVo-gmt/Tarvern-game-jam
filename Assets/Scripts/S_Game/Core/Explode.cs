using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] private float radius;

    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.onTakeDamage += CheckBreakableObjects;
    }

    private void OnDisable()
    {
        health.onTakeDamage -= CheckBreakableObjects;
    }

    public void CheckBreakableObjects()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach(Collider2D collider in colliders)
        {
            if (collider.GetComponent<IBreakable>() != null)
            {
                Destroy(collider.gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
