using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnUiHandler : IUiHandler
{
    public TextMeshProUGUI _turnCounterText;
    Button _endTurnButton;
    Image _turnPanelBg;

    public TurnUiHandler(TurnUiHandlerData data)
    {
        _turnCounterText = data.TurnCounterText;
        _endTurnButton = data.EndTurnButton;
        _turnPanelBg = data.TurnPanelBg;

        _endTurnButton.onClick.AddListener(ActionManager.Instance.OnEndTurn);
    }

    public void UpdateTurnCounter(int currentTurn, int maxTurn)
    {
        _turnCounterText.text = $"Current Turn: {currentTurn}/{maxTurn}";
    }

    public void SetState(bool state)
    {
        if(state)
        {
            _turnCounterText.gameObject.SetActive(true);
            _endTurnButton.gameObject.SetActive(true);
            _turnPanelBg.gameObject.SetActive(true);
            
        }
        else
        {
            _turnCounterText.gameObject.SetActive(false);
            _endTurnButton.gameObject.SetActive(false);
            _turnPanelBg.gameObject.SetActive(false);
        }
    }
}

[Serializable]
public struct TurnUiHandlerData
{
    public TextMeshProUGUI TurnCounterText;
    public Button EndTurnButton;
    public Image TurnPanelBg;
}