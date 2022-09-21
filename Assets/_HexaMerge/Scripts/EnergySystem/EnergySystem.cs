using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/LivesSystem", order = 1)]
public class EnergySystem : ScriptableObject
{
    [SerializeField] private int EnergyAmount = 3;
    [SerializeField] private string EnergyPrefKey;
    
    [SerializeField] private int MaxEnergy;
    [SerializeField] private int MaxTimer; // In seconds
    public int Timer;
    private bool _isTimerActive = false;

    [SerializeField] private int IdleStartTime;
    [SerializeField] private int CurrentTime;

    public Action<int> ChangeLivesText;
    public Action<int> ChangeTimerText;
    public Action SetFullText;

    [SerializeField] private AdSystem AdSystem;
    [SerializeField] private Coins CoinSystem;

    public void InitLivesSystem()
    {
        if (PlayerPrefs.HasKey(EnergyPrefKey))
            EnergyAmount = PlayerPrefs.GetInt(EnergyPrefKey);
        if (PlayerPrefs.HasKey("IdleStartTime"))
            IdleStartTime = PlayerPrefs.GetInt("IdleStartTime");
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
        PlayerPrefs.SetInt(EnergyPrefKey, EnergyAmount);
        IdleStartTime = GetEpochTime();
        PlayerPrefs.SetInt("IdleStartTime", IdleStartTime);
        PlayerPrefs.SetInt("EnergyTimer", Timer);
        PlayerPrefs.Save();
    }
    private int GetEpochTime()
    {
        var time = DateTime.Now - new DateTime(1970, 1, 1);
        return (int)time.TotalSeconds;
    }
    private void IdleLivesRestoration()
    {
        CurrentTime = GetEpochTime();
        Timer = PlayerPrefs.GetInt("EnergyTimer");
        var time = CurrentTime - IdleStartTime;
        Debug.LogError($"Time Before Addition: {time}");
        Debug.LogError($"MaxTimer: {MaxTimer} ; Timer: {Timer}");
        time += MaxTimer - Timer;
        Debug.LogError($"Time After Addition: {time}");
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
            Timer = MaxTimer;
            return;
        }
        StartTimer(Timer - time);
    }
    private void StartTimer(int time)
    {
        Timer = time;
        _isTimerActive = true;
    }

    public void AddLives(int amount)
    {
        if (IsFull()) return;
        EnergyAmount += amount;
        Timer = MaxTimer;
        SaveLivesPref();
        ChangeLivesText?.Invoke(EnergyAmount);
    }
    
    public void ReduceLives(int amount)
    {
        if (!HasLives()) return;
        EnergyAmount -= amount;
        StartTimer(Timer);
        SaveLivesPref();
        ChangeLivesText?.Invoke(EnergyAmount);
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
        var energy = MaxEnergy - EnergyAmount;
        AddLives(energy);
    }

    public bool HasLives()
    {
        return EnergyAmount > 0;
    }
    public int GetLives()
    {
        return EnergyAmount;
    }

    public int GetMaxLives()
    {
        return MaxEnergy;
    }

    public int GetMaxTimer()
    {
        return MaxTimer;
    }
    public bool IsFull()
    {
        return EnergyAmount == MaxEnergy;
    }
    
}
