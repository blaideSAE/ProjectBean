using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Bean : MonoBehaviour,IInteractable
{
    public Color color;
    public bool busy;
    public bool scaling;
    public bool shaking;
    public bool rotating;
    public MeshRenderer renderer;
    public List<BeanAnsweredQuestion> dialogue;
    
    
    public List<string> answers;

    public float timer;
    public float timeLimit;
    

    public void OnInteraction()
    {
        if (!busy)
        {
            busy = true;
            Tween t =
            transform.DOShakeScale(1, Vector3.one* 0.1f);
            t.onComplete += NotBusy;
        }
        DialogueSystem.OnDialogrequestEvent(dialogue);
    }

    private void Start()
    {
        renderer = gameObject.GetComponent<MeshRenderer>();
    }

    public void setMaterialToColor( Color _color)
    {
        color = _color;
        renderer.material.SetColor("_BaseColor",color);
       // material.SetColor("_BaseColor",color);
    }

    public void setMaterialToGrey()
    {
        renderer.material.color = Color.grey;
    }

    private void FixedUpdate()
    {
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
                Tween t = transform.DOJump(transform.position,0.2f,1,0.5f,false);
                t.onComplete += NotBusy;
                timer = 0;
                timeLimit = Random.Range(0f, 3f);

            }
        }


    }

    public void NotBusy()
    {
        busy = false;
    }
}

