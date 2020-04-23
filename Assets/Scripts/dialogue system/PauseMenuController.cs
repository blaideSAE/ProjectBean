using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using DG.Tweening;
using PlayerControllerScripts.Firstperson;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject creditsPanel;
    public GameObject controlsPanel;
    public GameObject blockingPanel;

    private PlayerMovement _playerMovement;

    private CameraController _cameraController;

    private MessageBoxController _messageBoxController;

    private DialogueSystem _dialogueSystem;

    private SceneChanger _sceneChanager;

    public AnimationCurve MenuTweenCurve;
    public float tweenTime;
    private Vector3 defaultMenuScale;
    private Vector3 defaultCreditsScale;
    private Vector3 defaultControlsScale;

    public  InputAction MenuKey;
    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _cameraController = FindObjectOfType<CameraController>();
        _messageBoxController = FindObjectOfType<MessageBoxController>();
        _dialogueSystem = FindObjectOfType<DialogueSystem>();
        _sceneChanager = FindObjectOfType<SceneChanger>();
        MenuKey.Enable();
        MenuKey.performed += MenuKeyPressed;
        defaultMenuScale = mainPanel.transform.localScale;
        defaultCreditsScale = creditsPanel.transform.localScale;
        defaultControlsScale = controlsPanel.transform.localScale;
    }

    public void DisableMovement()
    {
        _cameraController.enabled = false;
        _playerMovement.enabled = false;
    }

    public void EnableMovement()
    {
        if (!_dialogueSystem.answerPanel.activeInHierarchy && !_dialogueSystem.questionPanel.activeInHierarchy &&
            !_messageBoxController.AnyMessagesActive())
        {
            _cameraController.enabled = true;
            _playerMovement.enabled = true; 
        }

    }

    public void MenuKeyPressed( InputAction.CallbackContext ctx)
    {
        if (mainPanel.activeInHierarchy)
        {
            Resume();
            //EnableMovement();
        }
        else
        {
            blockingPanel.SetActive(true);
            mainPanel.SetActive(true);
            DisableMovement();
            mainPanel.transform.localScale = Vector3.zero;
            mainPanel.transform.DOScale(defaultMenuScale, tweenTime * 2).SetEase(MenuTweenCurve);
            
        }
    }

    public void Resume()
    {
        blockingPanel.SetActive(false);   
        EnableMovement();
        mainPanel.transform.DOScale(0, tweenTime * 2).SetEase(Ease.Linear).OnComplete(()=>mainPanel.SetActive(false));
        
    }

    public void ExitCompletely()
    {
        Application.Quit();
    }

    public void ExitToMain()
    {
        _sceneChanager.LoadMenu();
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
        creditsPanel.transform.localScale = Vector3.zero;
        creditsPanel.transform.DOScale(defaultCreditsScale, tweenTime * 2).SetEase(MenuTweenCurve);
    }

    public void HideCredits()
    {
        creditsPanel.transform.DOScale(0, tweenTime * 2).SetEase(Ease.Linear).OnComplete(()=>creditsPanel.SetActive(false));
    }

    public void ShowControls()
    {
        controlsPanel.SetActive(true);
        controlsPanel.transform.localScale = Vector3.zero;
        controlsPanel.transform.DOScale(defaultControlsScale, tweenTime * 2).SetEase(MenuTweenCurve);
    }

    public void HideControls()
    {
        controlsPanel.transform.DOScale(0, tweenTime * 2).SetEase(Ease.Linear).OnComplete(()=> controlsPanel.SetActive(false));
    }

}
