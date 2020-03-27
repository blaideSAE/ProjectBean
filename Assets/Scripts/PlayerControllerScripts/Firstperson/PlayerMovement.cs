using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControllerScripts.Firstperson
{
    public class PlayerMovement : MonoBehaviour 
    { 
        private CharacterController characterController; 
        public Vector2 moveControlBuffer; 
        public InputActionMap inputActionMap; 
        public InputActionAsset inputActionAsset;
        public float rotationSpeed; 
        private float deltaX; 
        private Vector3 rotEulers; 
 
        public Vector3 moveDir= Vector2.zero; 
        // Start is called before the first frame update 
        private void Awake() 
        { 
            characterController = GetComponent<CharacterController>(); 
            inputActionMap = inputActionAsset.actionMaps[0]; 
            inputActionMap.actions[0].Enable(); 
            inputActionMap.actions[0].performed += MovementControlChanged; 

            inputActionMap.actions[2].Enable(); 
 
 
 
        } 
 
        void Start() 
        { 
 
 
 
        } 
 
        public void MovementControlChanged(InputAction.CallbackContext ctx) 
        { 
            // moveControlBuffer = ctx.ReadValue<Vector2>(); 
        } 
 

        private void FixedUpdate() 
        { 
            deltaX = inputActionMap.actions[2].ReadValue<Vector2>().x * Time.deltaTime * rotationSpeed; 
            rotEulers = transform.rotation.eulerAngles; 
            rotEulers.y += deltaX; 
            transform.rotation = Quaternion.Euler(0, rotEulers.y, 0); 
         
         
         
            moveControlBuffer = inputActionMap.actions[0].ReadValue<Vector2>(); 
         
            moveDir.z = .1f * moveControlBuffer.y; 
            moveDir.x = .1f * moveControlBuffer.x; 
            moveDir.y = UnityEngine.Physics.gravity.y; 
            moveDir = transform.rotation * moveDir; 
            characterController.Move(moveDir); 
         
         
         
 
        
 
 
 
        } 
    }
} 