using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffectChanger : MonoBehaviour
{
    public CoreEventManager coreEventManager;
    public World_Manager world_Manager;
    public GameObject[] trails;
    public ParticleSystem[] trailsPS;
    public Color[] trailColors;


    void Awake()
    {
        coreEventManager = GameObject.Find("Managers").GetComponent<CoreEventManager>();
        world_Manager = GameObject.Find("Managers").GetComponent<World_Manager>();
        coreEventManager.OutPortal.AddListener(RefreshVisuals);
        coreEventManager.InPortal.AddListener(HideTrais);
    }

    
    public void RefreshVisuals()
    {
        int i = 0;
        foreach (GameObject tr in trails)
        {
            tr.SetActive(true);
            ParticleSystem.MainModule main = trailsPS[i].main;
            i++;
            main.startColor = trailColors[world_Manager.CurrentWorldID];
        }
    }

    public void HideTrais()
    {
        foreach (GameObject tr in trails)
        {
            tr.SetActive(false);
        }
    }
}
