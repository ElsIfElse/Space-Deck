using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System;
public class TutorialUiHandler
{
    private GameObject _tutorialPanelCollectorObj;

    Button _playTutorialButton;
    Button _dontPlayTutorialButton;

    int _currentPanelIndex = 0;
    MapData _tutorialMap;

    private List<GameObject> _panelList_01 = new();
    private List<GameObject> _panelList_02 = new();
    GameplayUiManager _gameplayUiManager;
    public bool IsFirstListDone;

    public TutorialUiHandler(TutorialUiManagerData data, GameplayUiManager gameplayUiManager)
    {
        _tutorialPanelCollectorObj = data._tutorialPanelCollectorObj;

        _panelList_01 = data.PanelList_01;
        _panelList_02 = data.PanelList_02;

        _tutorialMap = data.TutorialMap;

        _gameplayUiManager = gameplayUiManager;

        _playTutorialButton = data.PlayTutorialButton;
        _dontPlayTutorialButton = data.DontPlayTutorialButton;

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
            if(_currentPanelIndex == _panelList_01.Count)
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
            if(_currentPanelIndex == _panelList_02.Count)
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
        if(!IsFirstListDone) foreach(GameObject panel in _panelList_01) panel.SetActive(false);
        else foreach(GameObject panel in _panelList_02) panel.SetActive(false);
    }
    public void EnablePanel(int index)
    {
        if(!IsFirstListDone) _panelList_01[index].SetActive(true);
        else _panelList_02[index].SetActive(true);
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

    public List<GameObject> PanelList_01;
    public List<GameObject> PanelList_02;

    public Button PlayTutorialButton;
    public Button DontPlayTutorialButton;
    public MapData TutorialMap;
}