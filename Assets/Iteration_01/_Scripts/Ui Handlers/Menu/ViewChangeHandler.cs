using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewChangeHandler : IUiHandler
{
    GameObject _viewChangePanel;
    Button _viewChangeButton;
    TextMeshProUGUI _buttonText;
    MenuSlotHandler _menuSlotHandler;
    MenuCardUpgradeUiHandler _menuCardUpgradeUiHandler;
    bool _currentViewState = true;

    public ViewChangeHandler(ViewChangeHandlerData data, MenuSlotHandler menuSlotHandler, MenuCardUpgradeUiHandler menuCardUpgradeUiHandler)
    {
        _viewChangePanel = data.ViewChangePanel;
        _viewChangeButton = data.ViewChangeButton;
        _menuSlotHandler = menuSlotHandler;
        _menuCardUpgradeUiHandler = menuCardUpgradeUiHandler;

        _buttonText = _viewChangeButton.transform.GetComponentInChildren<TextMeshProUGUI>();

        _viewChangeButton.onClick.AddListener(HandleViewChangeButtonClick);
    }

    public void SetState(bool state)
    {
        if (state)
        {
            _viewChangePanel.SetActive(true);
            _currentViewState = true;
        }
        else
        {
            _viewChangePanel.SetActive(false);
            _currentViewState = false;
        }
    }

    public void HandleViewChangeButtonClick()
    {
        _menuSlotHandler ??= MenuManager.Instance.MenuSlotHandler;
        _menuSlotHandler.SetMenuSlots(!_currentViewState);
        _currentViewState = !_currentViewState;
        _buttonText.text = _currentViewState ? "Unlock Cards" : "View Deck";
        _menuCardUpgradeUiHandler.SetState(false);
    }
}

[Serializable]
public struct ViewChangeHandlerData
{
    public GameObject ViewChangePanel;
    public Button ViewChangeButton;
}