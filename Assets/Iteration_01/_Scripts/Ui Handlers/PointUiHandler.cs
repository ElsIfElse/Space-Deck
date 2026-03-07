using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointUiHandler : IUiHandler
{
    private TextMeshProUGUI _pointCounterText;
    private Image _fillRepresenter;
    Image _fillRepresenterBg;
    TextMeshProUGUI _pointGainIndicatorText;
    Transform _pointGainIndicatorTextOriginalTransform;
    Vector3 _pointGainIndicatorTextOriginalPosition;

    Tween _pointGainIndicatorTextTween_01;
    Tween _pointGainIndicatorTextTween_02;

    public PointUiHandler(PointUiHandlerData data)
    {
        _pointCounterText = data.PointCounterText;
        _fillRepresenter = data.FillRepresenter;
        _fillRepresenterBg = data.FillRepresenterBg;
        _pointGainIndicatorText = data.PointGainIndicatorText;

        _pointGainIndicatorTextOriginalTransform = _pointGainIndicatorText.transform;
        _pointGainIndicatorTextOriginalPosition = _pointGainIndicatorText.GetComponent<RectTransform>().anchoredPosition;
    }

    public void SetState(bool state)
    {
        if (state)
        {
            SetGainPointText("");

            _pointCounterText.gameObject.SetActive(true);
            _fillRepresenter.gameObject.SetActive(true);
            _fillRepresenterBg.gameObject.SetActive(true);
            _pointGainIndicatorText.gameObject.SetActive(true);
        }
        else
        {
            SetGainPointText("");

            _pointCounterText.gameObject.SetActive(false);
            _fillRepresenter.gameObject.SetActive(false);
            _fillRepresenterBg.gameObject.SetActive(false);
            _pointGainIndicatorText.gameObject.SetActive(false);
        }
    }

    public void GainPointSequence(int amount)
    {
        _pointGainIndicatorTextTween_02?.Kill();
        _pointGainIndicatorTextTween_01?.Kill();

        _pointGainIndicatorText.rectTransform.anchoredPosition = _pointGainIndicatorTextOriginalPosition;
        _pointGainIndicatorTextTween_02 = _pointGainIndicatorText.transform.DOMoveY(_pointGainIndicatorText.transform.position.y + 15f, 1f / GameStateManager.Instance.GlobalValues.AnimationSpeed).SetEase(Ease.InOutSine);

        _pointGainIndicatorText.gameObject.SetActive(true);
        _pointGainIndicatorText.text = "+" + amount.ToString() + " <sprite name=resource>";

        _pointGainIndicatorTextTween_01 = _pointGainIndicatorText.DOFade(1f, 0.1f / GameStateManager.Instance.GlobalValues.AnimationSpeed).SetEase(Ease.InOutSine).OnComplete(() =>
        _pointGainIndicatorTextTween_01 = _pointGainIndicatorText.transform.DOScale(1.2f, 0.4f).SetEase(Ease.InOutSine).OnComplete(() => 
        _pointGainIndicatorTextTween_01 = _pointGainIndicatorText.transform.DOScale(_pointGainIndicatorTextOriginalTransform.localScale, 0.4f / GameStateManager.Instance.GlobalValues.AnimationSpeed).SetEase(Ease.InOutSine).OnComplete(() =>
        _pointGainIndicatorTextTween_01 = _pointGainIndicatorText.DOFade(0f, 0.1f / GameStateManager.Instance.GlobalValues.AnimationSpeed).SetEase(Ease.InOutSine).OnComplete(() =>
        _pointGainIndicatorText.gameObject.SetActive(false)))));
    }

    void SetGainPointText(string text) => _pointGainIndicatorText.text = text;

    public void UpdatePointCounterText(int currentAmount, int pointsNeededForWin)
    {
        _pointCounterText.text = $"{currentAmount}/{pointsNeededForWin} <sprite name=resource>";
        _fillRepresenter.DOFillAmount((float)currentAmount / pointsNeededForWin,1f);
    }
}

[Serializable]
public struct PointUiHandlerData
{
    public TextMeshProUGUI PointCounterText;
    public Image FillRepresenter;
    public Image FillRepresenterBg;
    public TextMeshProUGUI PointGainIndicatorText;
}