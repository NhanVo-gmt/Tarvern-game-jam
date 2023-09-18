using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimatorController))]
public class FlashingEffect : MonoBehaviour
{
    [SerializeField] float cooldown;
    [SerializeField] Material flashMaterial; //todo setglobal
    
    Coroutine flashingCoroutine;

    SpriteRenderer sprite;
    Material defaultMaterial;

    void Awake() 
    {
        sprite = GetComponent<SpriteRenderer>();
        defaultMaterial = sprite.material;
    }
    
    public void StartFlashing()
    {
        StopFlashing();
        flashingCoroutine = StartCoroutine(Flashing());
    }

    IEnumerator Flashing()
    {
        sprite.material = flashMaterial;
        yield return new WaitForSeconds(cooldown);
        sprite.material = defaultMaterial;
    }

    void StopFlashing()
    {
        if (flashingCoroutine != null)
        {
            StopCoroutine(flashingCoroutine);
            sprite.material = defaultMaterial;
        }
    }
}
