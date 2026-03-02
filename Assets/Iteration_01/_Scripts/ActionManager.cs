using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    #region Singleton Init
    public static ActionManager Instance;

    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    #endregion
    
    public DeckManager DeckManager; 
    public HandManager HandManager;
    public DiscardPileManager DiscardPileManager;
    public ManaHandler ManaHandler;
    public TurnManager TurnManager;

    // public Button EndTurnBtn;
    public CardVfx CardVfx;
    public CardEffects cardEffects;
    bool _canPlayCard = true;
    int _cardsPlayedThisTurn = 0; public int CardsPlayedThisTurn => _cardsPlayedThisTurn;

    [HideInInspector] public CardEffects CardEffects;


    #region Initialization
    void CreateObjects()
    {
        CardVfx = new CardVfx();
        CardEffects = new CardEffects(HandManager,CardVfx);
    }

    void Start()
    {
        CreateObjects();
    }
    #endregion

    #region Gameplay Start
    public void StartGame()
    {
        StartCoroutine(StartGamePlayRoutine());
    }

    IEnumerator StartGamePlayRoutine()
    {
        AudioManager.Instance.PlayScore(MenuUiManager.Instance.ChooseLevelUiHandler.ChoosenMap.Score);
        TurnManager.Initialize();
        ManaHandler.Initialize();

        CreateCardsAtGameStart();
        PointCounterManager.Instance.Initialize();
        GameplayUiManager.Instance.LocationInfoUiHandler.Initialize();
        yield return new WaitForSeconds(0.5f);
        OnStartRound();
    }

    void CreateCardsAtGameStart()
    {
        int cardCount = 0;
        foreach(BaseCardData cardData in PlayerDeckHandler.Instance.RuntimeCards)
        {
            GameObject newCardObj = CardFactory.Instance.CreateNewCard();
            // newCardObj.transform.position = DeckManager.DeckPosition.position;
            CardMover.Instance.MoveCard(CardMover.Instance._cardPositions[CardPositionType.Deck],newCardObj.transform,CardPositionType.Deck,cardCount);
            Card newCard = newCardObj.GetComponent<Card>();
            newCard.SetCard(cardData);
            DeckManager.AddCardToDeck(newCard);
            cardCount++;
        }

        DeckManager.ShuffleDeck();
    }
    #endregion

    #region Start Turn
    public IEnumerator DrawCard(int amount = 4)
    {
        _canPlayCard = false;
        List<GameplayCardSlot> slots = new();

        for(int i = 0; i < amount; i++)
        {
            Card newCard = DeckManager.RemoveTopCard();

            if(newCard == null)
            {
                yield return StartCoroutine(MoveCardsFromDiscardPileToDeckRoutine());
                newCard = DeckManager.RemoveTopCard();
            }

            GameplayCardSlot slotToAdd = HandManager.AddCardToSlot(newCard);
            slots.Add(slotToAdd);
            yield return null;
        }

        HandManager.RearrangeCardSlots(true);

        yield return new WaitForSeconds(0.01f);

        foreach(GameplayCardSlot slot in slots)
        {
            CardMover.Instance.MoveCard(slot.transform,slot.CurrentCardInSlot.transform,CardPositionType.Hand);
            PlayCardDealingSound();
            yield return new WaitForSeconds(0.3f / GameStateManager.Instance.GlobalValues.AnimationSpeed);
        }

        _canPlayCard = true; 
    }

    IEnumerator OnRoundStartRoutine()
    {
        _canPlayCard = false;
        _cardsPlayedThisTurn = 0;
        ManaHandler.ResetMana();
        yield return DrawCard(4);
        _canPlayCard = true;
    }

    public void OnStartRound()
    {
        StartCoroutine(OnRoundStartRoutine());
    }
    #endregion
    
    #region Play Card
    public void OnPlayCard(GameplayCardSlot cardSlot)
    {
        if(_canPlayCard == false) return;

        Card card = cardSlot.CardInSlot();
        if(!ManaHandler.HasEnoughMana(card.ManaCost))
        {
            AudioManager.Instance.Play(AudioType.CantDoThat);
            CardVfx.CantDoThatEffect(card);
            return;
        }

        StartCoroutine(OnPlayCardRoutine(cardSlot));
    }
    IEnumerator OnPlayCardRoutine(GameplayCardSlot cardSlot)
    {
        SetCanPlayCard(false);
        PlayCardPlayedSound();

        Card currentcard = cardSlot.CardInSlot();

        ManaHandler.SpendMana(currentcard.ManaCost);

        CardMover.Instance.MoveCard(CardMover.Instance._cardPositions[CardPositionType.Field],currentcard.transform,CardPositionType.Field);
        yield return new WaitForSeconds(1f / GameStateManager.Instance.GlobalValues.AnimationSpeed);

        if(IsCardEffectDoubled())
        {
            yield return currentcard.CardEffect(CardVfx);
            yield return new WaitForSeconds(1f / GameStateManager.Instance.GlobalValues.AnimationSpeed);
            CardEffects.DoubleNextTurn = false;
            _cardsPlayedThisTurn++;
        }
        
        yield return currentcard.CardEffect(CardVfx);
        yield return new WaitForSeconds(1f / GameStateManager.Instance.GlobalValues.AnimationSpeed);

        _cardsPlayedThisTurn++;
        CardMover.Instance.MoveCard(CardMover.Instance._cardPositions[CardPositionType.Discard],currentcard.transform,CardPositionType.Discard);
        PlayCardToDiscardPileSound();
        DiscardPileManager.AddCardToDisardedPile(currentcard);
        yield return new WaitForSeconds(0.5f / GameStateManager.Instance.GlobalValues.AnimationSpeed);

        HandManager.RemoveCardFromSlot(cardSlot);
        HandManager.RearrangeCardSlots();
        _canPlayCard = true;
    }
    #endregion
    
    #region End Turn
    public void OnEndTurn()
    {
        if(!_canPlayCard) return;
        StartCoroutine(OnEndTurnRoutine());
    }

    IEnumerator OnEndTurnRoutine()
    {
        if(PointCounterManager.Instance.CurrentPoints >= PointCounterManager.Instance.PointsNeededForWin)
        {
            OnGameIsWon();
            yield break;
        }
        if(TurnManager.CurrentTurn == TurnManager.MaxTurn)
        {
            OnGameLost();
            yield break;
        }

        if(!HandManager.IsHandEmpty())
        {
            if(HandManager.IsCardInHand(CardType.Growero))
            {
                Card card = HandManager.IsCardInHand(CardType.Growero);
                card.SetcardValue(card.CardValue + CardEffects.GroweroGrowAmount);
                card.UpdateValueText();
                CardVfx.UseCardEffect(card);
                
                yield return new WaitForSeconds(1f / GameStateManager.Instance.GlobalValues.AnimationSpeed);
            }

            yield return StartCoroutine(MoveRemainingCardsInHandToDiscardPileRoutine());
            yield return new WaitForSeconds(1);
        }

        if(DeckManager.DeckCount() == 0)
        {
            yield return StartCoroutine(MoveCardsFromDiscardPileToDeckRoutine());
            yield return new WaitForSeconds(1 / GameStateManager.Instance.GlobalValues.AnimationSpeed);
        }

        TurnManager.ChangeTurn();
        HandManager.ResetSlotPositions();
        yield return new WaitForSeconds(1f / GameStateManager.Instance.GlobalValues.AnimationSpeed);
        StartCoroutine(OnRoundStartRoutine());
    }
 
    IEnumerator MoveRemainingCardsInHandToDiscardPileRoutine()
    {
        foreach(GameplayCardSlot slot in HandManager.NotEmptySlots())
        {
            AudioManager.Instance.Play(AudioType.Swoosh_Short,0,true);
            Card currentCard = slot.CardInSlot();
            HandManager.RemoveCardFromSlot(slot);
            CardMover.Instance.MoveCard(CardMover.Instance._cardPositions[CardPositionType.Discard],currentCard.transform,CardPositionType.Discard);
            DiscardPileManager.AddCardToDisardedPile(currentCard);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator MoveCardsFromDiscardPileToDeckRoutine()
    {
        int cardCount = 0;
        DiscardPileManager.ShuffleDeck();
        while(DiscardPileManager.DiscardedCardPileCount() > 0)
        {
            Card cardToMove = DiscardPileManager.RemoveCardFromDiscardedPile();
            CardMover.Instance.MoveCard(CardMover.Instance._cardPositions[CardPositionType.Deck],cardToMove.transform,CardPositionType.Deck,cardCount);
            DeckManager.AddCardToDeck(cardToMove);
            AudioManager.Instance.Play(AudioType.CardDealing,0,true);
            cardCount++;
            yield return new WaitForSeconds(0.3f / GameStateManager.Instance.GlobalValues.AnimationSpeed);
        }
    }
    #endregion

    void OnGameLost()
    {
        MenuManager.Instance.CurrencyHandler.AddCurrency_Primary(PointCounterManager.Instance.CurrentPoints / 10 * 2);
        GameStateManager.Instance.ChangeState(GameStateEnum.Menu);
    }
    void OnGameIsWon()
    {
        // Change state to main menu state
        // Add points based on gained resources
        // Unlock next level
    }

    #region Routine Helpers
    public void RunRoutine(IEnumerator routine)
    {
        if(routine != null) StartCoroutine(routine);
    }

    public void StopRoutine(IEnumerator routine)
    {
        if(routine != null) StopCoroutine(routine);
    }
    #endregion
    public void RemoveAllActiveCardsFromScene()
    {
        DeckManager.RemoveAndDestroyAllCardsInDeck();
        DiscardPileManager.RemoveAndDestroyAllCardsInDiscardedPile();
        HandManager.RemoveAndDestroyAllCardsFromSlots();
    }

    #region Utilities
    void PlayCardPlayedSound() => AudioManager.Instance.Play(AudioType.CardPlayed,0,true);
    void PlayCardToDiscardPileSound() => AudioManager.Instance.Play(AudioType.CardDoDiscardPile,0,true);
    void PlayCardDealingSound () => AudioManager.Instance.Play(AudioType.CardDealing,0,true);
    void SetCanPlayCard(bool state) => _canPlayCard = state;
    bool IsCardEffectDoubled() => CardEffects.DoubleNextTurn;

    #endregion


}