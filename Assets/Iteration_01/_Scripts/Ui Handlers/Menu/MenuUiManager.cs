using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public ViewChangeHandler ViewChangeHandler;
    public ViewChangeHandlerData ViewChangeHandlerData;

    public ChooseLevelPanelUiHandler ChooseLevelPanelUiHandler;
    public ChooseLevelPanelUiHanderData ChooseLevelPanelUiHanderData;


    List<IUiHandler> _subhandlers = new();

    public void Initialize()
    {
        CreateHandlers();

        SettingsMenuHandler.SetState(false);
        ChooseLevelPanelUiHandler.SetState(false);
    }
    void CreateHandlers()
    {
        MenuStarMover = new(MenuStarMoverData, this);
        MenuCardDetailUiHandler = new(MenuCardDetailUiHandlerData);
        MenuCardUpgradeUiHandler = new(MenuCardUpgradeUiHandlerData);
        MenuCurrencyUiHandler = new(MenuCurrencyUiData);
        GameStartUiHandler = new(GameStartUiHandlerData);
        SettingsMenuHandler = new(SettingsMenuHandlerData);
        ViewChangeHandler = new(ViewChangeHandlerData, MenuManager.Instance.MenuSlotHandler,MenuCardUpgradeUiHandler);
        ChooseLevelPanelUiHandler = new(ChooseLevelPanelUiHanderData,MenuCardUpgradeUiHandler,ViewChangeHandler);
        ChooseLevelUiHandler = new(ChooseLevelHandlerData,ViewChangeHandler,MenuCardUpgradeUiHandler,ChooseLevelPanelUiHandler);

        _subhandlers.Add(MenuCurrencyUiHandler);
        _subhandlers.Add(GameStartUiHandler);
        _subhandlers.Add(MenuStarMover);
        _subhandlers.Add(ViewChangeHandler);
        _subhandlers.Add(ChooseLevelUiHandler);
        // _subhandlers.Add(ChooseLevelPanelUiHandler);
    }

    public void HideMenuUi()
    {
        foreach(IUiHandler handler in _subhandlers) handler.SetState(false);

        MenuCardDetailUiHandler.SetState(false);
        MenuCardUpgradeUiHandler.SetState(false);
        SettingsMenuHandler.SetState(false);  
        ChooseLevelPanelUiHandler.SetState(false);  
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