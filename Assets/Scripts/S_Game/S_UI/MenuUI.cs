using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public void StartGame()
    {
        SceneLoader.Instance.ChangeScene(SceneLoader.Scene.GameScene, new Vector2(0, 6));
    }

    public void ReplayGame()
    {
        SceneLoader.Instance.ChangeScene(SceneLoader.Scene.MenuScene, new Vector2(0 ,0));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
