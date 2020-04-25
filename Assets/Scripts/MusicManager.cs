using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    public List<AudioClip> MusicList;

    public int CurrentTrackIndex;

    private bool songQued;

    public float timeUntilNext;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = MusicList[CurrentTrackIndex];
        audioSource.Play();
        QueNextTrack();
    }

    private void FixedUpdate()
    {
        if (songQued && timeUntilNext >= 0)
        {
            timeUntilNext -= Time.deltaTime;
        }
        else if(songQued)
        {
            audioSource.clip = MusicList[CurrentTrackIndex];
            audioSource.Play();
            songQued = false; //QueNextTrack();
        }
    }

    private void Awake()
    {
        GameManager.FirstDoorOpened += QueNextTrack;
        GameManager.SecondDoorOpened += QueNextTrack;
        GameManager.ThirdDoorOpened += QueNextTrack;
        GameManager.ApproachedEvilDoer += QueNextTrack;
        GameManager.FinalButton += QueNextTrack;
    }
    public void QueNextTrack()
    {
        CurrentTrackIndex += 1;
        songQued = true;
        timeUntilNext = audioSource.clip.length - audioSource.time;
       // audioSource.clip = MusicList[CurrentTrackIndex];
        //audioSource.PlayDelayed(TimeUntilNextWhenQued);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
