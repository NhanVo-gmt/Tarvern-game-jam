using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingSpike : MonoBehaviour
{
    [SerializeField] private CeilingSpikeTrigger trigger;
    private bool isDrop = false;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        trigger.OnTrigger += StartDropping;
    }

    private void OnDisable()
    {
        trigger.OnTrigger -= StartDropping;
    }

    void StartDropping(bool value)
    {
        if (!value)
        {
            isDrop = true;
        }
        else
        {
            SelfDestroy();
        }
    }

    private void DropSpike()
    {
        transform.Translate(Vector2.down * 5 * Time.deltaTime);
    }

    private void Update()
    {
        if (!isDrop) return;
        
        DropSpike();
    }

    void SelfDestroy()
    {
        isDrop = false;
        
        anim.SetBool("IsBreak", true);
        Destroy(gameObject, 1f);
    }
}
