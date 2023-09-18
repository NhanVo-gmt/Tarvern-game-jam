using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpriteGroupColor : MonoBehaviour
{
    [SerializeField] Color color = Color.white;

    void Awake() 
    {
        ChangeColorOfChildSprite();
    }


    void Update() 
    {
        if (Application.isPlaying) return;
        Debug.Log(1);
        
        ChangeColorOfChildSprite();
    }

    void ChangeColorOfChildSprite()
    {
        foreach(SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.color = color;
        }
    }
}
