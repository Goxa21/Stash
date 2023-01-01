using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public GameObject abilityButton;
    public CoreEventManager coreEventManager;
    public Image abilityChargeIcon;
    public Animator abilityCharge;
    public Animator abilitySpin;
    public float chargeBit;
    public float curCharge;
    public bool canCharge;



    public void Awake()
    {
        coreEventManager.gameStart.AddListener(DeactivateButton);
        coreEventManager.abilityDeactivation.AddListener(RestoreCharge);
    }
    public void IncreaseCharge()
    {
        if (canCharge)
        {
            curCharge += chargeBit;
            abilityChargeIcon.fillAmount = curCharge;
        }
        abilitySpin.Play("Spin");
        if (curCharge >= 1)
        {
            ActivateButton();
        }
        
    }

    public void ActivateButton()
    {
        abilityButton.SetActive(true);
        abilityCharge.Play("Charged");

    }

    public void DeactivateButton()
    {
        abilityButton.SetActive(false);
        curCharge = 0;
        abilityChargeIcon.fillAmount = curCharge;
        abilityCharge.Play("NotCharged");
    }


    public void ActivateAbility()
    {
        coreEventManager.abilityActivation.Invoke();
        DeactivateButton();
        canCharge = false;
    }

    public void RestoreCharge()
    {
        canCharge = true;
    }

    
}
