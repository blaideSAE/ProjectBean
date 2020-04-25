using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    
    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public AudioSource audioSource4;
    public AudioSource audioSource5;
    public float fadeTime;
    
    // Start is called before the first frame update
    void Start()
    {
        
        //audioSource.clip = MusicList[CurrentTrackIndex];
        // QueNextTrack();
    }

    public void MusicFade(AudioSource a, AudioSource b, float t)
    {
        a.DOFade(0, t).OnComplete(a.Stop);
        b.DOFade(1, t);
    }

    private void FixedUpdate()
    {

    }

    private void Awake()
    {
        GameManager.FirstDoorOpened += first;
        GameManager.SecondDoorOpened += second;
        GameManager.ThirdDoorOpened += third;
        GameManager.FinalButton += final;
    }

    public void first()
    {
        MusicFade(audioSource, audioSource2,fadeTime);
        
    }

    public void second()
    {
        MusicFade(audioSource2, audioSource3,fadeTime);
    }

    public void third()
    {
        MusicFade(audioSource3, audioSource4,fadeTime);
    }

    public void final()
    {
        MusicFade(audioSource4, audioSource5,fadeTime);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
