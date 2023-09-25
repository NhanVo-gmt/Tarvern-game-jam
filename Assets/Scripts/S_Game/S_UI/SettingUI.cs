using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private GameObject settingCanvas;

    public void OpenSetting()
    {
        settingCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseSetting()
    {
        settingCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToMenu()
    {
        CloseSetting();
        SceneLoader.Instance.ChangeScene(SceneLoader.Scene.MenuScene);
    }
}
