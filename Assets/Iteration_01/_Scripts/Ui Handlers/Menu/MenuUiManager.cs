using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUiManager : MonoBehaviour {
    
    #region Singleton Init

    public static MenuUiManager Instance;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }
    #endregion

    public MenuStarMover MenuStarMover;
    public MenuStarMoverData MenuStarMoverData;

    public MenuCardDetailUiHandler MenuCardDetailUiHandler;
    public MenuCardDetailUiHandlerData MenuCardDetailUiHandlerData;

    public MenuCardUpgradeUiHandler MenuCardUpgradeUiHandler;
    public MenuCardUpgradeUiHandlerData MenuCardUpgradeUiHandlerData;

    public MenuCurrencyUiHandler MenuCurrencyUiHandler;
    public MenuCurrencyUiData MenuCurrencyUiData;

    public GameStartUiHandler GameStartUiHandler;
    public GameStartUiHandlerData GameStartUiHandlerData;

    public ChooseLevelUiHandler ChooseLevelUiHandler;
    public ChooseLevelHandlerData ChooseLevelHandlerData;

    public SettingsMenuHandler SettingsMenuHandler;
    public SettingsMenuHandlerData SettingsMenuHandlerData;


    List<IUiHandler> _subhandlers = new();

    public void Initialize()
    {
        CreateHandlers();

        SettingsMenuHandler.SetState(false);
    }
    void CreateHandlers()
    {
        MenuStarMover = new(MenuStarMoverData, this);
        MenuCardDetailUiHandler = new(MenuCardDetailUiHandlerData);
        MenuCardUpgradeUiHandler = new(MenuCardUpgradeUiHandlerData);
        MenuCurrencyUiHandler = new(MenuCurrencyUiData);
        GameStartUiHandler = new(GameStartUiHandlerData);
        ChooseLevelUiHandler = new(ChooseLevelHandlerData, this);
        SettingsMenuHandler = new(SettingsMenuHandlerData);
        

        _subhandlers.Add(MenuCurrencyUiHandler);
        _subhandlers.Add(GameStartUiHandler);
        _subhandlers.Add(MenuStarMover);
        _subhandlers.Add(ChooseLevelUiHandler);
    }

    public void HideMenuUi()
    {
        foreach(IUiHandler handler in _subhandlers) handler.SetState(false);
        MenuCardDetailUiHandler.SetState(false);
        MenuCardUpgradeUiHandler.SetState(false);
        SettingsMenuHandler.SetState(false);    
    }

    public void ShowMenuUi()
    {
        foreach(IUiHandler handler in _subhandlers) handler.SetState(true);
    }

    public void StartRoutine(IEnumerator routine)
    {
        if(routine == null) return;
        StartCoroutine(routine);
    }

    public void KillRoutine(IEnumerator routine)
    {
        if(routine == null) return;
        StopCoroutine(routine);
    }

}