using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using PlayerControllerScripts.Firstperson;
using UnityEngine;

public class MessageBoxController : MonoBehaviour
{
    public GameObject startMessage;
    public GameObject goneGreyMessage;
    public GameObject firstDoorMessage;
    public GameObject secondDoorMessage;
    public GameObject thirdDoorMessage;
    public GameObject approachEvilDoerMessage;
    public GameObject finalButtonMessage;

    public PlayerMovement playerMovement;
    public CameraController cameraController;
    public DialogueSystem dialogueSystem;

    public List<GameObject> allMessageBoxes;
    private void Start()
    {
        startMessage.SetActive(false);
        goneGreyMessage.SetActive(false);
        firstDoorMessage.SetActive(false);
        secondDoorMessage.SetActive(false);
        thirdDoorMessage.SetActive(false);
        approachEvilDoerMessage.SetActive(false);
        playerMovement = FindObjectOfType<PlayerMovement>();
        cameraController = FindObjectOfType<CameraController>();
        dialogueSystem = FindObjectOfType<DialogueSystem>();
        
        allMessageBoxes.Add(startMessage);
        allMessageBoxes.Add(goneGreyMessage);
        allMessageBoxes.Add(firstDoorMessage);
        allMessageBoxes.Add(secondDoorMessage);
        allMessageBoxes.Add(thirdDoorMessage);
        allMessageBoxes.Add(approachEvilDoerMessage);
    }

    private void Awake()
    {
        GameManager.GameStart += DisplayStartMessage;
        GameManager.AfterBeansGoGray += DisplayGoneGreyMessage;
        GameManager.FirstDoorOpened += DisplayfirstDoorMessage;
        GameManager.SecondDoorOpened += DisplaySecondDoorMessage;
        GameManager.ThirdDoorOpened += DisplayThirdDoorMessage;
        GameManager.ApproachedEvilDoer += DisplayApproachEvilDoerMessage;
        GameManager.FinalButton += DisplayFinalMessage;
    }

    public bool AnyMessagesActive()
    {
        bool s = false;
        foreach (GameObject gg in allMessageBoxes)
        {
            if (gg.activeInHierarchy)
            {
                s = true;
            }
        }
        return s;
    }

    public bool AnyOtherMesagesActive( GameObject g)
    {
        bool s = false;
        foreach (GameObject gg in allMessageBoxes)
        {
            if (gg != g && gg.activeInHierarchy)
            {
                s = true;
            }
        }
        return s;
    }


    public void disablePlayerAndCameraMovement()
    {
        playerMovement.enabled = false;
        cameraController.enabled = false;
       // Cursor.lockState = CursorLockMode.None;
    }

    public void enablePlayerAndCameraMovement()
    {
        if (dialogueSystem.answerPanel.activeInHierarchy || dialogueSystem.questionPanel.activeInHierarchy || AnyMessagesActive())
        {
        }
        else
        {
            playerMovement.enabled = true;
            cameraController.enabled = true;
        }
    }

    public void DisplayStartMessage()
    {
        startMessage.SetActive(true);
        disablePlayerAndCameraMovement();
    }
    public void DisplayGoneGreyMessage()
    {
        goneGreyMessage.SetActive(true);
        disablePlayerAndCameraMovement();
    }
    public void DisplayfirstDoorMessage()
    {
        firstDoorMessage.SetActive(true);
        disablePlayerAndCameraMovement();
    }
    public void DisplaySecondDoorMessage()
    {
        secondDoorMessage.SetActive(true);
        disablePlayerAndCameraMovement();
    }
    public void DisplayThirdDoorMessage()
    {
        thirdDoorMessage.SetActive(true);
        disablePlayerAndCameraMovement();
    }

    public void DisplayApproachEvilDoerMessage()
    {
        approachEvilDoerMessage.SetActive(true);
        disablePlayerAndCameraMovement();
    }

    public void DisplayFinalMessage()
    {
        finalButtonMessage.SetActive(true);
        disablePlayerAndCameraMovement();
    }
}
