using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AdSystem", order = 1, fileName = "AdSystem")]
public class AdSystem : ScriptableObject
{
    public float ADTimerDelay = 30;
    [SerializeField] private float _lastLoadedTime;

    public bool RewardReceived;

    public bool RemoveRVIAP = false;

    public bool RemoveInterIAP = false;
    
    private const string MaxKey = "hlKffQFn1sKXRefAUUKG4o-i-OOURETonfImCKvE29oyDwftIiyhVZMlNNxwUFl8NgUmynX33XOEq5m09yb34Z";
    private const string RewardedAdUnit = "585f249ad115c420";
    private const string InterstitialAdUnit = "7d62e5180461f57a";
    private const string BannerAdUnit = "b56d58800dadb2d1";
    
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
        
        LoadRewardedAd();
        LoadInterstitialsAd();
        _lastLoadedTime = Mathf.NegativeInfinity;
    }
    private void OnRewardedAdClosedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        RewardReceived = true;
        _lastLoadedTime = Time.time;
        LoadRewardedAd();
    }
    private void OnInterstitialsAdClosedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        _lastLoadedTime = Time.time;
        LoadInterstitialsAd();
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
    public void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(RewardedAdUnit);
    }
    public bool IsAdSystemInitialized()
    {
        return MaxSdk.IsInitialized();
    }
    public void LoadInterstitialsAd()
    {
        MaxSdk.LoadInterstitial(InterstitialAdUnit);
    }

    public void ShowRVAd()
    {
        ShowRewardedAd();
    }
    public bool ShowRewardedAd()
    {
        if (MaxSdk.IsRewardedAdReady(RewardedAdUnit) && !RemoveRVIAP) // && (Time.time - _lastLoadedTime) > ADTimerDelay)
        {
            RewardReceived = false;
            MaxSdk.ShowRewardedAd(RewardedAdUnit);
            return true;
        }

        if (!RemoveRVIAP) return false;
        RewardReceived = true;
        return true;
    }
    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        RewardReceived = true;
        _lastLoadedTime = Time.time;
    }
    public void ShowInterstitialsAd()
    {
        if ((Time.time - _lastLoadedTime) > ADTimerDelay && MaxSdk.IsInterstitialReady(InterstitialAdUnit) && !RemoveInterIAP)
        {
            MaxSdk.ShowInterstitial(InterstitialAdUnit);
        }
    }

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
}
