using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneLoadManager : MonoBehaviour
{
    public Slider slider;
    public Text textM;
    public bool isIntro;
    public Animator flashBang;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (isIntro)
        {
            StartCoroutine("Intro");
        }
    }

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(0.5f);
        LoadScene(1);
    }

    public void LoadScene(int sceneName)
    {
        StartCoroutine("AsyncLoadSequence", sceneName);
    }

    IEnumerator AsyncLoadSequence(int sceneName)
    {
        //SceneManager.LoadScene(1);
        yield return new WaitForEndOfFrame();
        Debug.Log("TargetSceneAsyncLoad:" + sceneName);

        yield return new WaitForSeconds(0.5f);
        //slider = GameObject.Find("LoadSlider").GetComponent<Slider>();
        //textM = GameObject.Find("TextM").GetComponent<Text>();
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
        asyncOp.allowSceneActivation = false;
        float targetValue = 0f;
        while (asyncOp.progress < 0.9f || targetValue < 0.9f)
        {
            yield return new WaitForEndOfFrame();

            //float asyncProgress = Mathf.Clamp01(asyncOp.progress / 0.9f);
            float asyncProgress = (asyncOp.progress / 0.9f);
            Debug.Log(asyncProgress);
            //textM.text = Mathf.Round(asyncProgress) * 100f + "%";
            if (targetValue < asyncProgress)
            {
                targetValue += 0.01f;
                yield return new WaitForSeconds(0.02f);

            }
            //textM.text = Mathf.Round((targetValue) * 100f) + "%";
            slider.value = targetValue;
            
        }

        flashBang.Play("Flare");
        while (targetValue < 1)
        {
            targetValue += 0.002f;
            yield return new WaitForSeconds(0.02f);
            slider.value = targetValue;
        }

        //textM.text = 100f + "%";
        slider.value = 1f;

        yield return new WaitForSeconds(0.1f);
        asyncOp.allowSceneActivation = true;


    }
}
