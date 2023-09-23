using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodableObject : MonoBehaviour
{
    public float explosionForce = 10f; // Adjust as needed
    public float explosionRadius = 5f; // Adjust as needed

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Explode();
        }
    }

    void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D col in colliders)
        {
            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.AddForce((col.transform.position - transform.position).normalized * explosionForce, ForceMode2D.Impulse);
            }
        }

        // Optionally, play explosion effects here

        Destroy(gameObject); 
    }
}

