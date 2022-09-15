using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AdSystem", order = 1, fileName = "AdSystem")]
public class AdSystem : ScriptableObject
{
    public List<float> ADTimerDelay = new List<float>(){30, 30, 30};

    public int AdTimerIndex = 0;
    
    [SerializeField] private float _lastLoadedTime;
    
    [SerializeField] private DynamicOverlaySO AdOverlay;

    public delegate void InterRemoved();
    public static event InterRemoved OnInterRemoved;

    
    public bool RemoveRVIAP = false;

    public bool RemoveInterIAP = false;
    
    private const string MaxKey = "hlKffQFn1sKXRefAUUKG4o-i-OOURETonfImCKvE29oyDwftIiyhVZMlNNxwUFl8NgUmynX33XOEq5m09yb34Z";
    private const string RewardedAdUnit = "585f249ad115c420";
    private const string InterstitialAdUnit = "7d62e5180461f57a";
    private const string BannerAdUnit = "b56d58800dadb2d1";

    private Action _onRewardReceived;

    private void OnDisable()
    {
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent -= OnRewardedAdReceivedRewardEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent -= OnRewardedAdClosedEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent -= OnInterstitialsAdClosedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent -= OnRewardedAdDisplayFailed;
        MaxSdkCallbacks.OnSdkInitializedEvent -= OnMaxInitialized;
        RemoveInterIAP = false;
        AdTimerIndex = 0;
    }

    public void AdSystemInit()
    {
        string[] adUnitIds = {
            // rewarded
            RewardedAdUnit,
            // interstitial
            InterstitialAdUnit,
            // banner
            BannerAdUnit
        };

        MaxSdk.SetSdkKey(MaxKey);
        MaxSdk.SetUserId(SystemInfo.deviceUniqueIdentifier);
        MaxSdk.SetVerboseLogging(true);
        MaxSdkCallbacks.OnSdkInitializedEvent += OnMaxInitialized;
        MaxSdk.InitializeSdk(adUnitIds);
        
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdClosedEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialsAdClosedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdDisplayFailed;


        LoadRewardedAd();
        LoadInterstitialsAd();
        _lastLoadedTime = 0;
    }
    
    private void OnMaxInitialized(MaxSdkBase.SdkConfiguration sdkConfiguration)
    {
        if (MaxSdk.IsInitialized()) {
            Debug.Log("MaxSDK initialized");
        } else {
            // AdOverlay.EnableOverlay("Weak Internet Connection");
            Debug.Log("Failed to init MaxSDK");
        }
    }

    public bool IsAdSystemInitialized()
    {
        return MaxSdk.IsInitialized();
    }

    #region Interstitials Ad
    
    private void OnInterstitialsAdClosedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        _lastLoadedTime = Time.time;
        LoadInterstitialsAd();
    }

    private void LoadInterstitialsAd()
    {
        MaxSdk.LoadInterstitial(InterstitialAdUnit);
    }
    public void ShowInterstitialsAd()
    {
        var index = AdTimerIndex;
        if ((Time.time - _lastLoadedTime) > ADTimerDelay[index] && MaxSdk.IsInterstitialReady(InterstitialAdUnit) && !RemoveInterIAP)
        {
            AdTimerIndex = AdTimerIndex < ADTimerDelay.Count-1 ? AdTimerIndex+1 : AdTimerIndex;
            MaxSdk.ShowInterstitial(InterstitialAdUnit);
        }
    }

    #endregion
   

    #region Rewarded Ad

    private void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(RewardedAdUnit);
    }
    public void ShowRVAd()
    {
        ShowRewardedAd(null);
    }
    public void ShowRewardedAd(Action callback)
    {
        _onRewardReceived = callback;
        if (MaxSdk.IsRewardedAdReady(RewardedAdUnit) && !RemoveRVIAP) // && (Time.time - _lastLoadedTime) > ADTimerDelay)
        {
            MaxSdk.ShowRewardedAd(RewardedAdUnit);
            return;
        }

        if (!RemoveRVIAP) return;
        _onRewardReceived?.Invoke();
    }
    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        _onRewardReceived?.Invoke();
        _onRewardReceived = null;
    }
    
    private void OnRewardedAdClosedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        _lastLoadedTime = Time.time;
        _onRewardReceived = null;
        LoadRewardedAd();
    }

    private void OnRewardedAdDisplayFailed(string adUnitId, MaxSdk.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        Debug.LogError($"[ERROR] [AD DISPLAY FAILED] {errorInfo.Message}");
        _onRewardReceived = null;
    }
    

    #endregion

    #region IAP
    public void RemoveRV()
    {
        RemoveRVIAP = true;
    }

    public void RemoveInter()
    {
        RemoveInterIAP = true;
        OnInterRemoved?.Invoke();
    }
    
    public void ClearIAP()
    {
        RemoveInterIAP = false;
        RemoveRVIAP = false;
    }

    public void ResetInterTimer()
    {
        _lastLoadedTime = Time.time;
    }
    #endregion

}
