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
        Tween t = blackScreen.DOFade(0f, fadeTime);
    }
    // Update is called once per frame
    public void LoadMain()
    {
        Tween t = blackScreen.DOFade(1f, fadeTime);
        t.onComplete += onFadeComplete;
    }

    public void onFadeComplete()
    {
        SceneManager.LoadScene(1);
    }
}
