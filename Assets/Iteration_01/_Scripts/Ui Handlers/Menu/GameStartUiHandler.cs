using System;
using UnityEngine;
using UnityEngine.UI;

public class GameStartUiHandler : IUiHandler
{

    GameObject _gameStartPanel;
    Button _chooseMapButton; 

    public GameStartUiHandler(GameStartUiHandlerData gameStartUiHandlerData)
    {
        _gameStartPanel = gameStartUiHandlerData.GameStartPanel;
        _chooseMapButton = gameStartUiHandlerData.ChooseMapButton; 

        // _chooseMapButton.onClick.AddListener(()=>GameStateManager.Instance.ChangeState(GameStateEnum.GamePlay));
    }

    public void SetState(bool state)
    {
        if(state)
        {
            _gameStartPanel.SetActive(true);
        }
        else
        {
            _gameStartPanel.SetActive(false);
        }
    }
}

[Serializable]
public struct GameStartUiHandlerData
{
    public GameObject GameStartPanel;
    public Button ChooseMapButton; 

}