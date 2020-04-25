using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MessageBoxCode : MonoBehaviour
{
    public GameObject ThisDialogBox;
    public Vector3 defaultScale;
    public float tweenTime;
    public AnimationCurve tweenCurve;

    private MessageBoxController _messageBoxController;
    // Start is called before the first frame update
    private void OnEnable()
    {
        defaultScale = ThisDialogBox.transform.localScale;
        ThisDialogBox.transform.localScale = Vector3.zero;
        ThisDialogBox.transform.DOScale(defaultScale, tweenTime * 2).SetEase(tweenCurve);
    }

    public void CloseThisDialog()
    {
        _messageBoxController = FindObjectOfType<MessageBoxController>();

        ThisDialogBox.transform.DOScale(Vector3.zero, tweenTime * 2).SetEase(Ease.Linear)
            .OnComplete(DialogClosed);

    }
    public void DialogClosed()
    {
        ThisDialogBox.SetActive(false);
        _messageBoxController.enablePlayerAndCameraMovement();
    }
}
