﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class UI3DButtonScript : MonoBehaviour
{
    public bool busy = false;
    public float Time;
    public AnimationCurve speedCurve;
    public float buttonBlendShapeValue = -200f;
    public SkinnedMeshRenderer _skinnedMeshRenderer;
    public UnityEvent OnPress;

    public UnityEvent OnHover;
    // Start is called before the first frame update
    void Start()
    {
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        DOTween.Init();
        
    }

    public void MouseClick()
    {
        if (!busy)
        {
            busy = true;
            OnPress?.Invoke();
            Tween t = DOTween.To(() => buttonBlendShapeValue, x => buttonBlendShapeValue = x, 120f, Time)
                .SetEase(speedCurve);

            t.onComplete += NotBusy;
        }
    }

    void NotBusy()
    {
        busy = false;
    }

    public void MouseHover()
    {
        OnHover?.Invoke();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _skinnedMeshRenderer.SetBlendShapeWeight(0,buttonBlendShapeValue);
    }
}
