using System;
using System.Collections.Generic;
using PlayerControllerScripts.Firstperson;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
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

        public static event Action<List<BeanAnsweredQuestion>,bool> DialogrequestEvent;


        public PlayerMovement pM;
        public CameraController cC;

        private void Awake()
        {
            DialogueSystem.DialogrequestEvent += DisplayQuestions;
        }

        public void DisplayQuestionsWithRandomAnswers()
        {
            clearQuestions();
            questionPanel.SetActive(true);
            answerPanel.SetActive(false);
            questionPromptText.text = questionprompts[Random.Range(0, questionprompts.Count - 1)];
            pM.enabled = false;
            cC.enabled = false;
            
            DisplayQuestions(allQuestions);
        }

        public void DisplayQuestions( List<BeanAnsweredQuestion> answeredQuestions,bool randomAnswer = false)
        {
            clearQuestions();
            questionPanel.SetActive(true);
            answerPanel.SetActive(false);
            questionPromptText.text = questionprompts[Random.Range(0, questionprompts.Count - 1)];
            pM.enabled = false;
            cC.enabled = false;
            
            foreach (BeanAnsweredQuestion answeredQuestion in answeredQuestions)
            {
                GameObject questionButtonObject = Instantiate(questionPrefab,scrollViewContent.transform);
                questionButtonObject.GetComponentInChildren<TextMeshProUGUI>().text = answeredQuestion.question.body;
                QuestionButton questionButton = questionButtonObject.GetComponent<QuestionButton>();

                string answer;
                if (randomAnswer)
                {
                    answer = answeredQuestion.question.possibleAnswers[Random.Range(0, answeredQuestion.question.possibleAnswers.Count-1)]; 
                }
                else
                {
                    answer = answeredQuestion.answer;
                }

                questionButton.SetVariables(questionPanel,answerPanel,answerText,answer);
                questionsInScrollView.Add(questionButtonObject);
            }
        }

        public void DisplayQuestions(List<BeanQuestion> questions)
        {
            List<BeanAnsweredQuestion> answeredQuestions = new List<BeanAnsweredQuestion>();
            foreach (BeanQuestion question in questions)
            {
                answeredQuestions.Add(new BeanAnsweredQuestion(question,question.possibleAnswers[0]));
            }
            DisplayQuestions(answeredQuestions,true);
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

        public static void OnDialogrequestEvent(List<BeanAnsweredQuestion> obj,bool randomAnswer = false)
        {
            DialogrequestEvent?.Invoke(obj,randomAnswer);
        }
    }

    public class BeanAnsweredQuestion
    {
        public BeanQuestion question;
        public string answer;

        public BeanAnsweredQuestion(BeanQuestion question, string answer)
        {
            this.question = question;
            this.answer = answer;
        }
    }

    public class BeanQuestionAndAvailableAnswers
    {
        public BeanQuestion question;
        public List<string> availableAnsers;

        public BeanQuestionAndAvailableAnswers(BeanQuestion question)
        {
            this.question = question;
            availableAnsers = new List<string>();

            foreach (string s in question.possibleAnswers)
            {
             availableAnsers.Add(s);   
            }
        }
    }

    public class BeanAnsweredQuestionSet
    {
        public List<BeanAnsweredQuestion> answeredQuestions;
        public Color color;

        public BeanAnsweredQuestionSet(List<BeanAnsweredQuestion> answeredQuestions, Color color)
        {
            this.answeredQuestions = answeredQuestions;
            this.color = color;
        }
    }
}
