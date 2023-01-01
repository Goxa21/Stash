using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoreSettings : MonoBehaviour
{
    public int targetedFPS;
    public static float audioSFXVolume;
    public static float audioMusicVolume;
    public static bool needToShowExtraReward;
    public static int languageActiveID;
    public static bool AccelerometerIsActive;


    public void Awake()
    {
        AudioListener.volume = 2f;
        DontDestroyOnLoad(gameObject);
        needToShowExtraReward = true;
        Application.targetFrameRate = targetedFPS;
    }
    private void Update()
    {
        print(audioMusicVolume);
    }
    public void SetTimeScaleTo(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}



