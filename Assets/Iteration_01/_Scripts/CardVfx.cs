using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CardVfx
{
    Tween _upgradeScaleTween;
    Tween _upgradeRotateTween;

    Tween _cantDoThatColorTween;
    Tween _cantDoThatScaleTween;
    public float CantDoThatEffectLength
    {
        get
        {
            return 0.5f / GameStateManager.Instance.GlobalValues.AnimationSpeed; 
        }
    }

    public void UseCardEffect(Card card)
    {
        Transform cardTransform = card.gameObject.transform;
        Vector3 originalScale = card.gameObject.transform.localScale;
        Vector3 originalRotation = card.gameObject.transform.rotation.eulerAngles;

        cardTransform.DOScale(card.gameObject.transform.localScale * 1.1f, 0.2f).SetEase(Ease.InOutBounce).OnComplete(() => cardTransform.DOScale(originalScale, 0.2f).SetEase(Ease.InOutBounce));
        cardTransform.DORotate(new Vector3(0, 15, 0), 0.1f).SetEase(Ease.InOutBounce).OnComplete(() => 
        cardTransform.DORotate(new Vector3(0, -15, 0), 0.1f).SetEase(Ease.InOutBounce).OnComplete(() => 
        cardTransform.DORotate(originalRotation, 0.1f).SetEase(Ease.InOutBounce)));
    }

    public void CardForgerEffect(Card card)
    {
        Transform cardTransform = card.gameObject.transform;
        Vector3 originalScale = card.gameObject.transform.localScale;

        cardTransform.DOScale(card.gameObject.transform.localScale * 0.9f, 0.2f).SetEase(Ease.InOutBounce).OnComplete(() => 
        cardTransform.DOScale(1.3f, 0.1f).SetEase(Ease.InOutBounce)).OnComplete(() => 
        cardTransform.DOScale(originalScale, 0.1f).SetEase(Ease.InOutBounce));
    }

    public void CantDoThatEffect(Card card = null,MenuSlot menuSlot = null)
    {
        if(menuSlot != null)
        {
            _cantDoThatScaleTween?.Kill();
            _cantDoThatColorTween?.Kill();

            Transform cardTransform = menuSlot._cardGameObject.transform;
            Color originalColor = Color.white;

            _cantDoThatScaleTween = cardTransform.DOScale(0.8f, CantDoThatEffectLength * 0.2f).SetEase(Ease.InOutBounce).OnComplete(() => 
            _cantDoThatScaleTween = cardTransform.DOScale(1, CantDoThatEffectLength * 0.8f).SetEase(Ease.InOutBounce));

            _cantDoThatColorTween = menuSlot.MeshRenderer.material.DOColor(Color.red, CantDoThatEffectLength * 0.2f).SetEase(Ease.InOutBounce).OnComplete(() => 
            _cantDoThatColorTween = menuSlot.MeshRenderer.material.DOColor(originalColor, CantDoThatEffectLength * 0.8f).SetEase(Ease.InOutBounce));
        }
        else
        {
            _cantDoThatScaleTween?.Kill();
            _cantDoThatColorTween?.Kill();

            Transform cardTransform = card.gameObject.transform;
            Color originalColor = Color.white;

            _cantDoThatScaleTween = cardTransform.DOScale(0.8f, 0.1f).SetEase(Ease.InOutBounce).OnComplete(() => 
            _cantDoThatScaleTween = cardTransform.DOScale(1, 0.4f).SetEase(Ease.InOutBounce));

            _cantDoThatColorTween = card.MeshRenderer.material.DOColor(Color.red, 0.1f).SetEase(Ease.InOutBounce).OnComplete(() => 
            _cantDoThatColorTween = card.MeshRenderer.material.DOColor(originalColor, 0.4f).SetEase(Ease.InOutBounce));
        }
    }

    public void CardUpgradedEffect(GameObject card)
    {
        _upgradeScaleTween?.Kill();
        _upgradeRotateTween?.Kill();

        Transform cardTransform = card.transform;
        Vector3 originalScale = card.transform.localScale;
        Vector3 originalRotation = card.transform.rotation.eulerAngles;

        _upgradeScaleTween = cardTransform.DOScale(card.transform.localScale * 1.1f, 0.2f).SetEase(Ease.InOutBounce).OnComplete(() => 
        _upgradeScaleTween = cardTransform.DOScale(originalScale, 0.2f).SetEase(Ease.InOutBounce));

        _upgradeRotateTween = cardTransform.DORotate(new Vector3(0, 15, 0), 0.1f).SetEase(Ease.InOutBounce).OnComplete(() => 
        _upgradeRotateTween = cardTransform.DORotate(new Vector3(0, -15, 0), 0.1f).SetEase(Ease.InOutBounce).OnComplete(() => 
        _upgradeRotateTween = cardTransform.DORotate(originalRotation, 0.1f).SetEase(Ease.InOutBounce)));
    }
    public void GroweroValueGainEffect(GameObject card)
    {
        _upgradeScaleTween?.Kill();
        _upgradeRotateTween?.Kill();

        Transform cardTransform = card.transform;
        Vector3 originalScale = card.transform.localScale;
        Vector3 originalRotation = card.transform.rotation.eulerAngles;

        _upgradeScaleTween = cardTransform.DOScale(card.transform.localScale * 1.3f, 0.4f).SetEase(Ease.InOutBounce).OnComplete(() => 
        _upgradeScaleTween = cardTransform.DOScale(originalScale, 0.2f).SetEase(Ease.InOutBounce));

        _upgradeRotateTween = cardTransform.DORotate(new Vector3(0, 30, 0), 0.2f).SetEase(Ease.InOutBounce).OnComplete(() => 
        _upgradeRotateTween = cardTransform.DORotate(new Vector3(0, -30, 0), 0.2f).SetEase(Ease.InOutBounce).OnComplete(() => 
        _upgradeRotateTween = cardTransform.DORotate(originalRotation, 0.1f).SetEase(Ease.InOutBounce)));
    }

}