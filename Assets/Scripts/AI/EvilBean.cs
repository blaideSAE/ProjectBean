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
    public Renderer beanRenderer;
    public Color startColor;
    
    
    BeanQuestion qOne;
    BeanQuestion qTwo;
    BeanQuestion qThree;
    

    public enum evilQuestionState
    {
        noQuestionsAsked,FirstquestionAsked,SecondquestionAsked,AllQuestionsAsked

    }

    public evilQuestionState qState;
    private bool busy = false;
    private float fadeTime;

    private void Start()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
        qState = evilQuestionState.noQuestionsAsked;
        qOne = ScriptableObject.CreateInstance<BeanQuestion>();
        qTwo = ScriptableObject.CreateInstance<BeanQuestion>();
        qThree = ScriptableObject.CreateInstance<BeanQuestion>();

        beanRenderer = GetComponent<Renderer>();
        fadeTime = FindObjectOfType<GameManager>().timetakenForBeansToGoGrey;

        //startColor = renderer.material.GetColor("_BaseColor");
        beanRenderer.material.SetColor("_BaseColor",startColor);
        beanRenderer.material.SetColor("_EmissionColor", new Color(startColor.r, startColor.g, startColor.b, 0f));
        
        qOne.body = questionOne;
        qTwo.body = questionTwo;
        qThree.body = questionThree;
        setupAnswers();
    }

    public void setupAnswers()
    {
        qOne.possibleAnswers = new List<string>();
        qOne.possibleAnswers.Add(answerOne);
        
        qTwo.possibleAnswers = new List<string>();
        qTwo.possibleAnswers.Add(answerTwo);

        qThree.possibleAnswers = new List<string>();
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
        GameManager.BeansGoGray += setMaterialToGrey;
        GameManager.FinalButton += setMaterialBackToStartColor;
    }

    private void OnDestroy()
    {
        GameManager.ApproachedEvilDoer -= onApproach;
        GameManager.BeansGoGray += setMaterialToGrey;
        GameManager.FinalButton += setMaterialBackToStartColor;
    }

    public void setMaterialToGrey()
    {
        Color g = Color.gray;
        beanRenderer.material.DOColor(g,"_BaseColor" ,FindObjectOfType<GameManager>().timetakenForBeansToGoGrey);
        beanRenderer.material.DOColor(new Color(g.r,g.g,g.b,0f),"_EmissionColor" ,FindObjectOfType<GameManager>().timetakenForBeansToGoGrey);
    }

    public void setMaterialBackToStartColor()
    {
        beanRenderer.material.SetColor("_BaseColor", startColor);
        beanRenderer.material.SetColor("_EmissionColor", new Color(startColor.r, startColor.g, startColor.b, 0f));

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
