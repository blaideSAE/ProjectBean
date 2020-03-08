using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressAnimation : MonoBehaviour
{
    public bool buttonState;
    public float baseSpeed;
    
    public AnimationCurve speedCurve;
    

    public float buttonBlendShapeValue;

    public SkinnedMeshRenderer _skinnedMeshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (buttonState && buttonBlendShapeValue > 0f)
        {
            buttonBlendShapeValue -= baseSpeed *0.01f; 
            _skinnedMeshRenderer.SetBlendShapeWeight(0,speedCurve.Evaluate(buttonBlendShapeValue));
        }
        else if (!buttonState && buttonBlendShapeValue < 1f)
        {

            buttonBlendShapeValue += baseSpeed *0.01f;
            _skinnedMeshRenderer.SetBlendShapeWeight(0,speedCurve.Evaluate(buttonBlendShapeValue));
        }
        
    }
}
