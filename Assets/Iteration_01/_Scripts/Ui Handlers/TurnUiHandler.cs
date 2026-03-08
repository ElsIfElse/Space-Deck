using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnUiHandler : IUiHandler
{
    public TextMeshProUGUI _turnCounterText;
    public TextMeshProUGUI _cardsPlayedThisTurnCounterText;
    Button _endTurnButton;
    Image _turnPanelBg;
    Button _endRunButton;
    GameplayUiManager _gameplayUiManager;

    public TurnUiHandler(TurnUiHandlerData data,GameplayUiManager gameplayUiManager)
    {
        _turnCounterText = data.TurnCounterText;
        _cardsPlayedThisTurnCounterText = data.CardsPlayedThisTurnCounterText;
        _endTurnButton = data.EndTurnButton;
        _turnPanelBg = data.TurnPanelBg;
        _endRunButton = data.EndRunButton;
        _gameplayUiManager = gameplayUiManager;

        _endTurnButton.onClick.AddListener(ActionManager.Instance.OnEndTurn);
        _endRunButton.onClick.AddListener(()=>_gameplayUiManager.RunRoutine(ActionManager.Instance.GameplayEndTurn.OnGameLost()));
    }

    public void UpdateTurnCounter(int currentTurn, int maxTurn)
    {
        _turnCounterText.text = $"Current Turn: {currentTurn}/{maxTurn}";
    }
    public void UpdateCardsPlayedThisTurnCounter(int cardsPlayedThisTurn)
    {
        _cardsPlayedThisTurnCounterText.text = $"Cards played this turn: {cardsPlayedThisTurn}";
    }

    public void SetState(bool state)
    {
        if(state)
        {
            _turnCounterText.gameObject.SetActive(true);
            _endTurnButton.gameObject.SetActive(true);
            _turnPanelBg.gameObject.SetActive(true);
            _cardsPlayedThisTurnCounterText.gameObject.SetActive(true);
            _endRunButton.gameObject.SetActive(true);
            
        }
        else
        {
            _turnCounterText.gameObject.SetActive(false);
            _endTurnButton.gameObject.SetActive(false);
            _turnPanelBg.gameObject.SetActive(false);
            _cardsPlayedThisTurnCounterText.gameObject.SetActive(false);
            _endRunButton.gameObject.SetActive(false);
        }
    }
}

[Serializable]
public struct TurnUiHandlerData
{
    public TextMeshProUGUI TurnCounterText;
    public TextMeshProUGUI CardsPlayedThisTurnCounterText;
    public Button EndTurnButton;
    public Image TurnPanelBg;
    public Button EndRunButton;
}