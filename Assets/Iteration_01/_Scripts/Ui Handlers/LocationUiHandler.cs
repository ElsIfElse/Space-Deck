using System;
using TMPro;
using UnityEngine.UI;

public class LocationInfoUiHandler : IUiHandler
{
    TextMeshProUGUI _locationText;
    public LocationInfoUiHandler(LocationInfoUiHandlerData data)
    {
        _locationText = data.LocationText;
    }

    public void SetState(bool state)
    {
        if(state)
        {
            _locationText.gameObject.SetActive(true);
        }
        else
        {
            _locationText.gameObject.SetActive(false);
        }
    }

    public void SetLocationText(string text)
    {
        _locationText.text = text;
    }

    public void Initialize()
    {
        SetLocationText(MenuUiManager.Instance.ChooseLevelPanelUiHandler.ChoosenMap.MapName);
    }
}

[Serializable]
public struct LocationInfoUiHandlerData
{
    public TextMeshProUGUI LocationText;
}