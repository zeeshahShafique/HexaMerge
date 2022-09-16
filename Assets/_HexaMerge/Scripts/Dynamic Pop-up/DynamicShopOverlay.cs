using System;
using UnityEngine;
using UnityEngine.UI;

public class DynamicShopOverlay : MonoBehaviour
{
    [SerializeField] private Image OverlayImage;

    [SerializeField] private DynamicOverlaySO DynamicOverlay;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        DynamicOverlay.EnableShopOverlay += EnableOverlay;
        DynamicOverlay.DisableShopOverlay += DisableOverlay;
    }

    private void OnDisable()
    {
        DynamicOverlay.EnableShopOverlay -= EnableOverlay;
        DynamicOverlay.DisableShopOverlay -= DisableOverlay;
    }

    private void EnableOverlay()
    {
        OverlayImage.gameObject.SetActive(true);
    }

    private void DisableOverlay()
    {
        OverlayImage.gameObject.SetActive(false);
    }
}
