using System;
using _HexaMerge.Scripts.DynamicFeedback;
using UnityEngine;

[CreateAssetMenu(fileName = "DynamicFeedback", menuName = "Utilities/DynamicFeedback")]
public class DynamicFeedbackSO : ScriptableObject
{
    public Action<int> PlayAudio;
    public Action<int> PlayHaptics;

    public void PlayAudioSource(DynamicAudio audio)
    {
        int id = (int) audio;
        PlayAudio?.Invoke(id);
    }
    
    public void PlayHapticsSource(DynamicHaptics haptics)
    {
        int id = (int) haptics;
        PlayHaptics?.Invoke(id);
    }

    public void PlayAudioSourceByID(int id)
    {
        PlayAudio?.Invoke(id);
    }
}
