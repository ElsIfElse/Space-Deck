using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUiManager : MonoBehaviour
{
    #region Singleton Init

    public static GameplayUiManager Instance;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }
    #endregion

    public ManaUiHandler ManaUiHandler;
    public ManaUiHandlerData ManaUiHandlerData;

    public LocationInfoUiHandler LocationInfoUiHandler;
    public LocationInfoUiHandlerData LocationInfoUiHandlerData;

    public PointUiHandler PointUiHandler;
    public PointUiHandlerData PointUiHandlerData;

    public TurnUiHandler TurnUiHandler;
    public TurnUiHandlerData TurnUiHandlerData;

    public StarUiHandler StarMover;
    public StarMoverData StarMoverData;

    public HandUiManager HandUiManager;
    public HandUiData HandUiData;

    public DeckUiHandler DeckUiHandler;
    public DeckUiData DeckUiData;

    public DiscardPileUiHandler DiscardPileUiHandler;
    public DiscardPileUiData DiscardPileUiData;

    public AnimationSpeedUiHandler AnimationSpeedUiHandler;
    public AnimationSpeedUiHandlerData AnimationSpeedUiHandlerData;

    List<IUiHandler> _uiHandlers = new();

    public Image InfoPanelBg;

    public void Initialize()
    {
        CreateHandlers();
    }

    void CreateHandlers()
    {
        ManaUiHandler = new(ManaUiHandlerData);
        LocationInfoUiHandler = new(LocationInfoUiHandlerData);
        PointUiHandler = new(PointUiHandlerData);
        TurnUiHandler = new(TurnUiHandlerData);      
        StarMover = new(StarMoverData, this);
        HandUiManager = new(HandUiData);
        DeckUiHandler = new(DeckUiData);
        DiscardPileUiHandler = new(DiscardPileUiData);
        AnimationSpeedUiHandler = new(AnimationSpeedUiHandlerData);

        _uiHandlers.Add(ManaUiHandler);
        _uiHandlers.Add(LocationInfoUiHandler);
        _uiHandlers.Add(PointUiHandler);
        _uiHandlers.Add(TurnUiHandler);
        _uiHandlers.Add(StarMover);
        _uiHandlers.Add(HandUiManager); 
        _uiHandlers.Add(DeckUiHandler);
        _uiHandlers.Add(DiscardPileUiHandler);
        _uiHandlers.Add(AnimationSpeedUiHandler);
    }

    public void HideGameplayUi()
    {
        foreach(IUiHandler handler in _uiHandlers) handler.SetState(false);
        InfoPanelBg.gameObject.SetActive(false);
    }

    public void ShowGameplayUi()
    {
        InfoPanelBg.gameObject.SetActive(true);
        foreach(IUiHandler handler in _uiHandlers) handler.SetState(true);
    }

    public void RunRoutine(IEnumerator routine) => StartCoroutine(routine);
    public void KillRoutine(IEnumerator routine) => StopCoroutine(routine);
}