using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MainMenuShowCredits : MonoBehaviour
{
    public GameObject creditsPanel;

    private Vector3 defaultCreditsScale;

    public float tweenTime;

    public AnimationCurve tweenCurve;
    // Start is called before the first frame update
    void Start()
    {
        defaultCreditsScale = creditsPanel.transform.localScale;
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
        creditsPanel.transform.localScale = Vector3.zero;
        creditsPanel.transform.DOScale(defaultCreditsScale, tweenTime * 2).SetEase(tweenCurve);
    }

    public void HideCredits()
    {
        creditsPanel.transform.DOScale(0, tweenTime * 2).SetEase(Ease.Linear).OnComplete(()=>creditsPanel.SetActive(false));
    }
}
