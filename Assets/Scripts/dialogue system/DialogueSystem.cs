using System;
using System.Collections.Generic;
using PlayerControllerScripts.Firstperson;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace AI
{
    public class DialogueSystem : MonoBehaviour
    {
        public List<string> questionprompts;
        public List<BeanQuestion> allQuestions;
        public GameObject questionPanel;
        public TextMeshProUGUI questionPromptText;
        public GameObject answerPanel;
        public TextMeshProUGUI answerText;
        public GameObject questionPrefab;
        public GameObject scrollViewContent;

        public List<GameObject> questionsInScrollView;

        public PlayerMovement pM;
        public CameraController cC;

        public void DisplayAllQuestions()
        {
            clearQuestions();
            questionPanel.SetActive(true);
            answerPanel.SetActive(false);
            questionPromptText.text = questionprompts[Random.Range(0, questionprompts.Count - 1)];
            pM.enabled = false;
            cC.enabled = false;
            foreach (BeanQuestion question in allQuestions)
            {
                GameObject questionButtonObject = Instantiate(questionPrefab,scrollViewContent.transform);
                questionButtonObject.GetComponentInChildren<TextMeshProUGUI>().text = question.body;
                QuestionButton questionButton = questionButtonObject.GetComponent<QuestionButton>();

                string answer = question.possibleAnswers[Random.Range(0, question.possibleAnswers.Count-1)];
                questionButton.SetVariables(questionPanel,answerPanel,answerText,answer);
                
                questionsInScrollView.Add(questionButtonObject);
            }
        }

        private void Start()
        {
            //DisplayAllQuestions();
        }

        private void Update()
        {
            
        }

        public void clearQuestions()
        {
            foreach (GameObject questionObject in questionsInScrollView)
            {
                Destroy(questionObject);
            }
        }

        public void CloseDialogue()
        {
            clearQuestions();
            questionPanel.SetActive(false);
            answerPanel.SetActive(false);
            pM.enabled = true;
            cC.enabled = true;
        }
    }

    public class BeanAnsweredQuestion
    {
        public BeanQuestion Question;
        public string Answer;
    }
}
