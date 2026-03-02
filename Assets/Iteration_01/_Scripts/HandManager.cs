using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    float _cardOffset = 1.5f;


    public List<GameplayCardSlot> _activeCardSlots = new();
    public List<GameplayCardSlot> _emptyCardSlots;

    public GameplayCardSlot AddCardToSlot(Card card)
    {
        GameplayCardSlot slot = _emptyCardSlots[0];
        slot.gameObject.SetActive(true);
        _emptyCardSlots.RemoveAt(0);
        _activeCardSlots.Add(slot);
        slot.AddCardToSlot(card);

        // card.gameObject.transform.SetParent(slot.gameObject.transform,true);
        return slot;
    }

    public Card RemoveCardFromSlot(GameplayCardSlot slot)
    {
        Card cardToRemove = slot.CurrentCardInSlot;
        slot.RemoveCardFromSlot();
        _activeCardSlots.Remove(slot);
        _emptyCardSlots.Add(slot);
        slot.gameObject.SetActive(false);
        return cardToRemove;
    }
    public bool IsHandEmpty()
    {
        foreach(GameplayCardSlot slot in _activeCardSlots)
        {
            if(!slot.IsSlotEmpty()) return false;
        }
        return true;
    }

    public void RemoveAndDestroyAllCardsFromSlots()
    {
        if(_activeCardSlots.Count == 0) return;
        foreach(GameplayCardSlot slot in _activeCardSlots)
        {
            slot.RemoveAndDestroyCardFromSlot();
            _emptyCardSlots.Add(slot);
            _activeCardSlots.Remove(slot);
            slot.gameObject.SetActive(false);
        }
    }

    public List<GameplayCardSlot> NotEmptySlots()
    {
        List<GameplayCardSlot> notEmptySlots = new();

        foreach(GameplayCardSlot slot in _activeCardSlots)
        {
            if(!slot.IsSlotEmpty()) notEmptySlots.Add(slot);
        }

        return notEmptySlots;
    }

    public void ResetSlotPositions()
    {
        foreach(GameplayCardSlot slot in _activeCardSlots)
        {
            // slot.ResetCardSlotPosition();
        }
    }

    public Card IsCardInHand(CardType cardType)
    {
        foreach(GameplayCardSlot slot in _activeCardSlots)
        {
            if(slot.IsSlotEmpty()) continue;
            if(slot.CurrentCardInSlot.CardType == cardType) return slot.CurrentCardInSlot;
        }
        return null;
    }

    float CalculateSlotPosition(int slotIndex) => (slotIndex -(_activeCardSlots.Count -1) / 2f) * _cardOffset;

    public void RearrangeCardSlots(bool immediate = false)
    {
        if(immediate)
        {
            for(int i = 0; i < _activeCardSlots.Count; i++)
            {
                _activeCardSlots[i].transform.DOMoveX(CalculateSlotPosition(i),0);
            }
        }
        else
        {
            for(int i = 0; i < _activeCardSlots.Count; i++)
            {
                _activeCardSlots[i].transform.DOMoveX(CalculateSlotPosition(i),0.3f / GameStateManager.Instance.GlobalValues.AnimationSpeed);
            }
        }
    }

    void AddSlot(GameplayCardSlot slot)
    {
        slot.gameObject.SetActive(true);
        _activeCardSlots.Add(slot);
        RearrangeCardSlots();
    }
    void RemoveSlot(GameplayCardSlot slot)
    {
        slot.gameObject.SetActive(false);
        _activeCardSlots.Remove(slot);
        RearrangeCardSlots();
    }

}
