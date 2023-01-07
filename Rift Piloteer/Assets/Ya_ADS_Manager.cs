using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ya_ADS_Manager : MonoBehaviour
{
    [SerializeField] private Progress_Manager progress_manager;
    [SerializeField] private string reward;

    private void OnEnable()
    {
        YandexSDK.YaSDK.onRewardedAdReward += UserGotReward;
    }
    private void OnDisable()
    {
        YandexSDK.YaSDK.onRewardedAdReward -= UserGotReward;
    }
    public void ShowRewarded()
    {
        YandexSDK.YaSDK.instance.ShowRewarded(reward);
    }
    public void ShowIntersitial()
    {
        YandexSDK.YaSDK.instance.ShowInterstitial();
        print("Show Interstitial");
    }
    public void UserGotReward(string reward)
    {
        if (this.reward == reward)
        {
            //Награда пользователя
            progress_manager.DoubleReward();
            print("Got reward");
        }
    }
}
