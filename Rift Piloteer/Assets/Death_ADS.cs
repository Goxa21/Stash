using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_ADS : MonoBehaviour
{
    public int MaxDeathCounter;
    int DeathCounter;
    CoreEventManager coreEventManager;
    public ADSManager aDSManager;
    bool CanShow;

    private void Start()
    {
        coreEventManager = GetComponent<CoreEventManager>();
        coreEventManager.shipDestroyed.AddListener(CheckDeath);
        coreEventManager.gameStart.AddListener(CanShowADS);
    }
    void CheckDeath()
    {
        if (DeathCounter < MaxDeathCounter)
            DeathCounter++;
        else if(CanShow)
        {
            aDSManager.LaunchRewardedADS();
            DeathCounter = 0;
            CanShow = false;
        }
    }
    void CanShowADS()
    {
        CanShow = true;
    }
}
