using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[DefaultExecutionOrder(-2)]
public class SaveLoad : MonoBehaviour
{
    public Save sf = new Save();
    private string path;
    public int totalShipNumber;


    void Awake()
    {
        GameObject[] slm = GameObject.FindGameObjectsWithTag("SaveLoadManager");
        if (slm.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Debug.Log("persistentDataPath is: " + Application.persistentDataPath);
        path = Path.Combine(Application.persistentDataPath, "RiftData.json");
        if (File.Exists(path))
        {
            sf = JsonUtility.FromJson<Save>(File.ReadAllText(path));
            loadSettingsToCore();
        }
        else
        {
            sf.inHangar = new bool[totalShipNumber];
            sf.inHangar[0] = true;
            sf.records = new int[totalShipNumber];
            sf.audioMusicVolume = 1f;
            sf.audioSFXVolume = 1f;
            sf.curVow = 0;
            sf.curShipSelected = 0;
            sf.lastLaunchDay = DateTime.Now.Day;
            sf.lastLaunchMonth = DateTime.Now.Month;
            sf.lastLaunchYear = DateTime.Now.Year;
            sf.lastLaunchHour = DateTime.Now.Hour;
            sf.lastLaunchMinute = DateTime.Now.Minute;
            sf.isAccelerometerActive = false;
            if (System.Globalization.CultureInfo.CurrentCulture + "" == "ru-RU")
            {
                sf.languageIDActive = 1;
            }
            else
            {
                sf.languageIDActive = 0;
            }
            // значения по умолчанию
            File.WriteAllText(path, JsonUtility.ToJson(sf));
            loadSettingsToCore();
        }
    }

    public void ResetSaveFile()
    {
        sf.inHangar = new bool[totalShipNumber];
        sf.inHangar[0] = true;
        sf.records = new int[totalShipNumber];
        sf.audioMusicVolume = 1f;
        sf.audioSFXVolume = 1f;
        sf.curShipSelected = 0;
        sf.curVow = 0;
        sf.lastLaunchDay = DateTime.Now.Day;
        sf.lastLaunchMonth = DateTime.Now.Month;
        sf.lastLaunchYear = DateTime.Now.Year;
        sf.lastLaunchHour = DateTime.Now.Hour;
        sf.lastLaunchMinute = DateTime.Now.Minute;
        sf.isAccelerometerActive = false;
        if (System.Globalization.CultureInfo.CurrentCulture + "" == "ru-RU")
        {
            sf.languageIDActive = 1;
        }
        else
        {
            sf.languageIDActive = 0;
        }
        // значения по умолчанию
        File.WriteAllText(path, JsonUtility.ToJson(sf));
        loadSettingsToCore();
    }

    public void DebugAddResources()
    {
        sf.curVow += 1000;
        File.WriteAllText(path, JsonUtility.ToJson(sf));
    }


    [Serializable]
    public class Save
    {
        public bool[] inHangar;
        public int[] records;
        public int curShipSelected;
        public float audioSFXVolume;
        public float audioMusicVolume;
        public int lastLaunchDay;
        public int lastLaunchMonth;
        public int lastLaunchYear;
        public int lastLaunchHour;
        public int lastLaunchMinute;
        public int languageIDActive;
        public int curVow;
        public bool isAccelerometerActive;
        // Сохраняемые данные
    }


    public void ExtraSave()
    {
        sf.lastLaunchDay = DateTime.Now.Day;
        sf.lastLaunchMonth = DateTime.Now.Month;
        sf.lastLaunchYear = DateTime.Now.Year;
        sf.lastLaunchHour = DateTime.Now.Hour;
        sf.lastLaunchMinute = DateTime.Now.Minute;
        File.WriteAllText(path, JsonUtility.ToJson(sf));
        Debug.Log("SaveFileOverride");

    }

    public void loadSettingsToCore()
    {
        CoreSettings.languageActiveID = sf.languageIDActive;
        CoreSettings.audioMusicVolume = sf.audioMusicVolume;
        CoreSettings.audioSFXVolume = sf.audioSFXVolume;
        CoreSettings.AccelerometerIsActive = sf.isAccelerometerActive;
    }

    public void SaveCoreSettings()
    {
        sf.languageIDActive = CoreSettings.languageActiveID;
        sf.audioMusicVolume = CoreSettings.audioMusicVolume;
        sf.audioSFXVolume = CoreSettings.audioSFXVolume;
        sf.isAccelerometerActive = CoreSettings.AccelerometerIsActive;
        ExtraSave();
    }



    //private void OnApplicationQuit()
    //{
    //    File.WriteAllText(path, JsonUtility.ToJson(sf));
    //}

}

