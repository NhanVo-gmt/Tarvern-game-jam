using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float fireRate;

    [SerializeField] private Transform shootPos;
    [SerializeField] private Vector2 shootDirection;

    private float timeSinceLastShot;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        timeSinceLastShot -= Time.deltaTime;
        if (timeSinceLastShot <= 0)
        {
            timeSinceLastShot = fireRate;
            StartCoroutine(ShootCoroutine());
        }
    }

    IEnumerator ShootCoroutine()
    {
        anim.SetTrigger("shoot");
        yield return new WaitForSeconds(.4f);
        
        GameObject projectile = Instantiate(projectilePrefab.gameObject, shootPos.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().Initialize(shootDirection);
    }
}
