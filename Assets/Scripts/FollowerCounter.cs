using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowerCounter : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUgui;

    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        UpdateText();
    }

    private void Awake()
    {
        GameManager.followersChanged += UpdateText;
    }

    private void UpdateText()
    {
        textMeshProUgui.text = "Followers: " + gameManager.PlayersFollowers.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
