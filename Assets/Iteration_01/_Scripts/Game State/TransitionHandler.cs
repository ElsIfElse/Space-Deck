using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using VolFx;

public class TransitionHandler
{
    Image _blackFadeImg;
    float _effectFadeTime;
    float _effectStayTime;

    Vector3 _originalPosition;
    float _zooomingInValue;
    CinemachineCamera _camera;
    CinemachineVolumeSettings _volume;
    PixelationVol _pixelation;

    GameObject _onWinPanel;
    GameObject _onLosePanel;
    TextMeshProUGUI _loseText;
    TextMeshProUGUI _winText;

    SpaceQuotes _quotes;


    public TransitionHandler(TransitionHandlerData data)
    {
        _quotes = new SpaceQuotes();

        _effectFadeTime = data.EffectFadeTime;
        _effectStayTime = data.EffectStayTime;
        
        _loseText = data.LoseText;
        _winText = data.WinText;
        _onWinPanel = data.OnWinPanel;
        _onLosePanel = data.OnLosePanel;

        _zooomingInValue = data.ZoomingInValue;
        
        _blackFadeImg = data.BlackFadeImg;
        _camera = data.Camera;
        _volume = _camera.gameObject.GetComponent<CinemachineVolumeSettings>();

        GetPixelation();
    }  

    public IEnumerator CameraEffect_On()
    {
        DOTween.To(()=> _pixelation.m_Scale.value, x => _pixelation.m_Scale.value = x, 0.5f, _effectFadeTime);
        _camera.transform.DOMove(_camera.transform.position + Vector3.forward * _zooomingInValue, _effectFadeTime);
        _blackFadeImg.DOFade(1,_effectFadeTime);

        yield return new WaitForSeconds(_effectStayTime + _effectFadeTime);
    }
    public IEnumerator CameraEffect_Off()
    {
        DOTween.To(()=> _pixelation.m_Scale.value, x => _pixelation.m_Scale.value = x, 1, _effectFadeTime);
        _camera.transform.DOMove(_originalPosition, _effectFadeTime);
        _blackFadeImg.DOFade(0,_effectFadeTime);

        yield return new WaitForSeconds(_effectStayTime + _effectFadeTime);
    }

    void GetPixelation()
    {
        if (_volume.Profile.TryGet<PixelationVol>(out _pixelation))
        {
            
        }
        else 
        {
            Debug.Log("Pixelation not found");
        }
    }

    void SetWinLoseText()
    {
        
    }

    void SetWinPanel(bool state)=> _onWinPanel.SetActive(state);
    void SetLosePanel(bool state) => _onLosePanel.SetActive(state);
    public void SetAllPanelState(bool state)
    {
        _onWinPanel.SetActive(state);
        _onLosePanel.SetActive(state);
    }

    public IEnumerator OnGameLost()
    {
        yield return GameStateManager.Instance.StartCoroutine(CameraEffect_On());
        _loseText.text = $"<i>{_quotes.RandomQuote()}</i> \n\n\n\n\nYou gain <sprite index=0> <size=30>{MenuManager.Instance.CurrencyHandler.CurrencyCount_Primary()}";
        SetLosePanel(true);
        yield return GameStateManager.Instance.StartCoroutine(CameraEffect_Off());
    }

    public IEnumerator OnGameWon()
    {
        yield return GameStateManager.Instance.StartCoroutine(CameraEffect_On());
        _winText.text = $"Nice job! Now <b><size=40>Crimson Dust</size></b> is unlocked \n\n\n\n\nYou gain <sprite index=0> <size=30>{MenuManager.Instance.CurrencyHandler.CurrencyCount_Primary()}";
        SetWinPanel(true);
        yield return GameStateManager.Instance.StartCoroutine(CameraEffect_Off());
    }
}

    [Serializable]
    public struct TransitionHandlerData
    {
        public Image BlackFadeImg;
        public CinemachineCamera Camera;
        public GameObject OnWinPanel;
        public GameObject OnLosePanel;
        public TextMeshProUGUI WinText;
        public TextMeshProUGUI LoseText;

        public float ZoomingInValue;
        public float EffectFadeTime;
        public float EffectStayTime;

    }  