using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace ButtonScripts
{
    public class ButtonPress : MonoBehaviour,IInteractable
    {
        public bool busy;
        public float time;
        public AnimationCurve speedCurve;
        public float buttonBlendShapeValue;
        public SkinnedMeshRenderer _skinnedMeshRenderer;
        public UnityEvent OnPress;

        public bool sendEventOnce;

        private bool eventSent = false;
        // Start is called before the first frame update
        void Start()
        {
            _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }

        public void OnInteraction()
        {
            if (!busy)
            {
                busy = true;
                if ( !(sendEventOnce && eventSent))
                {
                    OnPress?.Invoke();
                    eventSent = true;
                }

                
                Tween t = DOTween.To(() => buttonBlendShapeValue, x => buttonBlendShapeValue = x, 120f, time)
                    .SetEase(speedCurve);

                t.onComplete += NotBusy;
            }
        }
        void NotBusy()
        {
            busy = false;
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            if (busy)
            {
                _skinnedMeshRenderer.SetBlendShapeWeight(0, buttonBlendShapeValue);
            }
        }
    }
}
