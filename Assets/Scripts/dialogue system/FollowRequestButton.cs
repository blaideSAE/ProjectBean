using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class FollowRequestButton : MonoBehaviour
{
    public Bean bean;
    public GameObject questionPanel;
    public GameObject answerPanel;
    public TextMeshProUGUI answerText;

    public string helpfulString;
    public string unHelpfulString;

    public void RequestFollow()
    {
        
        String s;
        GameManager g = FindObjectOfType<GameManager>();
        if (bean != null)
        {


            if (bean.helpful)
            {
                bean.helpState = Bean.HelpState.Following;
                s = helpfulString;
                g.PlayersFollowers.Add(bean);
                if (!g.succeededYet)
                {
                    g.succeededYet = true;
                    GameManager.OnFirstSuccessFollowRequest();
                }
            }
            else
            {
                s = unHelpfulString;
                if (g.PlayersFollowers.Count > 0)
                {
                    Bean b = g.PlayersFollowers[Random.Range(0, g.PlayersFollowers.Count)];
                    g.PlayersFollowers.Remove(b);
                    b.helpState = Bean.HelpState.WaitingToHelp;
                }

                if (!g.failedYet)
                {
                    g.failedYet = true;
                    GameManager.OnFirstFailFollowRequest();
                }

            }

            questionPanel.SetActive(false);
            answerPanel.SetActive(true);
            answerText.text = s;
        }
        GameManager.OnFollowersChanged();
    }
    public void SetVariables(GameObject _questionPanel, GameObject _answerPanel, TextMeshProUGUI _answerText,Bean _bean)
    {
        questionPanel = _questionPanel;
        answerPanel = _answerPanel;
        answerText = _answerText;
        bean = _bean;
    }
}
