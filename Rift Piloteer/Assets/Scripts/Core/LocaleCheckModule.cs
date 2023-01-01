using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[DefaultExecutionOrder(1)]


public class LocaleCheckModule : MonoBehaviour
{
    public LanguageManager languageManager;
    public LanguageManager.LocaleTextField localeTextField;
    public LocaleCheckModule thisModule;
    public string fieldName;
    public bool needToFill;
    public Text thisTextField;
    CoreEventManager coreEventManager;
    public void Awake()
    {
        languageManager = GameObject.Find("Managers").GetComponent<LanguageManager>();
        thisTextField = gameObject.GetComponent<Text>();
        thisModule = gameObject.GetComponent<LocaleCheckModule>();
        languageManager.localeCheckModules.Add(thisModule);
        coreEventManager = GameObject.Find("Managers").GetComponent<CoreEventManager>();
        coreEventManager.SwitchLanguage.AddListener(UpdateVisuals);
        UpdateVisuals();
    }
    public void UpdateVisuals()
    {
        localeTextField = languageManager.localeTextFields.Find(delegate (LanguageManager.LocaleTextField lcf)
        {
            if (lcf.stringName == fieldName)
                return true;
            else
                return false;
        });
        if (needToFill)
            thisTextField.text = localeTextField.textContent[CoreSettings.languageActiveID];
        if (languageManager.fonts[CoreSettings.languageActiveID] != null)
            thisTextField.font = languageManager.fonts[CoreSettings.languageActiveID];
    }
}
