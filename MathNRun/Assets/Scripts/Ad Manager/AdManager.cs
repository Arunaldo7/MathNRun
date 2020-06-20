using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;

    // private GamePanelController gamePanelController;

    private const string appId = "";
    private InterstitialAd fullScrAd;
    private const string fullScrAdId = "ca-app-pub-3940256099942544/1033173712";

    private RewardBasedVideoAd rewardAd;

    private const string rewardAdId = "ca-app-pub-3940256099942544/5224354917";

    private string rewardType;

    private const string extrLifeReward = "EXTRA LIFE";
    private const string lifePotionReward = "LIFE POTION";
    private const string magicPotionReward = "MAGIC POTION";

    private const string coinPurchaseReward = "BUY COINS";

    // Create Singleton Pattern
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        RequestFullScrAd();
        rewardAd = RewardBasedVideoAd.Instance;
        RequestRewardAd();

        rewardAd.OnAdRewarded += HandleRewardBasedVideoRewarded;

        rewardAd.OnAdClosed += HandleRewardBasedVideoClosed;
    }

    public void RequestFullScrAd()
    {
        fullScrAd = new InterstitialAd(fullScrAdId);

        AdRequest adRequest = new AdRequest.Builder().Build();

        fullScrAd.LoadAd(adRequest);
    }

    public void ShowFullScrAd()
    {
        if (fullScrAd.IsLoaded())
        {
            fullScrAd.Show();
        }
        else
        {
            Debug.Log("Full Screen Ad Not Loaded");
        }
    }

    public void RequestRewardAd()
    {
        AdRequest adRequest = new AdRequest.Builder().Build();

        rewardAd.LoadAd(adRequest, rewardAdId);
    }

    public void ShowLifeRewardAd()
    {
        if (rewardAd.IsLoaded())
        {
            rewardType = extrLifeReward;
            rewardAd.Show();
        }
        else
        {
            Debug.Log("Reward Ad Not Loaded");
        }
    }

    public void ShowLifePotionRewardAd()
    {
        if (rewardAd.IsLoaded())
        {
            rewardType = lifePotionReward;
            rewardAd.Show();
        }
        else
        {
            Debug.Log("Reward Ad Not Loaded");
        }
    }

    public void ShowMagicPotionRewardAd()
    {
        if (rewardAd.IsLoaded())
        {
            rewardType = magicPotionReward;
            rewardAd.Show();
        }
        else
        {
            Debug.Log("Reward Ad Not Loaded");
        }
    }

    public void ShowCoinPurchaseRewardAd()
    {
        if (rewardAd.IsLoaded())
        {
            rewardType = coinPurchaseReward;
            rewardAd.Show();
        }
        else
        {
            Debug.Log("Reward Ad Not Loaded");
        }
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward reward)
    {
        string type = reward.Type;
        double amount = reward.Amount;

        Debug.Log("You have been awarded with " + amount.ToString() + " " + type);
        if (rewardType == extrLifeReward)
        {
            DestroyNearbyObjects();
            GamePanelController.instance.ResumeGame();
        }
        else if (rewardType == lifePotionReward)
        {
            ShopController.instance.PurchasePotionFreeItem();
        }
        else if (rewardType == magicPotionReward)
        {
            ShopController.instance.PurchaseMagicPotionFreeItem();
        }
        else
        {
            GameStateManager.instance.totalCoins = GameStateManager.instance.totalCoins + 1000;

            GameStateManager.instance.SaveData();
            MainmenuController.instance.DisplayGameState();
        }
    }

    void DestroyNearbyObjects()
    {
        GameplayController.instance.DestroyNearObjects();
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        Debug.Log("Reward Video Closed");
        RequestRewardAd();
    }
}
