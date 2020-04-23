using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 OpenPosition;
    private Vector3 startPosition;
    public float moveTime;
    public AnimationCurve moveCurve;

    private Bounds bounds;

    private bool doorOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggle()
    {
        if (doorOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public void Open()
    {
        transform.DOMove(transform.position + OpenPosition, moveTime).SetEase(moveCurve)
            .OnComplete((() => doorOpen = true));
    }

    public void Close()
    {
        transform.DOMove(startPosition, moveTime).SetEase(moveCurve)
            .OnComplete((() => doorOpen = false));
    }

    private void OnDrawGizmosSelected()
    {
        if (startPosition == Vector3.zero)
        {
            bounds = GetComponent<Collider>().bounds;
            Gizmos.DrawWireCube( transform.position + OpenPosition,bounds.size);
        }
        else
        {
            
            bounds = GetComponent<Collider>().bounds;
            Gizmos.DrawWireCube( startPosition + OpenPosition ,bounds.size);
        }

    }
}
