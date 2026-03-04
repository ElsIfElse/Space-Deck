using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class GamePlayPlayCard : IGameplayState
{
    ICoroutineHelper _coroutineHelper;
    ManaHandler _manaHandler;
    CardVfx _cardVfx;
    HandManager _handManager;
    CardEffects _cardEffects;
    DiscardPileManager _discardPileManager;

    float _delayAfterCardPlay
    {
        get
        {
            return 0.75f / GameStateManager.Instance.GlobalValues.AnimationSpeed;
        }
    }
    public void Initialize(GameplayStateDataStruct data)
    {
        _coroutineHelper = data.CoroutineHelper;
        _manaHandler = data.ManaHandler;
        _handManager = data.HandManager;
        _cardVfx = data.CardVfx;
        _cardEffects = data.CardEffects;
        _discardPileManager = data.DiscardPileManager;
    }
    public IEnumerator PlayCard(GameplayCardSlot cardSlot)
    {
        SetCanInteract(false);
        Card card = cardSlot.CardInSlot(); 

        if(!CanAfford(card))
        {
            yield return _coroutineHelper.StartRoutine(CantDoThatEffect(card)); 
            yield break;
        }
        
        SpendMana(card);

        RemoveCardFromSlot(cardSlot);
        yield return _coroutineHelper.StartRoutine(MoveCardFromHandToField(card));

        if(IsEffectDoubled())
        {
            yield return _coroutineHelper.StartRoutine(card.CardEffect(_cardVfx));
            _cardEffects.DoubleNextTurn = false;
            yield return new WaitForSeconds(_delayAfterCardPlay);
        }   

        _cardEffects.IncreaseCardsPlayedThisTurn();
        yield return _coroutineHelper.StartRoutine(card.CardEffect(_cardVfx));
        yield return new WaitForSeconds(_delayAfterCardPlay);
        yield return _coroutineHelper.StartRoutine(MoveCardFromFieldToDiscardPile(card));
        yield return _coroutineHelper.StartRoutine(RearrangeCards()); 
        SetCanInteract(true);
    }

    IEnumerator MoveCardFromHandToField(Card card)
    {
        CardMover.Instance.MoveCardFromHandToField(card);
        AudioManager.Instance.Play(AudioType.Swoosh_Short,0,true);
        yield return new WaitForSeconds(CardMover.Instance.HandToFieldMoveTime);
    }
    IEnumerator CantDoThatEffect(Card card)
    {
        AudioManager.Instance.Play(AudioType.CantDoThat);
        _cardVfx.CantDoThatEffect(card);
        yield return new WaitForSeconds(_cardVfx.CantDoThatEffectLength);
        SetCanInteract(true);
    }
    IEnumerator MoveCardFromFieldToDiscardPile(Card card)
    {
        CardMover.Instance.MoveCardFromFieldToDiscardPile(card,_discardPileManager.DeckCount());
        _discardPileManager.AddCardToDiscardedPile(card);
        AudioManager.Instance.Play(AudioType.Swoosh_Short,0,true);
        yield return new WaitForSeconds(CardMover.Instance.FieldToDiscardPileMoveTime);
        // card.transform.localPosition = Vector3.zero;
    }
    void SpendMana(Card card) => _manaHandler.SpendMana(card.ManaCost);
    void RemoveCardFromSlot(GameplayCardSlot slot) => _handManager.RemoveCardFromSlot(slot);
    bool CanAfford(Card card) => _manaHandler.HasEnoughMana(card.ManaCost);
    void SetCanInteract(bool state) => ActionManager.Instance.SetcanInteract(state);
    IEnumerator RearrangeCards()
    {
        _handManager.RearrangeCardSlots();   
        yield return new WaitForSeconds(_handManager.RearrangeCardsLength*4);
    }
    bool IsEffectDoubled() => _cardEffects.DoubleNextTurn;

}