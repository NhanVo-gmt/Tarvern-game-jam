using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knight.Camera
{
    public class CameraController : SingletonObject<CameraController>
    {
        Transform cam;
        float camHeight = 0;

        protected override void Awake() 
        {
            base.Awake();
            cam = GetComponent<Transform>();
        }

        void Start() {
            CalculateCamHeight();
        }

        void CalculateCamHeight()
        {
            camHeight = cam.transform.position.y - Player.Instance.transform.position.y;
        }

        public float GetCamHeightToPlayer()
        {
            return camHeight;
        }
        
        void LateUpdate() 
        {
            cam.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
