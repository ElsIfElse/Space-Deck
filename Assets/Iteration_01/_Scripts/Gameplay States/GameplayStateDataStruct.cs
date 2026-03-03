public struct GameplayStateDataStruct
{
    public ICoroutineHelper CoroutineHelper;
    public TurnManager TurnManager;
    public HandManager HandManager;
    public DeckManager DeckManager; 
    public DiscardPileManager DiscardPileManager;
    public ManaHandler ManaHandler;
    public CardVfx CardVfx;
    public CardEffects CardEffects;
    public GameplayEndTurn GameplayEndTurn;
    public CurrencyHandler CurrencyHandler;
}