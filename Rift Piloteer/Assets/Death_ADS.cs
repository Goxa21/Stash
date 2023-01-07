using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_ADS : MonoBehaviour
{
    public int MaxDeathCounter;
    int DeathCounter;
    CoreEventManager coreEventManager;
    [SerializeField] private ADSManager aDSManager;
    [SerializeField] private Ya_ADS_Manager NewAdsManager;
    bool CanShow;

    private void Start()
    {
        NewAdsManager.ShowIntersitial();
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
            //aDSManager.LaunchRewardedADS();
            NewAdsManager.ShowRewarded();
            DeathCounter = 0;
            CanShow = false;
        }
    }
    void CanShowADS()
    {
        CanShow = true;
    }
}
