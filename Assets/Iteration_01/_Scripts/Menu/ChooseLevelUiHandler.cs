using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelUiHandler : IUiHandler
{
    MapData _map01;
    MapData _map02; 

    float _fadeTime = 0.1f;

    Button _startGameButton;
    Button _chooseMapButton01;
    Button _chooseMapButton02;
    Button _backToCardsButton;
    Button _toMapChoiceButton;

    TextMeshProUGUI _choosenMapNameText;

    Tween _map01_ColorTween;
    Tween _map02_ColorTween;
    
    List<Button> _mapButtons = new();

    GameObject _chooseMapPanel;
    MenuUiManager _coroutineHelper;
    MapData _choosenMap; public MapData ChoosenMap => _choosenMap;
    ViewChangeHandler _viewChangeHandler;
    MenuCardUpgradeUiHandler _menuCardUpgradeUiHandler;

    public ChooseLevelUiHandler(ChooseLevelHandlerData data, MenuUiManager coroutineHelper, ViewChangeHandler viewChangeHandler, MenuCardUpgradeUiHandler menuCardUpgradeUiHandler)
    {
        _map01 = data.Map01; 
        _map02 = data.Map02; 

        _startGameButton = data.StartGameButton;
        _chooseMapButton01 = data.ChooseMapButton01;
        _chooseMapButton02 = data.ChooseMapButton02;
        _backToCardsButton = data.BackToCardsButton;
        _toMapChoiceButton = data.ToMapChoiceButton;

        _coroutineHelper = coroutineHelper;
        _viewChangeHandler = viewChangeHandler;
        _menuCardUpgradeUiHandler = menuCardUpgradeUiHandler;


        _chooseMapPanel = data.ChooseMapPanel;

        _choosenMapNameText = data.ChoosenMapNameText;
        
        _mapButtons.Add(_chooseMapButton01);
        _mapButtons.Add(_chooseMapButton02); 

        _chooseMapButton01.onClick.AddListener(HandleMapButtonClick_01);
        _chooseMapButton02.onClick.AddListener(HandleMapButtonClick_02);

        _backToCardsButton.onClick.AddListener(HandleBackToCardsButtonClick);
        _toMapChoiceButton.onClick.AddListener(HandleChooseMapButtonClick);

        _startGameButton.onClick.AddListener(HandleStartGameButtonClick);

        _choosenMapNameText.text = "";

        DeselectButtons();
    }

    void HandleStartGameButtonClick()
    {
        if(_choosenMap == null)
        {   
            _choosenMapNameText.text = "<color=red>No map choosen</color>";
            return;
        }

        _coroutineHelper.StartRoutine(GameStateManager.Instance.ChangeState(GameStateEnum.GamePlay));
    }

    public void SetState(bool state)
    {
        if(state)
        {
            _choosenMapNameText.text = "";
            _chooseMapPanel.SetActive(false);
            _toMapChoiceButton.gameObject.SetActive(true);
        }
        else
        {
            _choosenMapNameText.text = "";
            _chooseMapPanel.SetActive(false);
            _toMapChoiceButton.gameObject.SetActive(false);
        }
    }

    void HandleMapButtonClick_01()
    {
        AudioManager.Instance.Play(AudioType.Click,0,true);
        MenuUiManager.Instance.StartRoutine(SelectButton(_chooseMapButton01));
        _choosenMap = _map01;
        _choosenMapNameText.text = _map01.MapName;
    }
    void HandleMapButtonClick_02()
    {
        AudioManager.Instance.Play(AudioType.Click,0,true);
        MenuUiManager.Instance.StartRoutine(SelectButton(_chooseMapButton02));
        _choosenMap = _map02;
        _choosenMapNameText.text = _map02.MapName;
    }

    void HandleChooseMapButtonClick()
    {
        AudioManager.Instance.Play(AudioType.Click,0,true);
        _choosenMapNameText.text = "";
        SetchooseMapPanelState(true);
        _viewChangeHandler.SetState(false);
    }

    void HandleBackToCardsButtonClick()
    {
        AudioManager.Instance.Play(AudioType.Click,0,true);
        _choosenMapNameText.text = "";
        SetchooseMapPanelState(false);
        _viewChangeHandler.SetState(true);
    }

    public void SetchooseMapPanelState(bool state)
    {
        DeselectButtons();

        if(state)
        {
            _chooseMapPanel.SetActive(true);
            _menuCardUpgradeUiHandler.SetState(false); 
            MenuManager.Instance.MenuSlotHandler.HideMenuSlots();
        }
        else
        {
            _chooseMapPanel.SetActive(false);
            MenuManager.Instance.MenuSlotHandler.HideMenuSlots();
            MenuManager.Instance.MenuSlotHandler.SetMenuSlots(true);
        }
    }

    public void DeselectButtons()
    {
        _map01_ColorTween?.Kill();
        _map02_ColorTween?.Kill();

        _map01_ColorTween = _chooseMapButton01.image.material.DOColor(Color.white,_fadeTime);
        _map02_ColorTween = _chooseMapButton02.image.material.DOColor(Color.white,_fadeTime);

        _choosenMap = null;
    }

    IEnumerator SelectButton(Button button)
    {
        DeselectButtons();
        yield return new WaitForSeconds(_fadeTime);
        button.image.material.DOColor(Color.blueViolet,_fadeTime);
    }
}

[Serializable]
public struct ChooseLevelHandlerData
{
    public Button StartGameButton;
    public Button ToMapChoiceButton;
    public Button ChooseMapButton01;
    public Button ChooseMapButton02;
    public Button BackToCardsButton;

    public MapData Map01; 
    public MapData Map02; 

    public TextMeshProUGUI ChoosenMapNameText;
    public GameObject ChooseMapPanel;
}