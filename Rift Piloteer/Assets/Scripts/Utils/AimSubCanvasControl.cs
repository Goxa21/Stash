using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSubCanvasControl : MonoBehaviour
{
    public CoreEventManager coreEventManager;
    public Animator anim;
    bool listenForAbility;

    void Awake()
    {
        listenForAbility = true;
        coreEventManager = GameObject.Find("Managers").GetComponent<CoreEventManager>();
        coreEventManager.abilityShot.AddListener(ShotShow);
        coreEventManager.abilityDeactivation.AddListener(EndShow);
    }

    
    public void ShotShow()
    {
        if (listenForAbility)
        {
            anim.Play("Shot");

        }
    }

    public void EndShow()
    {
        anim.Play("Outro");
        StartCoroutine(DestructionFunction());
        listenForAbility = false;
    }

    IEnumerator DestructionFunction()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
