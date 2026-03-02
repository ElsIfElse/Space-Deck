using System.Collections.Generic;
using UnityEngine;

public class CardEffects
{
    HandManager _handManager;
    CardVfx _cardVfx;

    public int GroweroGrowAmount = 2;
    public CardEffects(HandManager handManager, CardVfx cardVfx)
    {
        _handManager = handManager;
        _cardVfx = cardVfx;
    }

    public bool DoubleNextTurn{get;set;}
    public void StrengthenRandomCard(int amount)
    {
        List<GameplayCardSlot> notEmptySlots = _handManager.NotEmptySlots();
        int randomIndex = Random.Range(0,notEmptySlots.Count);
        Card randomCard = notEmptySlots[randomIndex].CurrentCardInSlot;
        randomCard.SetcardValue(randomCard.CardValue + amount);
        _cardVfx.CardForgerEffect(randomCard);
    }
}