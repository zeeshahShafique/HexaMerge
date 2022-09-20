using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/LivesSystem", order = 1)]
public class LivesSystem : ScriptableObject
{
    [SerializeField] private int LivesAmount = 3;
    [SerializeField] private string LivesPrefKey;
    
    [SerializeField] private int MaxLives;
    [SerializeField] private int MaxTimer; // In seconds
    public int Timer;
    private bool _isTimerActive = false;

    [SerializeField] private int _idleStartTime;
    [SerializeField] private int _currentTime;

    public Action<int> ChangeLivesText;
    public Action<int> ChangeTimerText;
    public Action SetFullText;

    [SerializeField] private AdSystem AdSystem;
    [SerializeField] private Coins CoinSystem;

    public void InitLivesSystem()
    {
        if (PlayerPrefs.HasKey(LivesPrefKey))
            LivesAmount = PlayerPrefs.GetInt(LivesPrefKey);
        if (PlayerPrefs.HasKey("IdleStartTime"))
            _idleStartTime = PlayerPrefs.GetInt("IdleStartTime");
        IdleLivesRestoration();
    }

    private void OnEnable()
    {
        InitLivesSystem();
    }

    private void OnDisable()
    {
        SaveLivesPref();
    }

    public void OnTickEvent()
    {
        if (!_isTimerActive) return;
        if (Timer > 0 && !IsFull())
        {
            ChangeTimerText?.Invoke(Timer);
            Timer--;
        }
        else
        {
            TimerComplete();
        }
    }
    
    private void TimerComplete()
    {
        // Reset Timer.
        _isTimerActive = false;
        Timer = MaxTimer;

        if (!IsFull())
        {
            // Add a life in the lives system.
            AddLives(1);
            
            // Restart timer if Lives are not full.
            StartTimer(MaxTimer);
            return;
        }
        SetFullText?.Invoke();
    }

    public void SaveLivesPref()
    {
        PlayerPrefs.SetInt(LivesPrefKey, LivesAmount);
        _idleStartTime = GetEpochTime();
        PlayerPrefs.SetInt("IdleStartTime", _idleStartTime);
        PlayerPrefs.Save();
    }
    private int GetEpochTime()
    {
        var time = DateTime.Now - new DateTime(1970, 1, 1);
        return (int)time.TotalSeconds;
    }
    private void IdleLivesRestoration()
    {
        _currentTime = GetEpochTime();
        var time = _currentTime - _idleStartTime;
        time += MaxTimer - Timer;
        while (time > MaxTimer && !IsFull())
        {
            AddLives(1);
            time -= MaxTimer;
            StartTimer(time);
            return;
        }

        if (IsFull())
        {
            _isTimerActive = false;
            return;
        }
        StartTimer(MaxTimer - time);
    }
    private void StartTimer(int time)
    {
        Timer = time;
        _isTimerActive = true;
    }

    public void AddLives(int amount)
    {
        if (IsFull()) return;
        LivesAmount += amount;
        Timer = MaxTimer;
        SaveLivesPref();
        ChangeLivesText?.Invoke(LivesAmount);
    }
    
    public void ReduceLives(int amount)
    {
        if (!HasLives()) return;
        LivesAmount -= amount;
        StartTimer(Timer);
        SaveLivesPref();
        ChangeLivesText?.Invoke(LivesAmount);
    }

    public void BuyEnergy()
    {
        if (CoinSystem.GetCoins() > 200)
        {
            CoinSystem.RemoveCoins(200);
            RefillEnergy();
        }
    }
    
    public void AddRewardAdLives()
    {
        AdSystem.ShowRewardedAd(RefillEnergy);
    }

    private void RefillEnergy()
    {
        var energy = MaxLives - LivesAmount;
        AddLives(energy);
    }

    public bool HasLives()
    {
        return LivesAmount > 0;
    }
    public int GetLives()
    {
        return LivesAmount;
    }

    public int GetMaxLives()
    {
        return MaxLives;
    }

    public int GetMaxTimer()
    {
        return MaxTimer;
    }
    public bool IsFull()
    {
        return LivesAmount == MaxLives;
    }
    
}
