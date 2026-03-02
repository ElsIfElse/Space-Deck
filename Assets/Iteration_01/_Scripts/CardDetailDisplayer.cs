using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardDetailDisplayer : MonoBehaviour
{
    #region Simpleton Init
    public static CardDetailDisplayer Instance;
    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }
    #endregion
    public TextMeshPro CardEffectDescriptionText_01;
    public TextMeshPro CardEffectDescriptionText_02;
    public TextMeshPro CardNameText;
    public TextMeshPro CardValueText;
    public TextMeshPro CardManaCostText;
    Tween _currentTween;

    public GameObject DetailPanelObj;

    public void SetDisplayText(Card data)
    {
        CardNameText.text = $"<b>{data.CardName}</b>";
        CardManaCostText.text = $"<b>Cost</b>: {data.ManaCost}";
        CardValueText.text = $"<b>Value</b>: {data.CardValue}";

        CardEffectDescriptionText_01.text = (data.CardEffectDescription_01 == null || data.CardEffectDescription_01 == "") ? "" : $"<b>Primary Effect</b>: {data.CardEffectDescription_01}";
        CardEffectDescriptionText_02.text = (data.CardEffectDescription_02 == null || data.CardEffectDescription_02 == "") ? "" : $"<b>Secondary Effect</b>: {data.CardEffectDescription_02}";
    }

    public void ShowDetailDisplay(Transform targetTransform, Card data)
    {
        _currentTween?.Kill();
        SetDisplayText(data);
        DetailPanelObj.transform.position = new Vector3(targetTransform.position.x,0,targetTransform.position.z);
        _currentTween = DetailPanelObj.transform.DOScale(1,0.3f).SetEase(Ease.InOutBounce);
    }

    public void HideDetailDisplay()
    {
        _currentTween?.Kill();
        // DetailPanelObj.SetActive(false);
        _currentTween = DetailPanelObj.transform.DOScale(0,0.3f).SetEase(Ease.InOutSine);
    }
}