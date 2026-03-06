using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePLayStart : IGameplayState
{

    ICoroutineHelper _coroutineHelper;
    TurnManager _turnManager;
    ManaHandler _manaHandler;
    DeckManager _deckManager;

    public void Initialize(GameplayStateDataStruct data)
    {
        _coroutineHelper = data.CoroutineHelper;
        _turnManager = data.TurnManager;
        _deckManager = data.DeckManager;
        _manaHandler = data.ManaHandler;
    }

    public void StartGame()
    {
        _coroutineHelper.StartRoutine(StartGamePlayRoutine());
    }

    public IEnumerator StartGamePlayRoutine()
    {
        SetCanInteract(false);
        AudioManager.Instance.PlayScore(MenuUiManager.Instance.ChooseLevelUiHandler.ChoosenMap.Score);
        InitializeSystems();
        SetCards();
        yield return new WaitForSeconds(1f);
        SetCanInteract(true);
        ActionManager.Instance.TurnStart();
    }

    void SetCards()
    {
        CreateCards();
        ShuffleDeck();
        SetcardPositions();
    }
    void SetcardPositions()
    {
        int cardCount = 0;
        foreach(Card card in _deckManager.CardsInDeck)
        {
            card.gameObject.transform.position = _deckManager.DeckPosition.position + new Vector3(cardCount*0.01f,0,cardCount*0.05f);
            cardCount++;
        }
    }
    void CreateCards()
    {
        foreach(BaseCardData cardData in PlayerDeckHandler.Instance.RuntimeCards)
        {
            GameObject newCardObj = CardFactory.Instance.CreateNewCard();
            Card newCard = newCardObj.GetComponent<Card>();
            newCard.SetCard(cardData);
            _deckManager.AddCardToDeck(newCard);
        }
    }

    void InitializeSystems()
    {
        _turnManager.Initialize();
        _manaHandler.Initialize();
        PointCounterManager.Instance.Initialize();
        GameplayUiManager.Instance.LocationInfoUiHandler.Initialize();
    }

    void StartRoundOne()
    {
        
    }

    void ShuffleDeck() => _deckManager.ShuffleDeck();
    void SetCanInteract(bool state) => ActionManager.Instance.SetcanInteract(state);
}