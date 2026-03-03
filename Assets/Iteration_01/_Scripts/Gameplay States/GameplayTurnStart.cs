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
    bool debug = false;
    
    public void Initialize(GameplayStateDataStruct data)
    {
        _handManager = data.HandManager;
        _deckManager = data.DeckManager;
        _coroutineHelper = data.CoroutineHelper;
        _manaHandler = data.ManaHandler;
        _gameplayEndTurn = data.GameplayEndTurn;
        _discardPileManager = data.DiscardPileManager;
    }
    public IEnumerator OnTurnStartRoutine()
    {
        yield return _coroutineHelper.StartRoutine(DrawCards(4));
    }

    public IEnumerator DrawCards(int amount = 4)
    {
        SetCanInteract(false);
        if(debug) Debug.Log("Drawing cards");
        _manaHandler.ResetMana();

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
        _handManager.RearrangeCardSlots(true);
        yield return null;

        // Move cards to the slot positions
        for(int i = 0; i< amount; i++)
        {
            Card card = _deckManager.RemoveTopCard();

            if(card == null)
            {
                Debug.Log("Not enough cards in deck. Shuffling discard pile into deck.");
                yield return _coroutineHelper.StartRoutine(MoveCardsFromDiscardPileToDeckRoutine());
                card = _deckManager.RemoveTopCard();
            } 

            cards.Add(card);
            MoveCardToSlotPosition(card,slots[i]);
            yield return new WaitForSeconds(0.05f);
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
    }

    void Setcards()
    {
        
    }
    void MoveCardToSlotPosition(Card card, GameplayCardSlot slot)
    {
        CardMover.Instance.MoveCardToSlotPosition(card,slot);
    }

    void SetCanInteract(bool state) => ActionManager.Instance.SetcanInteract(state);

    public IEnumerator MoveCardsFromDiscardPileToDeckRoutine()
{
    Debug.Log("Moving cards from discard pile to deck..");
    List<Card> cardsToMove = new();

    int count = _discardPileManager.DiscardedPileList.Count;
    for(int i = 0; i < count; i++)
    {
        Card removedCard = _discardPileManager.RemoveCardFromDiscardedPile();
        cardsToMove.Add(removedCard);
        _deckManager.AddCardToDeck(removedCard);
    }

    _deckManager.ShuffleDeck();

    yield return null;

    foreach(Card card in cardsToMove)
    {
        CardMover.Instance.MoveCardFromDiscardPileToDeck(card);
        yield return new WaitForSeconds(0.05f);
    }

    yield return new WaitForSeconds(CardMover.Instance.DiscardPileToDeckMoveTime);
    Debug.Log("Cards moved!");
}
}