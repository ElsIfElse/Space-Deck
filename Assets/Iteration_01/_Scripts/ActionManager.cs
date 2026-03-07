using System.Collections;
using UnityEngine;

public class ActionManager : MonoBehaviour,ICoroutineHelper
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
    
    public GamePLayStart GamePLayStart;
    public GameplayTurnStart GameplayTurnStart;
    public GamePlayPlayCard GamePlayPlayCard;
    public GameplayEndTurn GameplayEndTurn;

    // public Button EndTurnBtn;
    public CardVfx CardVfx;
    public CardEffects cardEffects;
    bool _canInteract = true; public bool CanInteract => _canInteract;
    int _cardsPlayedThisTurn = 0; public int CardsPlayedThisTurn => _cardsPlayedThisTurn;

    [HideInInspector] public CardEffects CardEffects;


    #region Initialization
    void CreateObjects()
    {
        CardVfx = new CardVfx();
        CardEffects = new CardEffects(HandManager,CardVfx);

        GamePLayStart = new GamePLayStart();
        GameplayTurnStart = new GameplayTurnStart();
        GamePlayPlayCard = new GamePlayPlayCard();
        GameplayEndTurn = new GameplayEndTurn();

        GameplayStateDataStruct data = new()
        {
            CoroutineHelper = this,
            ManaHandler = ManaHandler,
            HandManager = HandManager,
            CardVfx = CardVfx,
            CardEffects = CardEffects,
            TurnManager = TurnManager,
            DeckManager = DeckManager,
            DiscardPileManager = DiscardPileManager,
            GameplayEndTurn = GameplayEndTurn,
        };

        GamePLayStart.Initialize(data);
        GameplayTurnStart.Initialize(data);
        GamePlayPlayCard.Initialize(data);
        GameplayEndTurn.Initialize(data);
    }

    void Start()
    {
        CreateObjects();
    }
    #endregion

    #region Gameplay Start
    public void StartGame() => StartCoroutine(StartGamePlayRoutine());
    public void TurnStart() => StartCoroutine(GameplayTurnStart.OnTurnStartRoutine());
    public void OnPlayCard(GameplayCardSlot slot) => StartCoroutine(GamePlayPlayCard.PlayCard(slot));
    public void OnEndTurn() => StartCoroutine(GameplayEndTurn.EndTurnRoutine());

    IEnumerator StartGamePlayRoutine()
    {
        yield return StartCoroutine(GamePLayStart.StartGamePlayRoutine());
    }

    #region Routine Helpers
    public IEnumerator StartRoutine(IEnumerator routine)
    {
        if(routine != null) 
        yield return StartCoroutine(routine);
    }

    public void KillRoutine(IEnumerator routine)
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

    public void DrawCard(int amountOfCardsToBeDrawn,bool isTurnStart = true)
    {
        StartCoroutine(GameplayTurnStart.DrawCards(isTurnStart,amountOfCardsToBeDrawn));
    }
    
    // /// <summary>
    // /// Sets the state of the gameplay so that player is blocked from playing cards at times.
    // /// </summary>
    public void SetcanInteract(bool state) => _canInteract = state;
    // bool IsCardEffectDoubled() => CardEffects.DoubleNextTurn;

    #endregion
}