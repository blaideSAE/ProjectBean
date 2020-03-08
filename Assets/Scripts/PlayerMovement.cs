using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System; 
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.InputSystem; 
 
public class PlayerMovement : MonoBehaviour 
{ 
    private CharacterController characterController; 
    public Vector2 moveControlBuffer; 
    public InputActionMap inputActionMap; 
    public InputActionAsset inputActionAsset; 
    public bool use; 
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
        inputActionMap.actions[1].Enable(); 
        inputActionMap.actions[1].performed += Use; 
        inputActionMap.actions[2].Enable(); 
 
 
 
    } 
 
    void Start() 
    { 
 
 
 
    } 
 
    public void MovementControlChanged(InputAction.CallbackContext ctx) 
    { 
        // moveControlBuffer = ctx.ReadValue<Vector2>(); 
    } 
 
    void Use(InputAction.CallbackContext ctx) 
    { 
        use = !use; 
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
        moveDir.y = 0; 
        moveDir = transform.rotation * moveDir; 
        characterController.Move(moveDir); 
         
         
         
 
        
 
 
 
    } 
} 