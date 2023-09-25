using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Player player;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        player.OnPlayerRespawn += OnPlayerRespawn;
    }

    private void OnDisable()
    {
        if (player != null)
            player.OnPlayerRespawn -= OnPlayerRespawn;
    }

    private void OnPlayerRespawn()
    {
        anim.SetBool("respawn", true);
    }
}
