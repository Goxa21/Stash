using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class LanguageManager : MonoBehaviour
{
    OptionsManager optionsManager;
    public SaveLoad saveLoad;
    public List<LocaleTextField> localeTextFields;
    public List<LocaleCheckModule> localeCheckModules;
    public Font[] fonts;
    CoreEventManager coreEventManager;
    private void Start()
    {
        coreEventManager = GetComponent<CoreEventManager>();
        optionsManager = GetComponent<OptionsManager>();
    }

    [Serializable]
    public class LocaleTextField
    {
        public string stringName;
        public string[] textContent;
    }

    public void Awake()
    {
        saveLoad = GameObject.Find("SaveLoadManager").GetComponent<SaveLoad>();
        switch (CoreSettings.languageActiveID)
        {
            case 0:
                Debug.Log("SelectedLanguage is EN");
                
                break;
            case 1:
                Debug.Log("SelectedLanguage is RU");

                break;
            case 2:
                Debug.Log("SelectedLanguage is ES");

                break;
            case 3:
                Debug.Log("SelectedLanguage is PT");

                break;
            case 4:
                Debug.Log("SelectedLanguage is DE");

                break;
            case 5:
                Debug.Log("SelectedLanguage is FR");

                break;
            case 6:
                Debug.Log("SelectedLanguage is ZH");

                break;
            case 7:
                Debug.Log("SelectedLanguage is JA");

                break;
            case 8:
                Debug.Log("SelectedLanguage is IT");

                break;
            case 9:
                Debug.Log("SelectedLanguage is KO");

                break;
            default:
                Debug.Log("WTF");

                break;
            
        }
    }
    public void ToggleLanguage()
    {
        coreEventManager.ClickButton.Invoke();

        if (CoreSettings.languageActiveID < 9)
        {
            CoreSettings.languageActiveID++;
        }
        else
        {
            CoreSettings.languageActiveID = 0;
        }

        switch (CoreSettings.languageActiveID)
        {
            case 0:
                Debug.Log("SelectedLanguage is EN");

                break;
            case 1:
                Debug.Log("SelectedLanguage is RU");

                break;
            case 2:
                Debug.Log("SelectedLanguage is ES");

                break;
            case 3:
                Debug.Log("SelectedLanguage is PT");

                break;
            case 4:
                Debug.Log("SelectedLanguage is DE");

                break;
            case 5:
                Debug.Log("SelectedLanguage is FR");

                break;
            case 6:
                Debug.Log("SelectedLanguage is ZH");

                break;
            case 7:
                Debug.Log("SelectedLanguage is JA");

                break;
            case 8:
                Debug.Log("SelectedLanguage is IT");

                break;
            case 9:
                Debug.Log("SelectedLanguage is KO");

                break;
            default:
                Debug.Log("WTF");

                break;

        }
        coreEventManager.SwitchLanguage.Invoke();
        optionsManager.UpdateLanguage();
        saveLoad.SaveCoreSettings();
    }
}
