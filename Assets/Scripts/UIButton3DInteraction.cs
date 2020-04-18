using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIButton3DInteraction : MonoBehaviour
{
    private Camera cam;
    
    
    
    public InputActionMap inputActionMap; 
    public InputActionAsset inputActionAsset;
    public LayerMask _layerMask;

    private Ray ray;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    private void Awake()
    {
        inputActionMap = inputActionAsset.actionMaps[0];
        inputActionMap.actions[3].Enable();
        inputActionMap.actions[4].Enable();
        inputActionMap.actions[3].performed += cursorMove;
        inputActionMap.actions[4].performed += cursorClick;

    }

    private void cursorMove( InputAction.CallbackContext ctx)
    {
        ray = cam.ScreenPointToRay(ctx.ReadValue<Vector2>());
    }

    private void cursorClick( InputAction.CallbackContext ctx )
    {
        RaycastHit hit;
        
        if(Physics.Raycast(ray,out hit,500,_layerMask,QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.gameObject.GetComponent<UI3DButtonScript>() != null)
            {
                UI3DButtonScript button = hit.collider.gameObject.GetComponent<UI3DButtonScript>();
                
                button.MouseClick();
                
            }
        }
    }

    // Update is called once per frame

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(ray);
        Gizmos.DrawSphere(ray.GetPoint(1), 0.01f);
    }
}
