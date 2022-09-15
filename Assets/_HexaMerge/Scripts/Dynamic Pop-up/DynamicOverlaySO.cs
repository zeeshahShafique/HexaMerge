using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DynamicOverlay", menuName = "Utilities/DynamicOverlay")]
public class DynamicOverlaySO : ScriptableObject
{
    [SerializeField] private string OverlayText;
    public Action<string> EnableOverlayButton;
    public Action EnableShopOverlay;
    public Action DisableShopOverlay;

    [ContextMenu("Enable Overlay")]
    public void EnableClickableOverlay(string text)
    {
        OverlayText = text;
        EnableOverlayButton?.Invoke(OverlayText);
    }

    public void EnableShopOverlayCanvas()
    {
        EnableShopOverlay?.Invoke();
    }

    public void DisableShopOverlayCanvas()
    {
        DisableShopOverlay?.Invoke();   
    }
}
