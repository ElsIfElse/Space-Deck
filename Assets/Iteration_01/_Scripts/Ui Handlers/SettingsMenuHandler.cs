using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsMenuHandler : IUiHandler
{
    GameObject _settingsMenuPanel;
    Button _settingsMenuButton;
    Slider _musicVolumeSlider;
    Slider _sfxVolumeSlider;

    public SettingsMenuHandler(SettingsMenuHandlerData settingsMenuHandlerData)
    {
        _settingsMenuPanel = settingsMenuHandlerData.SettingsMenuPanel;
        _settingsMenuButton = settingsMenuHandlerData.SettingsMenuButton;
        _musicVolumeSlider = settingsMenuHandlerData.MusicVolumeSlider;
        _sfxVolumeSlider = settingsMenuHandlerData.SfxVolumeSlider;

        _settingsMenuButton.onClick.AddListener(HandleSettingsMenuClick);

        _sfxVolumeSlider.onValueChanged.AddListener(AudioManager.Instance.SetSfxVolume);
        _musicVolumeSlider.onValueChanged.AddListener(AudioManager.Instance.SetScoreVolume);
    }

    public void SetState(bool state)
    {
        if (state)
        {
            _settingsMenuPanel.SetActive(true);
        }
        else
        {
            _settingsMenuPanel.SetActive(false);
        }
    }    

    void HandleSettingsMenuClick()
    {
        AudioManager.Instance.Play(AudioType.Click);
        SetState(!_settingsMenuPanel.activeSelf);
    }

    public void SetMusicSlider(float value)
    {
        _musicVolumeSlider.value = value;
    }

    public float GetMusicSliderValue()
    {
        return _musicVolumeSlider.value;
    }

    public void SetSfxSlider(float value)
    {
        _sfxVolumeSlider.value = value;
    }

    public float GetSfxSliderValue()
    {
        return _sfxVolumeSlider.value;
    }
}

[Serializable]
public struct SettingsMenuHandlerData
{
    public GameObject SettingsMenuPanel;
    public Button SettingsMenuButton;
    public Slider MusicVolumeSlider;
    public Slider SfxVolumeSlider;
}