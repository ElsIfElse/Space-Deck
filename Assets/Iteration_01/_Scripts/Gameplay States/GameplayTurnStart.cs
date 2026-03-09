using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayTurnStart : IGameplayState
{
    HandManager _handManager;
    DeckManager _deckManager;
    ICoroutineHelper _coroutineHelper;
    ManaHandler _manaHandler;
    GameplayEndTurn _gameplayEndTurn;
    DiscardPileManager _discardPileManager;
    CardEffects _cardEffects;
    bool debug = false;
    
    public void Initialize(GameplayStateDataStruct data)
    {
        _handManager = data.HandManager;
        _deckManager = data.DeckManager;
        _coroutineHelper = data.CoroutineHelper;
        _manaHandler = data.ManaHandler;
        _gameplayEndTurn = data.GameplayEndTurn;
        _discardPileManager = data.DiscardPileManager;
        _cardEffects = data.CardEffects;
    }
    public IEnumerator OnTurnStartRoutine()
    {
        yield return _coroutineHelper.StartRoutine(DrawCards(true,4));
    }

    public IEnumerator DrawCards(bool isTurnStart = true,int amount = 4)
    {
        if(!GameStateManager.Instance.HasTutorialBeenPlayed) yield return new WaitUntil(() => GameplayUiManager.Instance.TutorialUiHandler.IsFirstListDone);
        // yield return new WaitUntil(() => GameplayUiManager.Instance.TutorialUiHandler.IsFirstListDone);

        SetCanInteract(false);
        _cardEffects.ResetCardsPlayedThisTurn();
        if(debug) Debug.Log("Drawing cards");
        if(isTurnStart) _manaHandler.ResetMana();

        List<GameplayCardSlot> slots = new();
        List<Card> cards = new();

        // Activate the needed amount of slots
        for(int i = 0; i< amount; i++)
        {
            GameplayCardSlot slot = _handManager.ActivateSlot();
            slots.Add(slot);    
        }

        // Rearrange slot positions
        yield return null;
        _handManager.RearrangeCardSlots();
        yield return new WaitForSeconds(_handManager.RearrangeCardsLength*amount);

        // Move cards to the slot positions
        for(int i = 0; i< amount; i++)
        {
            Card card = null;
            yield return _coroutineHelper.StartRoutine(TryDrawCard(c => card = c));

            if(card == null)
            {
                Debug.Log("Not enough cards in deck. Shuffling discard pile into deck.");
                yield return _coroutineHelper.StartRoutine(MoveCardsFromDiscardPileToDeckRoutine());
                card = _deckManager.RemoveTopCard();
            } 

            cards.Add(card);
            MoveCardToSlotPosition(card,slots[i]);
            yield return new WaitForSeconds(0.15f);
        }

        if(debug) Debug.Log($"Cards drawn [${cards.Count}].");

        // Wait until cards are at place        
        yield return new WaitForSeconds(CardMover.Instance.DeckToSlotMoveTime);

        // Parent and add cards to slots
        for(int i = 0; i< amount; i++)
        {
            slots[i].AddCardToSlot(cards[i]);
            slots[i].ParentCardInSlot();
        }
        yield return null;
        SetCanInteract(true);

        if(!GameStateManager.Instance.HasTutorialBeenPlayed) GameplayUiManager.Instance.TutorialUiHandler.ShowNextPanel();
        GameStateManager.Instance.HasTutorialBeenPlayed = true;
    }
    void MoveCardToSlotPosition(Card card, GameplayCardSlot slot)
    {
        CardMover.Instance.MoveCardToSlotPosition(card,slot);
        AudioManager.Instance.Play(AudioType.CardDealing,0,true);
    }

    void SetCanInteract(bool state) => ActionManager.Instance.SetcanInteract(state);

    public IEnumerator MoveCardsFromDiscardPileToDeckRoutine()
    {
        int count = _discardPileManager.DiscardedPileList.Count;
        for(int i = 0; i < count; i++)
        {
            Card removedCard = _discardPileManager.RemoveCardFromDiscardedPile();
            _deckManager.AddCardToDeck(removedCard);
        }
    
        _deckManager.ShuffleDeck(); // Shuffle first
    
        yield return null;
    
        // Now move visually in shuffled deck order
        for(int i = 0; i < _deckManager.DeckCount(); i++)
        {
            Card card = _deckManager.CardsInDeck[i];
            CardMover.Instance.MoveCardFromDiscardPileToDeck(card, i);
            AudioManager.Instance.Play(AudioType.CardDealing,0,true);
            yield return new WaitForSeconds(0.1f);
        }
    
        yield return new WaitForSeconds(CardMover.Instance.DiscardPileToDeckMoveTime);
    }
    IEnumerator TryDrawCard(Action<Card> onCard)
    {
        Card card = _deckManager.RemoveTopCard();

        if (card == null)
        {
            Debug.Log("Not enough cards in deck. Shuffling discard pile into deck.");
            yield return _coroutineHelper.StartRoutine(MoveCardsFromDiscardPileToDeckRoutine());
            card = _deckManager.RemoveTopCard();
        }

        onCard?.Invoke(card);
    }

}