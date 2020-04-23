using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControllerScripts.Firstperson
{
    public class CameraController : MonoBehaviour 
    {
        public float rotationSpeed; 
        public bool invertYAxis; 
     
        public InputActionMap inputActionMap; 
        public InputActionAsset inputActionAsset;
        private Vector2 mouse; 
        private float  deltaY; 
        private Vector3 camAngle; 
 
        public bool rotationEnabled = true; 
 
        void Start() 
        { 
            Cursor.lockState = CursorLockMode.Locked; 
            inputActionMap = inputActionAsset.actionMaps[0]; 
            inputActionMap.actions[2].Enable(); 
            inputActionMap.actions[2].performed += CameraMovementControlChanged;
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        void Update() 
        { 
            if (Application.isFocused && rotationEnabled && Cursor.lockState == CursorLockMode.Locked ) 
            { 
                RotateCamera(); 
            } 
        }
    
        void CameraMovementControlChanged(InputAction.CallbackContext ctx) 
        { 
         
        } 
 
        void RotateCamera() 
        { 
        
            mouse = inputActionMap.actions[2].ReadValue<Vector2>(); // this is the vertical axis 
            deltaY = -mouse.y * Time.deltaTime * rotationSpeed; 
            camAngle = transform.rotation.eulerAngles; 
            camAngle.x += deltaY; //  camera roll uses vertical mouse axis 
            camAngle.x = camAngle.x % 360; // This just takes any numbers outside of 0 - 360 and converts them to a number between 0-360. 
            // camera angle can not rotate to completely vertical  because weird stuff happens... because im doing euler quaternion conversions. 
            if (camAngle.x > 70 && camAngle.x < 180) 
            { 
                camAngle.x = 70; 
            } 
            else if (camAngle.x < 278 && camAngle.x > 180) 
            { 
                camAngle.x = 278; 
            }
            transform.rotation = Quaternion.Euler(camAngle.x, camAngle.y, 0); 
        } 

    }
} 
