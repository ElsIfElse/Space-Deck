using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuSlotHandler
{
    [SerializeField] List<MenuSlot> _menuSlots = new();

    bool _areSlotsInitialized = false;
    Light _selectedSlotLight;
    MenuSlot _selectedSlot;
    Tween _lightTween;
    Tween _lightMoveTween;
    CardEffectDescriptions _cardEffectDescriptions;

    public MenuSlotHandler(MenuSlotHandlerData data, CardEffectDescriptions cardEffectDescriptions)
    {
        _cardEffectDescriptions = cardEffectDescriptions;
        _menuSlots = data.MenuSlots;
        _selectedSlotLight = data.SelectedSlotLight;
    }

    /// <summary>
    /// Sets the menu view. Choose between deck view and locked cards view. True = Deck view, False = Locked cards view.
    /// </summary>
    /// <param name="deckView">Set to true for deck view and false for locked cards view.</param>
    public void SetMenuSlots(bool deckView)
    {
        List<BaseCardData> cards = deckView ? PlayerDeckHandler.Instance.RuntimeCards : PlayerDeckHandler.Instance.RuntimeLockedCards;

        int slotIndex = 0;

        foreach(BaseCardData baseCardData in cards)
        {
            _menuSlots[slotIndex].gameObject.SetActive(true);
            _menuSlots[slotIndex].InitializeMenuSlot(baseCardData,_cardEffectDescriptions);
            slotIndex++;
        }

        for(int i = slotIndex; i < _menuSlots.Count; i++)
        {
            _menuSlots[i].gameObject.SetActive(false);
        }

        _areSlotsInitialized = true;
    }

    public void EmptyMenuSlots()
    {
        foreach(MenuSlot slot in _menuSlots) slot.EmptySlot();
    }

    public void HideMenuSlots()
    {
        foreach(MenuSlot slot in _menuSlots) slot.gameObject.SetActive(false);
    }

    /// <summary>
    /// Sets the menu view. Choose between deck view and locked cards view. True = Deck view, False = Locked cards view.
    /// </summary>
    /// <param name="deckView"> Set to true for deck view and false for locked cards view.</param>
    public void ShowMenuSlots(bool deckView)
    {
        if(!_areSlotsInitialized) SetMenuSlots(deckView);

        foreach(MenuSlot slot in _menuSlots)
        {
            if(!slot.IsSlotEmpty()) slot.gameObject.SetActive(true);
        }
    }

    public void HandleSlotClick(MenuSlot slot)
    {
        _lightTween?.Kill();
        _lightMoveTween?.Kill();

        _lightMoveTween = _selectedSlotLight.transform.DOMove(slot.gameObject.transform.position + new Vector3(0,0.5f,6.5f),0.3f);
        _lightTween = _selectedSlotLight.DOIntensity(5.5f,0.3f);
    }

    public void TurnOffSlotLight()
    {
        _lightTween?.Kill();
        _lightTween = _selectedSlotLight.DOIntensity(0f,0.3f);
    }
}



[Serializable]
public struct MenuSlotHandlerData
{
    public List<MenuSlot> MenuSlots;
    public Light SelectedSlotLight;
}
