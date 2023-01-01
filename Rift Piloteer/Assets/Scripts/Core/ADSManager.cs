using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
[DefaultExecutionOrder(0)]
public class ADSManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsListener
{
    public Progress_Manager progress_manager;
    public bool isinExtraMode;
    public bool testMode;
    private string gameID = "4572889";
    private string simpleVideo = "Interstitial_Android";
    private string rewardedVideo = "Rewarded_Android";
    public bool doubleVowFlag;


    public void Awake()
    {
        GameObject[] slm = GameObject.FindGameObjectsWithTag("ADSManager");
        progress_manager = GameObject.Find("Managers").GetComponent<Progress_Manager>();
        if (slm.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Advertisement.AddListener(this);
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(gameID, testMode);
            Debug.Log("ADS Initialized");
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void LaunchFreeADS()
    {
        LaunchADS(simpleVideo);
    }

    public void LaunchRewardedADS()
    {
        LaunchADS(rewardedVideo);
    }

    public void LaunchADS(string adsName)
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show(adsName);
        }
        else
        {
            Debug.Log("ADS not ready");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == rewardedVideo)
        {
            Debug.Log("OMG, YOU READY TO WATCH REWARDED ADS");
            
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            Debug.Log("ADS finished");
            if (placementId == rewardedVideo)
            {
                //reward
                if (doubleVowFlag)
                {
                    progress_manager.DoubleReward();
                }
            }
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("Nooo, you skipped it :<");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.Log("Whoops, there is no ADS available");
        }
        
    }
}

    
