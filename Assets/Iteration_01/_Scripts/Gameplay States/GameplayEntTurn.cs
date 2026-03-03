using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class GameplayEndTurn : IGameplayState
{
    ICoroutineHelper _coroutineHelper;
    TurnManager _turnManager;
    HandManager _handManager;
    DeckManager _deckManager;
    DiscardPileManager _discardPileManager;
    CardEffects _cardEffects;

    public void Initialize(GameplayStateDataStruct data)
    {
        _coroutineHelper = data.CoroutineHelper;
        _turnManager = data.TurnManager;
        _handManager = data.HandManager;
        _deckManager = data.DeckManager;
        _discardPileManager = data.DiscardPileManager;
        _cardEffects = data.CardEffects;
    }

    public IEnumerator EndTurnRoutine()
    {
        if(!ActionManager.Instance.CanInteract) yield break;
        SetCanInteract(false);

        Card growero = _handManager.IsCardInHand(CardType.Growero);
        if(growero != null)
        {
            yield return _coroutineHelper.StartRoutine(_cardEffects.AddValueToCard(growero, _cardEffects.GroweroGrowAmount));
        }
        
        if(IsGameWon())
        {
            yield return _coroutineHelper.StartRoutine(OnGameWon());
            yield break;
        }
        if(WasLastTurn())
        {
            yield return _coroutineHelper.StartRoutine(OnGameLost());
            yield break;
        }

        if(!IsHandEmpty()) yield return _coroutineHelper.StartRoutine(MoveCardsFromHandToDiscardPile());
        _turnManager.ChangeTurn();
        ActionManager.Instance.TurnStart();
    }

    bool IsGameWon() => PointCounterManager.Instance.CurrentPoints >= PointCounterManager.Instance.PointsNeededForWin;
    bool WasLastTurn() => _turnManager.CurrentTurn == _turnManager.MaxTurn;
    bool IsHandEmpty() => _handManager.IsHandEmpty();
    void SetCanInteract(bool state) => ActionManager.Instance.SetcanInteract(state);

    IEnumerator MoveCardsFromHandToDiscardPile()
    {
        List<GameplayCardSlot> slots = _handManager.NotEmptySlots();

        foreach(GameplayCardSlot slot in slots)
        {
            CardMover.Instance.MoveCardFromHandToDiscardPile(slot.CardInSlot());
            _discardPileManager.AddCardToDisardedPile(slot.CardInSlot());
            _handManager.RemoveCardFromSlot(slot);
            yield return new WaitForSeconds(0.05f);
        }   

        yield return new WaitForSeconds(CardMover.Instance.HandToDiscardPileMoveTime);
    }
    IEnumerator OnGameWon()
    {
        GameStateManager.Instance.ChangeState(GameStateEnum.Menu);
        yield return null;
    }
    IEnumerator OnGameLost()
    {
        MenuManager.Instance.CurrencyHandler.AddCurrency_Primary(PointCounterManager.Instance.CurrentPoints / 5);
        GameStateManager.Instance.ChangeState(GameStateEnum.Menu);
        yield return null;
    }
    void MoveCardToSlotPosition(Card card, GameplayCardSlot slot) => CardMover.Instance.MoveCardToSlotPosition(card,slot);
}