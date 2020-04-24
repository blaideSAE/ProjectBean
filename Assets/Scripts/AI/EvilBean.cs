using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class EvilBean : MonoBehaviour
{
    public string questionOne;
    public string answerOne;
    public string questionTwo;
    public string answerTwo;
    public string questionThree;
    public string answerThree;
    public DialogueSystem dialogueSystem;
   // public List<BeanAnsweredQuestion> dialogue;
    public GameObject modifiedAnswerPanel;
    public TextMeshProUGUI modifiedAnswerText;

    public GameObject ThemText;
    public GameObject ThemBackGround;
    
    
    BeanQuestion qOne;
    BeanQuestion qTwo;
    BeanQuestion qThree;
    

    public enum evilQuestionState
    {
        noQuestionsAsked,FirstquestionAsked,SecondquestionAsked,AllQuestionsAsked

    }

    public evilQuestionState qState;
    private bool busy = false;

    private void Start()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
        qState = evilQuestionState.noQuestionsAsked;
        qOne = ScriptableObject.CreateInstance<BeanQuestion>();
        qTwo = ScriptableObject.CreateInstance<BeanQuestion>();
        qThree = ScriptableObject.CreateInstance<BeanQuestion>();

        qOne.body = questionOne;
        qTwo.body = questionTwo;
        qThree.body = questionThree;
        
        qOne.possibleAnswers.Add(answerOne);
        qTwo.possibleAnswers.Add(answerTwo);
        qThree.possibleAnswers.Add(answerThree);
    }

    public List<BeanAnsweredQuestion> dialogue( BeanQuestion bQ,string ans)
    {
        List<BeanAnsweredQuestion> bnQs = new List<BeanAnsweredQuestion>();
        
        bnQs.Add(new BeanAnsweredQuestion(bQ,ans));
        return bnQs;
    }

    private void Awake()
    {
        GameManager.ApproachedEvilDoer += onApproach;
    }

    public void onApproach()
    {
        if (!busy)
        {
            busy = true;
            transform.DOShakeScale(1, Vector3.one * 0.1f).OnComplete(() => busy = false);
        }
        
        dialogueSystem.answerPanel.SetActive(false);
        FindObjectOfType<DialogueSystem>().Lastbean = null;
        dialogueSystem.answerPanel = modifiedAnswerPanel;
        dialogueSystem.answerText = modifiedAnswerText;
        DialogueSystem.OnDialogrequestEvent(dialogue(qOne,answerOne));
        qState = evilQuestionState.FirstquestionAsked;
    }

    public void DisplayQuestion()
    {
        ThemText.SetActive(false);
        ThemBackGround.SetActive(false);
        //dialogueSystem.CloseDialogue();
        if (qState == evilQuestionState.FirstquestionAsked)
        {
            DialogueSystem.OnDialogrequestEvent(dialogue(qTwo, answerTwo));
            qState = evilQuestionState.SecondquestionAsked;
        }
        else if (qState == evilQuestionState.SecondquestionAsked)
        {
            DialogueSystem.OnDialogrequestEvent(dialogue(qThree, answerThree));
            qState = evilQuestionState.AllQuestionsAsked;
        }
        else if (qState == evilQuestionState.AllQuestionsAsked)
        {
            ThemText.SetActive(true);
            ThemBackGround.SetActive(true);
            dialogueSystem.CloseDialogue();
        }
        
    }


}
