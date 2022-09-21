using System;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class DynamicFeedback : MonoBehaviour
{
    [SerializeField] private List<AudioSource> AudioSources;

    [SerializeField] private List<HapticTypes> Haptics;

    [SerializeField] private DynamicFeedbackSO DyFeedback;


    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        DyFeedback.PlayAudio += PlayAudio;
        DyFeedback.PlayHaptics += PlayHaptics;
    }

    private void OnDisable()
    {
        DyFeedback.PlayAudio -= PlayAudio;
        DyFeedback.PlayHaptics -= PlayHaptics;
    }

    private void PlayAudio(int id)
    {
        AudioSources[id].Play();
    }

    private void PlayHaptics(int id)
    {
        MMVibrationManager.Haptic(Haptics[id]);
    }

}
