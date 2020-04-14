using UnityEngine;
using UnityEngine.Events;

namespace ButtonScripts
{
    public class ButtonPress : MonoBehaviour,IInteractable
    {
        public bool buttonState;
        public float baseSpeed;
    
        public AnimationCurve speedCurve;
    

        public float buttonBlendShapeValue;

        public SkinnedMeshRenderer _skinnedMeshRenderer;

        public UnityEvent OnPress;
        // Start is called before the first frame update
        void Start()
        {
            _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }

        public void OnInteraction()
        {
            buttonState = !buttonState;
                OnPress?.Invoke();
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
}
