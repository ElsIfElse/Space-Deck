using System;
using UnityEngine;

public class HandUiManager : IUiHandler
{
    GameObject _handPanel;

    public HandUiManager(HandUiData data)
    {
        _handPanel = data.HandPanel;
    }

    public void SetState(bool state)
    {
        if (state)
        {
            _handPanel.SetActive(true);
        }
        else
        {
            _handPanel.SetActive(false);
        }
    }
}

[Serializable]
public class HandUiData
{
    public GameObject HandPanel; 
}