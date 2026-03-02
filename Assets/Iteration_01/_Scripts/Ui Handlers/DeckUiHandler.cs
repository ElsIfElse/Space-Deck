using System;
using TMPro;
using UnityEngine;

public class DeckUiHandler : IUiHandler
{
    GameObject _deckPanel;
    TextMeshPro _decCounterText;

    public DeckUiHandler(DeckUiData data)
    {
        _deckPanel = data.DeckPanel;
        _decCounterText = data.DecCounterText;
    }

    public void SetState(bool state)
    {
        if (state)
        {
            _deckPanel.SetActive(true);
        }
        else
        {
            _deckPanel.SetActive(false);
        }
    }

    public void UpdateTextCounterText(int amount)
    {
        _decCounterText.text = amount.ToString();
    }
}


[Serializable]
public class DeckUiData
{
    public GameObject DeckPanel;
    public TextMeshPro DecCounterText;
}