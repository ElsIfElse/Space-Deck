using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelUiHandler : IUiHandler
{

    Button _toMapChoiceButton;
    ViewChangeHandler _viewChangeHandler;
    MenuCardUpgradeUiHandler _menuCardUpgradeUiHandler;
    ChooseLevelPanelUiHandler _chooseLevelPanelUiHandler;



    public ChooseLevelUiHandler(ChooseLevelHandlerData data, ViewChangeHandler viewChangeHandler, MenuCardUpgradeUiHandler menuCardUpgradeUiHandler, ChooseLevelPanelUiHandler chooseLevelPanelUiHandler)
    {
        _viewChangeHandler = viewChangeHandler;
        _menuCardUpgradeUiHandler = menuCardUpgradeUiHandler;
        _chooseLevelPanelUiHandler = chooseLevelPanelUiHandler;

        _toMapChoiceButton = data.ToMapChoiceButton;
        _toMapChoiceButton.onClick.AddListener(HandleChooseMapButtonClick);
    }

    void HandleChooseMapButtonClick()
    {
        AudioManager.Instance.Play(AudioType.Click,0,true);
        _chooseLevelPanelUiHandler.SetState(true);
        _viewChangeHandler.SetState(false);
        MenuUiManager.Instance.ChooseLevelPanelUiHandler.SetState(true);
    }


    public void SetState(bool state)
    {
        if(state)
        {
            _toMapChoiceButton.gameObject.SetActive(true);
        }
        else
        {
            _toMapChoiceButton.gameObject.SetActive(false);
        }
    }

    // public void DeselectButtons()
    // {
    //     _map01_ColorTween?.Kill();
    //     _map02_ColorTween?.Kill();

    //     // _map01_ColorTween = _chooseMapButton01.image.material.DOColor(Color.white,_fadeTime);
    //     // _map02_ColorTween = _chooseMapButton02.image.material.DOColor(Color.white,_fadeTime);

    //     _chooseMapButton01Image.material.DOColor(Color.white,_fadeTime);
    //     _chooseMapButton02Image.material.DOColor(Color.white,_fadeTime);

    //     _choosenMap = null;
    // }
}

[Serializable]
public struct ChooseLevelHandlerData
{
    public Button ToMapChoiceButton;
}