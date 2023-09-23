using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public void StartGame()
    {
        SceneLoader.Instance.ChangeScene(SceneLoader.Scene.GameScene, new Vector2(0, 6));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
