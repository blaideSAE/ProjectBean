using System;
using System.Collections;
using System.Collections.Generic;
using PlayerControllerScripts.Firstperson;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static event Action GameStart;

    public static event Action BeansGoGray;

    public static event Action AfterBeansGoGray;

    public static event Action FirstDoorOpened;
    
    public static event Action SecondDoorOpened;
    
    public static event Action ThirdDoorOpened;
    
    public static event Action ApproachedEvilDoer;
    
    public static event Action GameOver;

    public static event Action FirstFailFollowRequest;
    
    public static event Action FirstSuccessFollowRequest;

    public static event Action followersChanged; 

    private Vector3 playerStartPosition;
    private PlayerMovement _playerMovement;
    public float distanceBeforeStartMessage;

    public bool failedYet = false;
    public bool succeededYet = false;

    public enum GameStates
    {
        PlayerhasntMoved,GameJustStarted,BeansGoingGrey,BeansHaveGoneGrey,
    }

    public GameStates gameState;

    public float timer;
    public float timeUntilBeansGoGrey;
    public float timetakenForBeansToGoGrey;

    public List<Bean> PlayersFollowers;
    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        playerStartPosition = _playerMovement.transform.position;
        PlayersFollowers = new List<Bean>();
    }

    public void GameStartDialogClosed()
    {
        gameState = GameStates.GameJustStarted;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(playerStartPosition, _playerMovement.transform.position) >= distanceBeforeStartMessage && gameState == GameStates.PlayerhasntMoved)
        {
            
            OnGameStart();
        }
        else if (gameState == GameStates.GameJustStarted)
        {
            if (timer < timeUntilBeansGoGrey)
            {
                timer += Time.deltaTime;
            }
            else
            {
                gameState = GameStates.BeansGoingGrey;
                OnBeansGoGray();
                timer = 0;
            }
        }
        else if( gameState == GameStates.BeansGoingGrey)
        {
            if (timer < timetakenForBeansToGoGrey)
            {
                timer += Time.deltaTime;
            }
            else
            {
                gameState = GameStates.BeansHaveGoneGrey;
                OnAfterbeansGoGray();
            }
        }
        else if (gameState == GameStates.BeansHaveGoneGrey)
        {
            //Not sure i have to do anything here.
        }
        
    }

    public void doorOpned(int doorNumber)
    {
        if (doorNumber == 1)
        {
            OnFirstDoorOpened();
        }
        else if (doorNumber == 2)
        {
            OnSecondDoorOpened();
        }
        else if (doorNumber == 3)
        {
            OnThirdDoorOpened();
        }
    }


    private static void OnGameStart()
    {
        GameStart?.Invoke();
    }

    private static void OnBeansGoGray()
    {
        BeansGoGray?.Invoke();
    }

    private static void OnAfterbeansGoGray()
    {
        AfterBeansGoGray?.Invoke();
    }

    private static void OnFirstDoorOpened()
    {
        FirstDoorOpened?.Invoke();
    }

    private static void OnSecondDoorOpened()
    {
        SecondDoorOpened?.Invoke();
    }

    private static void OnThirdDoorOpened()
    {
        ThirdDoorOpened?.Invoke();
    }

    private static void OnApproachedEvilDoer()
    {
        ApproachedEvilDoer?.Invoke();
    }

    private static void OnGameOver()
    {
        GameOver?.Invoke();
    }

    public static void OnFirstFailFollowRequest()
    {
        FirstFailFollowRequest?.Invoke();
    }

    public static void OnFirstSuccessFollowRequest()
    {
        FirstSuccessFollowRequest?.Invoke();
    }

    public static void OnFollowersChanged()
    {
        followersChanged?.Invoke();
    }
}
