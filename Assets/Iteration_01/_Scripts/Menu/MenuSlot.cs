using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MenuSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    public TextMeshPro CardCostText;
    public TextMeshPro CardValueText;
    public MeshRenderer MeshRenderer;
    Sprite _cardVisualSprite;
    public GameObject _cardGameObject;

    Tween _hoverTweenScale;
    Tween _hoverTweenRotation;

    Vector3 _originalRotation;

    BaseCardData _dataInSlot; public BaseCardData DataInSlot => _dataInSlot;
    CardEffectDescriptions _cardEffectDescriptions;

    public void InitializeMenuSlot(BaseCardData baseCardData, CardEffectDescriptions cardEffectDescriptions)
    {
        _cardEffectDescriptions = cardEffectDescriptions;

        _dataInSlot = baseCardData;
        _dataInSlot.EffectDescription_01 = _cardEffectDescriptions.EffectDescriptions[baseCardData.CardType].Effect_01_Description;
        _dataInSlot.EffectDescription_02 = _cardEffectDescriptions.EffectDescriptions[baseCardData.CardType].Effect_02_Description; 
        _cardVisualSprite = _dataInSlot.CardFront;

        SetCardVisual(_cardVisualSprite);

        CardCostText.text = _dataInSlot.ManaCost.ToString();
        CardValueText.text = _dataInSlot.CardValue.ToString();

        baseCardData.SetCardUpgrades(this);

        _originalRotation = transform.rotation.eulerAngles;
    }

    public void EmptySlot()
    {
        _dataInSlot = null;
        gameObject.SetActive(false);
    }

    void SetCardVisual(Sprite sprite) => MeshRenderer.material.SetTexture("_BaseMap",sprite.texture);
    public bool IsSlotEmpty() => _dataInSlot == null;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(IsSlotEmpty()) return;
        MenuUiManager.Instance.MenuCardDetailUiHandler.ShowCardDetail(_dataInSlot);

        _hoverTweenRotation?.Kill();
        _hoverTweenScale?.Kill();

        _hoverTweenScale = _cardGameObject.transform.DOScale(Vector3.one * 1.1f,0.2f);
        _hoverTweenRotation = _cardGameObject.transform.DORotate(new Vector3(0, 15, 0),0.1f).OnComplete(() => 
        _hoverTweenRotation = _cardGameObject.transform.DORotate(new Vector3(0, -15, 0),0.1f)).OnComplete(() => 
        _hoverTweenRotation = _cardGameObject.transform.DORotate(_originalRotation,0.2f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MenuUiManager.Instance.MenuCardDetailUiHandler.HideCardDetail();

        _hoverTweenRotation?.Kill();
        _hoverTweenScale?.Kill();
        _cardGameObject.transform.rotation = Quaternion.Euler(_originalRotation);
        _cardGameObject.transform.localScale = Vector3.one;
    }

    public void UpdateSlotValues()
    {
        CardCostText.text = _dataInSlot.ManaCost.ToString();
        CardValueText.text = _dataInSlot.CardValue.ToString();

        UpdateDescriptionTexts();
    }
    public void UpdateDescriptionTexts()
    {
        _dataInSlot.EffectDescription_01 = _cardEffectDescriptions.EffectDescriptions[_dataInSlot.CardType].Effect_01_Description;
        _dataInSlot.EffectDescription_02 = _cardEffectDescriptions.EffectDescriptions[_dataInSlot.CardType].Effect_02_Description;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(IsSlotEmpty()) return;
        AudioManager.Instance.Play(AudioType.Click,0,true);
        MenuUiManager.Instance.MenuCardUpgradeUiHandler.HandleCardClick(_dataInSlot, this);
        MenuManager.Instance.MenuSlotHandler.HandleSlotClick(this);
    }
}
