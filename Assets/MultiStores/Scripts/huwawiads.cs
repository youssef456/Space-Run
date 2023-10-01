using HmsPlugin;
using HuaweiMobileServices.Ads;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class huwawiads : MonoBehaviour
{
    public multiadmanager multiadmanager;
    [HideInInspector]
    public string REWARD_AD_ID = "testx9dtjwj8hp";
    private RewardAdManager rewardAdManager;

    public void InitRewardedAds()
    {
        rewardAdManager = RewardAdManager.GetInstance();
        rewardAdManager.AdId = REWARD_AD_ID;
        rewardAdManager.OnRewarded = OnRewarded;
        rewardAdManager.OnRewardAdFailedToShow = OnRewardAdFailedToShow;
    }
    public void ShowRewardedAd()
    {
        Debug.Log("[HMS] AdsDemoManager ShowRewardedAd");
        rewardAdManager.ShowRewardedAd();
    }
    public void OnRewarded(Reward reward)
    {
        multiadmanager.adreward();
    }
    public void OnRewardAdFailedToShow(int i)
    {
        multiadmanager.adskipped();
    }
}
