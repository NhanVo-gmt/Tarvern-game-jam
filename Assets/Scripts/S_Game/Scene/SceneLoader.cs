using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = System.Object;

public class SceneLoader : SingletonObject<SceneLoader>
{
    public enum Scene {
        MenuScene,
        GameScene,
        EndScene
    }

    public EventHandler OnSceneLoadingStarted;
    public EventHandler<float> OnSceneLoadingProgressChanged;
    public EventHandler<Vector2> OnSceneLoadingCompleted;

    AsyncOperation loadingOperation;
    Vector2 spawnPos;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void DestroyInMenu(UnityEngine.SceneManagement.Scene scene, LoadSceneMode arg1)
    {
        // do not destroy
    }

    public void ChangeScene(Scene scene, Vector2 position = new Vector2())
    {
        if (scene != Scene.GameScene)
        {
            Destroy(Player.Instance);
        }
        StartCoroutine(LoadSceneCoroutine(scene));
        spawnPos = position;
    }

    IEnumerator LoadSceneCoroutine(Scene scene)
    {
        OnSceneLoadingStarted?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(1f);

        loadingOperation = SceneManager.LoadSceneAsync(scene.ToString());
    }


    void Update() {
        if (loadingOperation == null) return;

        if (!loadingOperation.isDone)
        {
            OnSceneLoadingProgressChanged?.Invoke(this, loadingOperation.progress);
        }
        else
        {
            loadingOperation = null;
            StartCoroutine(CompleteLoadSceneCoroutine());
        }
    }


    IEnumerator CompleteLoadSceneCoroutine()
    {
        if (Player.Instance != null)
            Player.Instance.transform.position = spawnPos;  
        
        yield return new WaitForSeconds(1f);
        
        OnSceneLoadingCompleted?.Invoke(this, spawnPos);
    }
}
