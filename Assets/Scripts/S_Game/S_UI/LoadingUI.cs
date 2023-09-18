using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;

    CanvasGroup canvasGroup;

    Coroutine LoadCoroutine;
    bool isLoading;

    void Awake() {
        canvasGroup = loadingScreen.GetComponent<CanvasGroup>();
    }

    void Start() 
    {
        SceneLoader.Instance.OnSceneLoadingStarted += SceneLoader_OnSceneLoadingStarted;
        SceneLoader.Instance.OnSceneLoadingProgressChanged += SceneLoader_OnSceneLoadingProgressChanged;
        SceneLoader.Instance.OnSceneLoadingCompleted += SceneLoader_OnSceneLoadingCompleted;
    }

    private void SceneLoader_OnSceneLoadingCompleted(object sender, Vector2 e)
    {
        if (canvasGroup.alpha != 0)
        {
            StartCoroutine(FadeOut());
        }
    }

    private void SceneLoader_OnSceneLoadingStarted(object sender, EventArgs e)
    {
        LoadCoroutine = StartCoroutine(FadeIn());
    }

    private void SceneLoader_OnSceneLoadingProgressChanged(object sender, float progress)
    {
        slider.value = Mathf.Clamp01(progress/ 0.9f);
    }


    IEnumerator FadeIn()
    {
        yield return Fade(1, 1);
    }

    IEnumerator FadeOut()
    {
        if (LoadCoroutine != null)
        {
            yield return LoadCoroutine;
        }
        
        yield return Fade(0, 1);
    }

    IEnumerator Fade(float targetAlpha, float duration)
    {
        isLoading = true;

        float startAlpha = canvasGroup.alpha;
        float time = 0;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        isLoading = false;
    }
}
