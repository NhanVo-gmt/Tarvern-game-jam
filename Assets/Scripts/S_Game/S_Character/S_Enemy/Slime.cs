using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Slime : MonoBehaviour
{
    enum SlimeState
    {
        Idle,
        Jump
    }

    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;

    private SlimeState currentState = SlimeState.Idle;
    private bool canChangeState = true;

    private Vector2 jumpDirection;

    private void Update()
    {
        ChangeState();
        Move();
    }

    void ChangeState()
    {
        if (canChangeState && currentState == SlimeState.Idle)
        {
            currentState = SlimeState.Jump;
            SetRandomJumpDirection();
        }
    }

    private void SetRandomJumpDirection()
    {
        float randomX = Random.Range(-1f, 1f); // Random horizontal direction
        float randomY = Random.Range(0.5f, 1f); // Random vertical direction (upward)
        jumpDirection = new Vector2(randomX, randomY).normalized;
    }

    void Move()
    {
        if (currentState != SlimeState.Jump) return;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = jumpDirection * jumpForce;

        currentState = SlimeState.Idle;
        canChangeState = false;
        StartCoroutine(SetChangeStateCoroutine());
    }

    IEnumerator SetChangeStateCoroutine()
    {
        yield return new WaitForSeconds(1f);
        canChangeState = true;
    }
}
