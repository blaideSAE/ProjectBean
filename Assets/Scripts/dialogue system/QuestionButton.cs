using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionButton : MonoBehaviour
{
    public GameObject questionPanel;
    public GameObject answerPanel;
    public TextMeshProUGUI answerText;
    public String answer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetVariables(GameObject _questionPanel, GameObject _answerPanel, TextMeshProUGUI _answerText, string _answer)
    {
        questionPanel = _questionPanel;
        answerPanel = _answerPanel;
        answerText = _answerText;
        answer = _answer;
    }

    public void DisplayAnAnswer() 
    {
        questionPanel.SetActive(false);
        answerPanel.SetActive(true);
        answerText.text = answer;
    }

    // Update is called once per frame
}
