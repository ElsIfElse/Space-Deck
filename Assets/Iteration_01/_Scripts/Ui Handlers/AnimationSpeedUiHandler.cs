using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimationSpeedUiHandler : IUiHandler
{
    GameObject _changeAnimationSpeedPanel;
    Button _changeAnimationSpeedButton;
    TextMeshProUGUI _changeAnimationSpeedButtonText;

    public AnimationSpeedUiHandler(AnimationSpeedUiHandlerData data)
    {
        _changeAnimationSpeedButton = data.ChangeAnimationSpeedButton;
        _changeAnimationSpeedPanel = data.ChangeAnimationSpeedPanel;
        _changeAnimationSpeedButtonText = data.ChangeAnimationSpeedButton.GetComponentInChildren<TextMeshProUGUI>();
        _changeAnimationSpeedButton.onClick.AddListener(GameStateManager.Instance.GlobalValues.ChangeAnimationSpeed);

        SetAnimationSpeedButtonText("1");
    }

    public void SetAnimationSpeedButtonText(string text)
    {
        _changeAnimationSpeedButtonText.text = text + "x";
    }

    public void SetState(bool state)
    {
        if (state)
        {
            _changeAnimationSpeedPanel.SetActive(true);
        }
        else
        {
            _changeAnimationSpeedPanel.SetActive(false);
        }
    }
}

[Serializable]
public struct AnimationSpeedUiHandlerData
{
    public Button ChangeAnimationSpeedButton;
    public GameObject ChangeAnimationSpeedPanel;
}
