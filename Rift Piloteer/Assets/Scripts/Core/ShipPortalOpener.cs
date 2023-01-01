using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPortalOpener : MonoBehaviour
{
    public ParticleSystem portalLightning;
    public Animator portalAnim;
    public CoreEventManager coreEventManager;


    public void Awake()
    {
        coreEventManager = GameObject.Find("Managers").GetComponent<CoreEventManager>();
        coreEventManager.OpenPortal.AddListener(LaunchPortal);
        coreEventManager.InPortal.AddListener(HidePortal);
    }

    public void LaunchPortal()
    {
        portalAnim.Play("Show");
        portalLightning.Play();
    }
    public void HidePortal()
    {
        portalAnim.Play("Off");
    }
}
