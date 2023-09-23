using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Slime : MonoBehaviour, IBreakable
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
        timeSinceLastShot = Random.Range(1, 4);
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
        
        GameObject projectile = ObjectPoolSpawnController.Instance.SpawnPooledPrefab(projectilePrefab.gameObject, shootPos.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().Initialize(shootDirection);
    }
}
