using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffects
{
    HandManager _handManager;
    CardVfx _cardVfx;

    public int GroweroGrowAmount = 2;
    private int _cardsPlayedThisTurn = 0;
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
    public IEnumerator AddValueToCard(Card card,int amount)
    {
        card.CardValue += amount;
        _cardVfx.GroweroValueGainEffect(card.gameObject);
        card.UpdateValueText();
        yield return new WaitForSeconds(0.5f);
    }

    public void IncreaseCardsPlayedThisTurn()
    {
        _cardsPlayedThisTurn++;
        GameplayUiManager.Instance.TurnUiHandler.UpdateCardsPlayedThisTurnCounter(_cardsPlayedThisTurn);
    }
    public void ResetCardsPlayedThisTurn()
    {
        _cardsPlayedThisTurn = 0;
        GameplayUiManager.Instance.TurnUiHandler.UpdateCardsPlayedThisTurnCounter(_cardsPlayedThisTurn);
    }

    public int CardsPlayedThisTurn() => _cardsPlayedThisTurn;

}