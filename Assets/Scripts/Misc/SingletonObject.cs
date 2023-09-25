using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonObject<T> : MonoBehaviour where T : SingletonObject<T>
{
    public static T Instance;

    protected virtual void Awake() 
    {
        if (Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Debug.LogError("There is more than one " + name);
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += DestroyInMenu;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= DestroyInMenu;
    }

    protected virtual void DestroyInMenu(Scene scene, LoadSceneMode arg1)
    {
        if (scene.name == "MenuScene")
        {
            Destroy(gameObject);
        }
    }
}
