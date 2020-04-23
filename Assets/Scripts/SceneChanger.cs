using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public Image blackScreen;
    public float fadeTime;
    // Start is called before the first frame update
    void Start()
    {
        blackScreen.enabled = true;
        blackScreen.color = new Color(0,0,0,1);
        Tween t = blackScreen.DOFade(0f, fadeTime);
    }
    // Update is called once per frame
    public void LoadMain()
    {
        Tween t = blackScreen.DOFade(1f, fadeTime);
        t.onComplete += onFadeCompleteMain;
    }

    public void LoadMenu()
    {
        Tween t = blackScreen.DOFade(1f, fadeTime);
        t.onComplete += onFadeCompleteMenu;
    }

    public void onFadeCompleteMain()
    {
        SceneManager.LoadScene(1);
    }
    public void onFadeCompleteMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
