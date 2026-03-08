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
    float _blackFadeTime = 0.3f;
    float _cameraEffectTime = 0.3f;
    public float TransitionTime
    {
        get
        {
            return _blackFadeTime + _cameraEffectTime;
        }
    } 
    Vector3 _originalPosition;
    float _zooomingInValue;
    CinemachineCamera _camera;
    CinemachineVolumeSettings _volume;
    PixelationVol _pixelation;

    

    public TransitionHandler(TransitionHandlerData data)
    {
        _blackFadeImg = data.BlackFadeImg;
        _camera = data.Camera;
        _volume = _camera.gameObject.GetComponent<CinemachineVolumeSettings>();
        GetPixelation();
    }
    public IEnumerator OnTransition()
    {
        Debug.Log("Transitioning..");
        UseCameraEffect(true);
        _camera.transform.DOMove(_camera.transform.position + Vector3.forward * _zooomingInValue, _cameraEffectTime / 2 + _blackFadeTime);
        _blackFadeImg.DOFade(1,_blackFadeTime);

        yield return new WaitForSeconds(_cameraEffectTime + _blackFadeTime);

        _camera.transform.DOMove(_originalPosition, _cameraEffectTime / 2);
        _blackFadeImg.DOFade(0,_blackFadeTime);
        UseCameraEffect(false);
        Debug.Log("Transition done!");
    }

    void UseCameraEffect(bool effectState)
    {
        if(effectState)
        {
            DOTween.To(()=> _pixelation.m_Scale.value, x => _pixelation.m_Scale.value = x, 0.5f, _cameraEffectTime);
        }
        else
        {
            DOTween.To(()=> _pixelation.m_Scale.value, x => _pixelation.m_Scale.value = x, 1, _cameraEffectTime);
        }
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
}

    [Serializable]
    public struct TransitionHandlerData
    {
        public Image BlackFadeImg;
        public CinemachineCamera Camera;
    }  