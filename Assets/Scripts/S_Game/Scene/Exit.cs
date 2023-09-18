using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Exit : MonoBehaviour
{
    public SceneLoader.Scene scene;
    public Vector2 spawnPos;

    void Awake() {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Player>())
        {
            SceneLoader.Instance.ChangeScene(scene, spawnPos);
        }
    }
}
