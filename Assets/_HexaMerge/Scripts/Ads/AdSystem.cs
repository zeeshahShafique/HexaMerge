using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AdSystem", order = 1, fileName = "AdSystem")]
public class AdSystem : ScriptableObject
{
    public float ADTimerDelay = 30;
    [SerializeField] private float _lastLoadedTime;
    
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
        _lastLoadedTime = Mathf.NegativeInfinity;
    }
    
    private void OnMaxInitialized(MaxSdkBase.SdkConfiguration sdkConfiguration)
    {
        if (MaxSdk.IsInitialized()) {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            MaxSdk.ShowMediationDebugger();
#endif
            Debug.Log("MaxSDK initialized");
        } else {
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
        if ((Time.time - _lastLoadedTime) > ADTimerDelay && MaxSdk.IsInterstitialReady(InterstitialAdUnit) && !RemoveInterIAP)
        {
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
    }
    
    public void ClearIAP()
    {
        RemoveInterIAP = false;
        RemoveRVIAP = false;
    }
    #endregion

}
