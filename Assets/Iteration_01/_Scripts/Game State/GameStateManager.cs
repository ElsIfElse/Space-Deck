using System.Collections;
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
    public TransitionHandler TransitionHandler;
    public TransitionHandlerData TransitionHandlerData;
    public GlobalValues GlobalValues;

    #region Save/Load
    void LoadSaveData()
    {
        SavedDataClass data = SaveManager.Instance.LoadData();

        if(data == null)
        {
            PlayerDeckHandler.Instance.OnFirstLoad();
            MenuManager.Instance.CurrencyHandler.OnFirstLoad();
            Debug.Log("No data found. First Load.");

        }
        else
        {
            MenuManager.Instance.CurrencyHandler.OnLoadSaveData(data);
            PlayerDeckHandler.Instance.OnLoadingSavedData(data);
            Debug.Log("Data Loaded!");
        }

        MenuManager.Instance.MenuSlotHandler.SetMenuSlots(true);
    }
    #endregion
    void Cheat()
    {
        MenuManager.Instance.CurrencyHandler.AddCurrency_Primary(20);
    }

    void OnGameStart()
    {
        CreateSubHandlers();
        Initialize_SingletonManagers();
        Initialize_GameStateManager(); 
        Cheat();
    }
    IEnumerator Start()
    {
        OnGameStart();
        yield return new WaitForSeconds(0.05f);
        LoadSaveData();
    }

    void Initialize_GameStateManager()
    {
        CreateStates();
        AddStatesToDictionary();
        InitializeStates();

        StartCoroutine(_gameplayState.OnExit());
        StartCoroutine(ChangeState(GameStateEnum.Menu));
    }
    void Initialize_SingletonManagers()
    {
        PlayerDeckHandler.Instance.Initialize();
        GameplayUiManager.Instance.Initialize();
        MenuUiManager.Instance.Initialize();
        MenuManager.Instance.Initialize();
        AudioManager.Instance.Initialize();
    }
    public IEnumerator ChangeState(GameStateEnum newState)
    {
        if(_currentState != null)
        {
            yield return StartCoroutine(_currentState.OnExit());
        }
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
        TransitionHandler = new(TransitionHandlerData);
    }
    void AddStatesToDictionary()
    {
        _states.Add(GameStateEnum.GamePlay, _gameplayState);
        _states.Add(GameStateEnum.Menu, _menuState);
    }

    void OnGUI()
    {
        if(GUI.Button(new Rect(0, 0, 100, 30), "Save"))
        {
            SaveManager.Instance.SaveData();
        }

        if(GUI.Button(new Rect(0, 30, 100, 30), "Delete Save Data"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("Save data cleared.");
        }
        if(GUI.Button(new Rect(0, 60, 100, 30), "Cheat"))
        {
            Cheat();
        }
    }
}