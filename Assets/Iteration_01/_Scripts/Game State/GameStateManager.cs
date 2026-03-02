using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    #region Simpleton Init
    public static GameStateManager Instance { get; private set; }
    
    public void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }
    #endregion

    GameState _currentState;
    Dictionary<GameStateEnum, GameState> _states = new();

    GameplayState _gameplayState;
    MenuState _menuState;

    public GlobalValues GlobalValues;
    public bool IsCheatOn = true;
    
    void Cheat()
    {
        if(IsCheatOn) MenuManager.Instance.CurrencyHandler.AddCurrency_Primary(20);
    }

    void OnGameStart()
    {
        CreateSubHandlers();
        Initialize_SingletonManagers();
        Initialize_GameStateManager(); 
        Cheat();
    }
    void Start()
    {
        OnGameStart();
    }

    void Initialize_GameStateManager()
    {
        CreateStates();
        AddStatesToDictionary();
        InitializeStates();

        _gameplayState.OnExit();
        ChangeState(GameStateEnum.Menu);
    }
    void Initialize_SingletonManagers()
    {
        PlayerDeckHandler.Instance.Initialize();
        GameplayUiManager.Instance.Initialize();
        MenuUiManager.Instance.Initialize();
        MenuManager.Instance.Initialize();
        AudioManager.Instance.Initialize();
    }
    public void ChangeState(GameStateEnum newState)
    {
        _currentState?.OnExit();
        _currentState = _states[newState];
        _currentState.OnEnter();
    }

    void InitializeStates()
    {   
        foreach(GameStateEnum state in System.Enum.GetValues(typeof(GameStateEnum))) _states[state].Initialize(this);
    }

    void CreateStates()
    {
        _gameplayState = new();
        _menuState = new();
    }

    void CreateSubHandlers()
    {
        GlobalValues = new();
    }
    void AddStatesToDictionary()
    {
        _states.Add(GameStateEnum.GamePlay, _gameplayState);
        _states.Add(GameStateEnum.Menu, _menuState);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0,0,200,50),"Animation Speed: " + GlobalValues.AnimationSpeed);
    }
}