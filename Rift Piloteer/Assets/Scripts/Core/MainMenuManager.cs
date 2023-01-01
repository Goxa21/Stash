using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public CoreEventManager coreEventManager;
    public FlashBangMainControl flashBangMainControl;
    public Animator mainMenuAnim;
    public Animator globalCameraController;
    public SaveLoad saveLoad;
    ShipReplacer Replacer;

    public void Awake()
    {
        coreEventManager = GameObject.Find("Managers").GetComponent<CoreEventManager>();
        coreEventManager.ShowResults.AddListener(GoToRunResults);
        coreEventManager.backToMenu.AddListener(GoToMainMenu);
        Replacer = GetComponent<ShipReplacer>();
    }
    
    public void GoToMainMenu()
    {
        coreEventManager.ClickBackButton.Invoke();
        mainMenuAnim.Play("MainMenu");
        globalCameraController.Play("Main");
    }

    public void GoToShipSelect()
    {
        coreEventManager.ClickButton.Invoke();
        mainMenuAnim.Play("SelectScreen");
        globalCameraController.Play("Select");
        Replacer.UpdateInfo();
    }

    public void GoToOptions()
    {
        coreEventManager.ClickButton.Invoke();
        mainMenuAnim.Play("Options");
        globalCameraController.Play("Options");
    }

    public void GoToGamePlay()
    {
        coreEventManager.ClickButton.Invoke();
        mainMenuAnim.Play("GamePlay");
        coreEventManager.gameStart.Invoke();
        saveLoad.ExtraSave();
    }

    public void GoToRunResults()
    {
        mainMenuAnim.Play("RunResults");
    }

    public void CloseResults()
    {
        coreEventManager.ClickButton.Invoke();
        coreEventManager.backToMenu.Invoke();
        flashBangMainControl.FlareEnd();
    }

    public void GoToQuit()
    {
        Application.Quit();
    }
}
