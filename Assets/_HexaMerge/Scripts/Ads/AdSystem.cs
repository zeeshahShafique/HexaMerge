using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AdSystem", order = 1, fileName = "AdSystem")]
public class AdSystem : ScriptableObject
{
    [SerializeField] private List<float> AdTimerDelay;

    [SerializeField] private int AdTimerIndex = 0;
    
    [SerializeField] private float LastLoadedTime;
    
    [SerializeField] private DynamicOverlaySO AdOverlay;

    // public delegate void InterRemoved();
    // public static event InterRemoved OnInterRemoved;

    
    public bool RemoveRViAP = false;

    public int RemoveInterIAP = 0;
    
    private const string _maxKey = "hlKffQFn1sKXRefAUUKG4o-i-OOURETonfImCKvE29oyDwftIiyhVZMlNNxwUFl8NgUmynX33XOEq5m09yb34Z";
    private const string _rewardedAdUnit = "585f249ad115c420";
    private const string _interstitialAdUnit = "7d62e5180461f57a";
    private const string _bannerAdUnit = "b56d58800dadb2d1";

    private Action _onRewardReceived;

    private void OnDisable()
    {
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent -= OnRewardedAdReceivedRewardEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent -= OnRewardedAdClosedEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent -= OnInterstitialsAdClosedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent -= OnRewardedAdDisplayFailed;
        MaxSdkCallbacks.OnSdkInitializedEvent -= OnMaxInitialized;
        RemoveInterIAP = PlayerPrefs.GetInt("RemoveAds");
        AdTimerIndex = 0;
    }

    public void AdSystemInit()
    {
        string[] adUnitIds = {
            // rewarded
            _rewardedAdUnit,
            // interstitial
            _interstitialAdUnit,
            // banner
            _bannerAdUnit
        };

        MaxSdk.SetSdkKey(_maxKey);
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
        LastLoadedTime = 0;
    }
    
    private void OnMaxInitialized(MaxSdkBase.SdkConfiguration sdkConfiguration)
    {
        if (MaxSdk.IsInitialized()) 
        {
            Debug.Log("MaxSDK initialized");
        } 
        else 
        {
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
        LastLoadedTime = Time.time;
        LoadInterstitialsAd();
    }

    private void LoadInterstitialsAd()
    {
        MaxSdk.LoadInterstitial(_interstitialAdUnit);
    }
    public void ShowInterstitialsAd()
    {
        // AdOverlay.EnableOverlayButton($"Remove Ads Value: {RemoveInterIAP}");
        var index = AdTimerIndex;
        if (Time.time - LastLoadedTime > AdTimerDelay[index] && MaxSdk.IsInterstitialReady(_interstitialAdUnit) && RemoveInterIAP != 1)
        {
            AdTimerIndex = AdTimerIndex < AdTimerDelay.Count-1 ? AdTimerIndex+1 : AdTimerIndex;
            MaxSdk.ShowInterstitial(_interstitialAdUnit);
        }
    }

    #endregion
   

    #region Rewarded Ad

    private void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(_rewardedAdUnit);
    }
    public void ShowRVAd()
    {
        ShowRewardedAd(null);
    }
    public void ShowRewardedAd(Action callback)
    {
        _onRewardReceived = callback;
        if (MaxSdk.IsRewardedAdReady(_rewardedAdUnit) && !RemoveRViAP) // && (Time.time - _lastLoadedTime) > ADTimerDelay)
        {
            MaxSdk.ShowRewardedAd(_rewardedAdUnit);
            return;
        }

        if (!RemoveRViAP) return;
        _onRewardReceived?.Invoke();
    }
    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        _onRewardReceived?.Invoke();
        _onRewardReceived = null;
    }
    
    private void OnRewardedAdClosedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        LastLoadedTime = Time.time;
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
        RemoveRViAP = true;
    }

    public void RemoveInter()
    {
        RemoveInterIAP = 1;
        // OnInterRemoved?.Invoke();
    }
    
    public void ClearIAP()
    {
        RemoveInterIAP = 0;
        RemoveRViAP = false;
        PlayerPrefs.SetInt("RemoveAds", 0);
        PlayerPrefs.Save();
    }

    public void ResetInterTimer()
    {
        LastLoadedTime = Time.time;
    }
    #endregion

}
