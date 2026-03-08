using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System;
public class TutorialUiHandler
{
    private GameObject _tutorialPanelCollectorObj;
    private GameObject _panel01;
    private GameObject _panel02;
    private GameObject _panel03;

    private GameObject _panel_02_01;

    Button _playTutorialButton;
    Button _dontPlayTutorialButton;

    int _currentPanelIndex = 0;
    MapData _tutorialMap;

    private List<GameObject> _panels_01;
    private List<GameObject> _panels_02;
    GameplayUiManager _gameplayUiManager;
    public bool IsFirstListDone;

    public TutorialUiHandler(TutorialUiManagerData data, GameplayUiManager gameplayUiManager)
    {
        _tutorialPanelCollectorObj = data._tutorialPanelCollectorObj;
        _panel01 = data._panel01_01;
        _panel02 = data._panel01_02;
        _panel03 = data._panel01_03;

        _panel_02_01 = data._panel02_01;

        _tutorialMap = data.TutorialMap;

        _gameplayUiManager = gameplayUiManager;

        _playTutorialButton = data.PlayTutorialButton;
        _dontPlayTutorialButton = data.DontPlayTutorialButton;

        _panels_01 = new List<GameObject>
        {
            _panel01,
            _panel02,
            _panel03
        };

        _panels_02 = new List<GameObject>
        {
            _panel_02_01
        };

        _playTutorialButton.onClick.AddListener(HandlePlayTutorialButtonPress);
        _dontPlayTutorialButton.onClick.AddListener(HandleDontPlayTutorialButtonPress);
    }

    private void HandleDontPlayTutorialButtonPress()
    {
        _tutorialPanelCollectorObj.SetActive(false);
    }

    void HandlePlayTutorialButtonPress()
    {
        MenuUiManager.Instance.ChooseLevelUiHandler.SetChoosenMapData(_tutorialMap);
        GameplayUiManager.Instance.RunRoutine(GameStateManager.Instance.ChangeState(GameStateEnum.GamePlay));
        ShowNextPanel();
    }

    public void ShowNextPanel()
    {
        ActionManager.Instance.SetcanInteract(false); 

        if(!IsFirstListDone)
        {
            if(_currentPanelIndex == _panels_01.Count)
            {
                OnLastPanel();
                return;
            } 
            _tutorialPanelCollectorObj.SetActive(true);
            DisableAllPanels();
            EnablePanel(_currentPanelIndex);
            IncreaseCurrentPanelIndex();  
        }
        else
        {
            if(_currentPanelIndex == _panels_02.Count)
            {
                OnLastPanel();
                return;
            } 
            _tutorialPanelCollectorObj.SetActive(true);
            DisableAllPanels();
            EnablePanel(_currentPanelIndex);
            IncreaseCurrentPanelIndex();
        }
    }

    public void DisableAllPanels() 
    {
        foreach(GameObject panel in _panels_01) panel.SetActive(false);
    }
    public void EnablePanel(int index)
    {
        if(!IsFirstListDone) _panels_01[index].SetActive(true);
        else _panels_02[index].SetActive(true);
    }
    public void DisableTutorialPanelCollectorObj()
    {
        DisableAllPanels();
        _tutorialPanelCollectorObj.SetActive(false);
    }
    public void IncreaseCurrentPanelIndex() => _currentPanelIndex++;
    public void OnLastPanel()
    {
        if(IsFirstListDone) GameStateManager.Instance.HasTutorialBeenPlayed = true;
        ActionManager.Instance.SetcanInteract(true);
        Debug.Log("Last panel reached.");
        DisableTutorialPanelCollectorObj();
        _currentPanelIndex = 0;
        IsFirstListDone = true;
    }
}

[Serializable]
public struct TutorialUiManagerData
{
    public GameObject _tutorialPanelCollectorObj;
    public GameObject _panel01_01;
    public GameObject _panel01_02;
    public GameObject _panel01_03;

    public GameObject _panel02_01;

    public Button PlayTutorialButton;
    public Button DontPlayTutorialButton;
    public MapData TutorialMap;
}