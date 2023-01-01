using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    public GameObject background;
    public CoreEventManager coreEventManager;

    public void Awake()
    {
        coreEventManager.startCutsceneEnded.AddListener(DisableBackground);
        coreEventManager.backToMenu.AddListener(EnableBackground);
    }

    public void EnableBackground()
    {
        background.SetActive(true);
    }

    public void DisableBackground()
    {
        background.SetActive(false);
    }
}
