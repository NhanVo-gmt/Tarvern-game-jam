using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bat : MonoBehaviour
{
    enum BatState
    {
        Idle,
        Fly
    }

    [SerializeField] private float rangeFly;
    [SerializeField] private float moveSpeed;

    private BatState currentState = BatState.Idle;
    private bool canChangeState = true;

    private Vector2 destination;
    
    private void Update()
    {
        ChangeState();
        Move();
    }

    void ChangeState()
    {
        if (canChangeState && currentState == BatState.Idle)
        {
            currentState = BatState.Fly;
            SetRandomDestination();
        }
    }

    private void SetRandomDestination()
    {
        destination = Random.insideUnitCircle * rangeFly;
    }

    void Move()
    {
        if (currentState != BatState.Fly) return;

        if (Vector2.Distance(transform.position, destination) > 0.1f)
        {
            Vector2 direction = (destination - (Vector2)transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            currentState = BatState.Idle;
            canChangeState = false;
            StartCoroutine(SetChangeStateCoroutine());
        }
    }

    IEnumerator SetChangeStateCoroutine()
    {
        yield return new WaitForSeconds(1f);
        canChangeState = true;
    }
}
