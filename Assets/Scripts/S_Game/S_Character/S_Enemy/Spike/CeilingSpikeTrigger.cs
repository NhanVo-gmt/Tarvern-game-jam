using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingSpikeTrigger : MonoBehaviour
{
    public Action<bool> OnTrigger;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnTrigger?.Invoke(false);
        }
        else if (other.CompareTag("Spike"))
        {
            OnTrigger?.Invoke(true);
        }
    }
}
