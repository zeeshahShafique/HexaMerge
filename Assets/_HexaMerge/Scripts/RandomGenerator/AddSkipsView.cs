using System;
using System.Collections;
using System.Collections.Generic;
using _HexaMerge.Scripts.DynamicFeedback;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AddSkipsView : MonoBehaviour
{
    [SerializeField] private Transform TouchSystemTransform;

    [SerializeField] private Transform GridSystemTransform;

    [SerializeField] private Transform ButtonsTransform;
    
    [SerializeField] private Transform TileControllerTransform;

    [SerializeField] private Button CloseButton;

    [SerializeField] private DynamicFeedbackSO DynamicFeedback;

    private Vector3 _touchSystemCachePos;
    
    // Start is called before the first frame update

    private void OnEnable()
    {
        _touchSystemCachePos = TouchSystemTransform.localPosition;
        CloseButton.onClick.AddListener(OnCLoseButtonCalled);
        var seq = DOTween.Sequence();
        seq.Append(TouchSystemTransform.DOLocalMove(Vector3.left * 100, 0.5f).SetEase(Ease.InOutCirc));
        seq.Append(GridSystemTransform.DOLocalMove(Vector3.left * 100, 0.5f).SetEase(Ease.InOutCirc));
        seq.Append(ButtonsTransform.DOLocalMove(Vector3.left * 3000, 0.5f).SetEase(Ease.InOutCirc));
        seq.Append(TileControllerTransform.DOLocalMove(Vector3.left * 3000, 0.5f).SetEase(Ease.InOutCirc));
        seq.Play().OnComplete(() =>
        {
            transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InOutBounce);
        });
    }

    private void OnDisable()
    {
        CloseButton.onClick.RemoveListener(OnCLoseButtonCalled);
    }

    private void OnCLoseButtonCalled()
    {
        DynamicFeedback.PlayAudioSource(DynamicAudio.ButtonClick);
        DynamicFeedback.PlayHapticsSource(DynamicHaptics.SoftImpact);
        CloseButton.transform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 5, 0.75f);
        var seq = DOTween.Sequence();
        seq.Append(GridSystemTransform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InOutCirc));
        seq.Append(ButtonsTransform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InOutCirc));
        seq.Append(TouchSystemTransform.DOLocalMove(_touchSystemCachePos, 0.5f).SetEase(Ease.InOutCirc));
        seq.Append(TileControllerTransform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InOutCirc));
        transform.DOLocalMove(Vector3.right * 1125, 0.5f).SetEase(Ease.InOutBounce).OnComplete(() =>
        {
            seq.Play().OnComplete(()=> this.gameObject.SetActive(false));
        });
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
