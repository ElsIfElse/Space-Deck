using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffects
{
    HandManager _handManager;
    CardVfx _cardVfx;

    public int GroweroGrowAmount = 2;
    private int _cardsPlayedThisTurn = 0;
    
    ICoroutineHelper _coroutineHelper;
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
    public IEnumerator StrenghtenAllCardsInHand(int amount)
    {
        List<GameplayCardSlot> notemptySlots = _handManager.NotEmptySlots();

        foreach(GameplayCardSlot slot in notemptySlots)
        {
            Card card = slot.CurrentCardInSlot;
            card.SetcardValue(card.CardValue + amount);
            _cardVfx.CardForgerEffect(card);
            AudioManager.Instance.Play(AudioType.ForgerBell);
            yield return new WaitForSeconds(0.15f / GameStateManager.Instance.GlobalValues.AnimationSpeed);
        }
    }

    public IEnumerator ChangeHand(int additionalCardsToBeDrawn)
    {
        int cardsInHandCount = _handManager.NotEmptySlots().Count;
        yield return ActionManager.Instance.GameplayEndTurn.MoveCardsFromHandToDiscardPile();
        ActionManager.Instance.DrawCard(cardsInHandCount+1+additionalCardsToBeDrawn,false);
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