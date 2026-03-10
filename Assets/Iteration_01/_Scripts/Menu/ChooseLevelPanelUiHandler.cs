using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelPanelUiHandler : IUiHandler
{
    List<MapData> _mapDataList = new List<MapData>();
    List<Button> _buttonList = new();
    GameObject _buttonPrefab;
    GameObject _buttonParentObj;

    Dictionary<Button,Map> _mapButtonDictionary = new Dictionary<Button,Map>();

    Map _choosenMap; public Map ChoosenMap => _choosenMap;
    Sprite _lockedMapSprite;
    Sprite _buttonSprite;
    GameObject _chooseLevelPanel;
    TextMeshProUGUI _choosenMapNameText;

    Button _backToCardsButton;
    Button _startGameButton;

    ViewChangeHandler _viewChangeHandler;
    MenuCardUpgradeUiHandler _menuCardUpgradeUiHandler;
    
    public ChooseLevelPanelUiHandler(ChooseLevelPanelUiHanderData data,MenuCardUpgradeUiHandler menuCardUpgradeUiHandler, ViewChangeHandler viewChangeHandler)
    {
        _viewChangeHandler = viewChangeHandler;
        _menuCardUpgradeUiHandler = menuCardUpgradeUiHandler;

        _mapDataList = data.MapDataList;
        _buttonPrefab = data.ButtonPrefab;
        _chooseLevelPanel = data.ChooseLevelPanel;
        _lockedMapSprite = data.LockedMapSprite;
        _buttonSprite = data.ButtonSprite;
        _choosenMapNameText = data.ChoosenMapNameText;

        _backToCardsButton = data.BackToCardsButton;
        _startGameButton = data.StartGameButton;

        _buttonParentObj = data.ButtonParentObj;

        CreateMapObjects();

        _backToCardsButton.onClick.AddListener(HandleBackToCardsButtonClick);
        _startGameButton.onClick.AddListener(HandleStartGameButtonClick);
    }
    void CreateMapObjects()
    {
        for (int i = 0; i < _mapDataList.Count; i++)
        {
            Map map = CreateMapObject(_mapDataList[i],i);
            Button button = CreateButton(i,map); 
            _mapButtonDictionary.Add(button,map);
        }
    }
    
    Image GetImageOnMapButton(Button button)
    {
        foreach(Transform child in button.transform)
        {
            if(child.TryGetComponent(out Image img))
            {
                return img;
            }
        }

        Debug.LogError("Image not found on map button");
        return null;
    }
    Button CreateButton(int index,Map map)
    {
        GameObject btnObj = MenuManager.Instance.CreateGameObject(_buttonPrefab,$"Map Choosing Button [{index}]");
        Button btn = btnObj.GetComponent<Button>();

        Image image = GetImageOnMapButton(btn);
        Material btnMat = image.material; 
        Material newMat = new Material(btnMat);
        image.material = newMat;   
        image.sprite = map.MapSprite;

        btn.transform.SetParent(_buttonParentObj.transform,false);

        btn.onClick.AddListener(() => HandleMapChoosingButtonClick(btn,map));
        return btn;
    }
    void HandleMapChoosingButtonClick(Button btn,Map map)
    {
        HighlightButton(btn);
        _choosenMap = map;
        SetChoosenMapText();
    }
    void SetButtonBasedOnUnlockState()
    {
        foreach(Button button in _mapButtonDictionary.Keys)
        {
            Image img = GetImageOnMapButton(button);
            Map map = _mapButtonDictionary[button];

            if(map.IsMapLocked)
            {
                img.sprite = _lockedMapSprite;

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => HandleUnlockedButtonClick(button));
                // button.image.sprite = null;
                ChangeMaterialToTransparent(true,button.image);

                continue;
            }

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => HandleMapChoosingButtonClick(button,map));
            // button.image.sprite = _buttonSprite;
            ChangeMaterialToTransparent(false,button.image);

            img.sprite = map.MapSprite;          
        }
    }

    void ChangeMaterialToTransparent(bool shouldItBeTransparent, Image image)
    {
        if(shouldItBeTransparent)
        {
            Color color = image.color;
            color.a = 0f;
            image.color = color;
        }
        else
        {
            Color color = image.color;
            color.a = 1f;
            image.color = color;
        }
    }
    void HandleUnlockedButtonClick(Button button)
    {
        AudioManager.Instance.Play(AudioType.CantDoThat);
    }
    void DeHighlightButtons()
    {
        foreach(Button button in _mapButtonDictionary.Keys) DeHighlightButton(button);
    }
    void DeHighlightButton(Button button)
    {
        GetImageOnMapButton(button).material.color = Color.white;
    }
    void HighlightButton(Button button)
    {
        foreach(Button btn in _mapButtonDictionary.Keys)
        {
            if(btn != button) DeHighlightButton(btn);
        }

        GetImageOnMapButton(button).material.color = Color.blueViolet;
    }
    void SetChoosenMapText()
    {
        if(_choosenMap == null) _choosenMapNameText.text = "<color=red>No map choosen</color>";
        else _choosenMapNameText.text = _choosenMap.MapName;
    }
    public void SetState(bool state)
    {
        if(state)
        {
            MenuManager.Instance.MenuSlotHandler.HideMenuSlots();
            _chooseLevelPanel.SetActive(true);
            SetButtonBasedOnUnlockState();
            DeHighlightButtons();
            SetChoosenMap(null);
            SetChoosenMapText();
        }
        else
        {
            _chooseLevelPanel.SetActive(false);
        }
    }
    public void SetChoosenMap(Map map) => _choosenMap = map;
    public void SetChoosenMapFromData(MapData mapData) => _choosenMap = CreateMapObject(mapData,0);

    Map CreateMapObject(MapData data, int mapIndex)
    {
        Map map = new(
            mapName: data.MapName,
            targetPoints: data.TargetPoints,
            score: data.Score,
            mapSprite: data.MapSprite,
            isMapLocked: data.IsMapLocked
        );
        map.MapIndex = mapIndex;
        return map;
    }
    void HandleStartGameButtonClick()
    {
        if(_choosenMap == null)
        {   
            _choosenMapNameText.text = "<color=red>No map choosen</color>";
            return;
        }

        MenuUiManager.Instance.StartRoutine(GameStateManager.Instance.ChangeState(GameStateEnum.GamePlay));
    }
    void HandleBackToCardsButtonClick()
    {
        AudioManager.Instance.Play(AudioType.Click,0,true);
        SetState(false);
        _viewChangeHandler.SetState(true);
        MenuUiManager.Instance.ChooseLevelUiHandler.SetState(true);
        MenuManager.Instance.MenuSlotHandler.ShowMenuSlots(true);
    }
}

[Serializable]
public struct ChooseLevelPanelUiHanderData
{
    public Button BackToCardsButton;
    public Button StartGameButton;

    public GameObject ButtonPrefab;
    public GameObject ChooseLevelPanel;
    public GameObject ButtonParentObj;

    public List<MapData> MapDataList;
    public Sprite LockedMapSprite;
    public Sprite ButtonSprite;
    public TextMeshProUGUI ChoosenMapNameText;
}