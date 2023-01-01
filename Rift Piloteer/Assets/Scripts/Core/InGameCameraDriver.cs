using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(5)]

public class InGameCameraDriver: MonoBehaviour
{
    //public Transform shipPosition;
    public Transform cameraDriverPosition;
    public CoreEventManager coreEventManager;
    public Animator inGameAnim;

    public void Awake()
    {
        coreEventManager = GameObject.Find("Managers").GetComponent<CoreEventManager>();
        coreEventManager.gameStart.AddListener(PlayCutscene);
    }

    public void PlayCutscene()
    {
        inGameAnim.Play("Intro");
    }

    public void Update()
    {
        cameraDriverPosition.position = new Vector3(ShipReplacer.curShip.transform.position.x,0,0);
    }

    public  void CusceneEnding()
    {
        coreEventManager.startCutsceneEnded.Invoke();
    }
}
