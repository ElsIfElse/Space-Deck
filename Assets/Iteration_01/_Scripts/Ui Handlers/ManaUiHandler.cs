using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ManaUiHandler : IUiHandler
{
    TextMeshProUGUI _currentManaText;
    Color _originalColor;
    public ManaUiHandler(ManaUiHandlerData data)
    {
        _currentManaText = data.CurrentManaText;
        _originalColor = _currentManaText.color;
    }

    public void UpdateCurrentMana(int amount)
    {
        _currentManaText.text = "O<size=15>2:</size> " + amount.ToString();
    }

    public void GainManaEffect()
    {
        _currentManaText.transform.DOScale(1.2f, 0.2f).SetEase(Ease.InOutBounce).OnComplete(() => _currentManaText.transform.DOScale(1f, 0.2f).SetEase(Ease.InOutBounce));
        _currentManaText.DOColor(Color.green, 0.2f).SetEase(Ease.InOutBounce).OnComplete(() => _currentManaText.DOColor(_originalColor, 0.2f).SetEase(Ease.InOutBounce));
    }
    public void UseManaEffect()
    {
        _currentManaText.transform.DOScale(1.2f, 0.2f).SetEase(Ease.InOutBounce).OnComplete(() => _currentManaText.transform.DOScale(1f, 0.2f).SetEase(Ease.InOutBounce));
        _currentManaText.DOColor(Color.red, 0.2f).SetEase(Ease.InOutBounce).OnComplete(() => _currentManaText.DOColor(_originalColor, 0.2f).SetEase(Ease.InOutBounce));
    }

    public void SetState(bool state)
    {
        if(state)
        {
            _currentManaText.gameObject.SetActive(true);
        }
        else
        {
            _currentManaText.gameObject.SetActive(false);
        }
    }
}

[Serializable]
public struct ManaUiHandlerData
{
    public TextMeshProUGUI CurrentManaText;
}