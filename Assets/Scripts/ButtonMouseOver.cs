
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonMouseOver : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private bool mouseOver;
    private bool busy = false;
    private Vector3 originalSize;

    private Tween tweenEnter;
    private Tween tweenLeave;

    public float sizeMultiplier = 0.02f;

    private void Start()
    {
        originalSize = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        if (!busy)
        {
            busy = true;
            tweenEnter = gameObject.transform.DOScale(originalSize + Vector3.one * sizeMultiplier, 0.5f).SetEase(Ease.OutBounce);
            tweenEnter.onComplete += notBusy;
        }
        else if (tweenLeave != null && tweenLeave.IsActive())
        {
            tweenLeave.Complete();
            busy = true;
            tweenEnter = gameObject.transform.DOScale(originalSize + Vector3.one * sizeMultiplier, 0.5f).SetEase(Ease.OutBounce);
            tweenEnter.onComplete += notBusy;
        }
    }

    public void notBusy()
    {
        busy = false;
    }

    public void LeaveAnim()
    {
        
        tweenLeave = gameObject.transform.DOScale(originalSize, 0.5f).SetEase(Ease.OutBounce)
            .OnComplete(() => busy = false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        if (!busy)
        {
            busy = true;
            tweenLeave = gameObject.transform.DOScale(originalSize, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => busy = false);
        }
        else if( tweenEnter.IsPlaying())
        {
            tweenEnter.onComplete += LeaveAnim;




        }
        mouseOver = false;    
    }
}