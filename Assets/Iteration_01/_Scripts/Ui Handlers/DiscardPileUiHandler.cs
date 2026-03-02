using System;
using TMPro;
using UnityEngine;

public class DiscardPileUiHandler : IUiHandler
{
    GameObject _discardPilePanel;
    TextMeshPro _discardPileCountText;

    public DiscardPileUiHandler(DiscardPileUiData data)
    {
        _discardPilePanel = data.DiscardPilePanel;
        _discardPileCountText = data.DiscardPileCountText;
    }

    public void SetState(bool state)
    {
        if (state)
        {
            _discardPileCountText.text = "0";
            _discardPilePanel.SetActive(true);
        }
        else
        {
            _discardPileCountText.text = "0";
            _discardPilePanel.SetActive(false);
        }
    }

    public void UpdateDiscardPileCountText(int amount)
    {
        _discardPileCountText.text = amount.ToString();
    }
}


[Serializable]
public class DiscardPileUiData
{
    public GameObject DiscardPilePanel;
    public TextMeshPro DiscardPileCountText;
}