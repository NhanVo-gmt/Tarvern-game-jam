using System;
using System.Collections;
using System.Collections.Generic;
using Knight.Camera;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float parralaxEffect;
    private Vector2 startPos;

    static float cameraHeight = 0;
    
    private Camera cam;
    private Vector2 camStartPos;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        UpdateCamStartPos(cam.transform.position); //todo remove after menu scene
        startPos = transform.position;
    }

    private void OnEnable() {
        SceneLoader.Instance.OnSceneLoadingCompleted += SceneLoader_OnSceneLoadingCompleted;
    }

    private void SceneLoader_OnSceneLoadingCompleted(object sender, Vector2 e)
    {
        UpdateCamStartPos(new Vector2(e.x, e.y + CameraController.Instance.GetCamHeightToPlayer()));
    }

    public void UpdateCamStartPos(Vector2 startPos)
    {
        camStartPos = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(cam.transform.position + " " + camStartPos);
        Vector2 dist = GetDistanceFromEffect(cam.transform.position, camStartPos);
        transform.position = new Vector2(startPos.x + dist.x, startPos.y + dist.y);
    }


    Vector2 GetDistanceFromEffect(Vector2 camPos, Vector2 camStartPos)
    {
        return new Vector2((camPos.x - camStartPos.x) * parralaxEffect, (camPos.y - camStartPos.y) * parralaxEffect);
    }
}
