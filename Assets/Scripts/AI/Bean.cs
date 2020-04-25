using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using DG.Tweening;
using PlayerControllerScripts.Firstperson;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Bean : MonoBehaviour,IInteractable
{
    public Color StartColor;
    public Color CurrentColor;
    public bool busy;
    public bool scaling;
    public bool shaking;
    public bool rotating;
    public MeshRenderer renderer;
    public List<BeanAnsweredQuestion> dialogue;
    public NavMeshAgent navMeshAgent;
    public List<string> answers;
    public float minFollowDistance = 3;

    private PlayerMovement _playerMovement;

    public float timer;
    public float timeLimit;

    public bool helpful;

    public Vector3 startPosition;
    public enum HelpState
    {
        Following,AtPressurePlate,WaitingToHelp
    };

    public HelpState helpState = HelpState.WaitingToHelp;

    private float colorFadeRatio = 0;

    public void OnInteraction()
    {
        if (!busy)
        {
            busy = true;
            Tween t =
            transform.DOShakeScale(1, Vector3.one* 0.1f);
            t.onComplete += NotBusy;
        }
        FindObjectOfType<DialogueSystem>().Lastbean = this;
        DialogueSystem.OnDialogrequestEvent(dialogue);
        
    }

    private void Start()
    {
        renderer = gameObject.GetComponent<MeshRenderer>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
    }

    private void Awake()
    {
        GameManager.BeansGoGray += setMaterialToGrey;
    }

    private void OnDestroy()
    {
        GameManager.BeansGoGray -= setMaterialToGrey;
    }

    public void setMaterialToColor( Color _color)
    {
        StartColor = _color;
        renderer.material.SetColor("_BaseColor",StartColor);
        renderer.material.SetColor("_EmissionColor",new Color(StartColor.r,StartColor.g,StartColor.b,0f));
        // material.SetColor("_BaseColor",color);
        
    }

    public void setMaterialToGrey()
    {
        Color g = Color.gray;
        renderer.material.DOColor(g,"_BaseColor" ,FindObjectOfType<GameManager>().timetakenForBeansToGoGrey);
        renderer.material.DOColor(new Color(g.r,g.g,g.b,0f),"_EmissionColor" ,FindObjectOfType<GameManager>().timetakenForBeansToGoGrey);
    }

    private void FixedUpdate()
    {
        if (helpState == HelpState.WaitingToHelp)
        {
            if (Vector3.Distance(transform.position, startPosition) < 0.2f)
            {
                navMeshAgent.isStopped = true;
                if (timer <= timeLimit)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    if (!busy)
                    {
                        //setMaterialToColor();
                        busy = true;
                        Tween t = transform.DOJump(transform.position, 0.2f, 1, 0.5f, false);
                        t.onComplete += NotBusy;
                        timer = 0;
                        timeLimit = Random.Range(0f, 3f);
                    }
                }
            }
            else
            {
                navMeshAgent.SetDestination(startPosition);
                navMeshAgent.isStopped = false;
            }
        }
        else if (helpState == HelpState.Following)
        {
            if (Vector3.Distance(transform.position, _playerMovement.transform.position) > minFollowDistance)
            {
                navMeshAgent.SetDestination(_playerMovement.transform.position);
                navMeshAgent.isStopped = false;
            }
            else
            {
                navMeshAgent.isStopped = true;
            }
        }
        else if( helpState == HelpState.AtPressurePlate)
        {
            navMeshAgent.isStopped = true;
        }
        
    }

    public void NotBusy()
    {
        busy = false;
    }
}

