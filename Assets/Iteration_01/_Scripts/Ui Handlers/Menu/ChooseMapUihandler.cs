using System;
using UnityEngine.UI;
using UnityEngine;

public class ChooseMapUihandler : IUiHandler
{
    private GameObject _mapChooseingPanel;
    private Button _mapBtn_01;
    private Button _mapBtn_02;
    private Button _mapBtn_03;
    private Button _startGameButton;
    private Button _backFromChoosingButton;

    public ChooseMapUihandler(ChooseMapUiHandlerData chooseMapUiHandlerData)
    {
        _mapChooseingPanel = chooseMapUiHandlerData.MapChooseingPanel;
        _mapBtn_01 = chooseMapUiHandlerData.MapBtn_01;
        _mapBtn_02 = chooseMapUiHandlerData.MapBtn_02;
        _mapBtn_03 = chooseMapUiHandlerData.MapBtn_03;
        _startGameButton = chooseMapUiHandlerData.StartGameButton;
        _backFromChoosingButton = chooseMapUiHandlerData.BackFromChoosingButton;
    }

    public void SetState(bool state)
    {
        if(state)
        {
            _mapChooseingPanel.SetActive(true);
        }
        else
        {
            _mapChooseingPanel.SetActive(false);
        }
    }
}

[Serializable]
public struct ChooseMapUiHandlerData
{
    public GameObject MapChooseingPanel;
    public Button MapBtn_01;
    public Button MapBtn_02;
    public Button MapBtn_03;

    public Button StartGameButton;
    public Button BackFromChoosingButton;
}