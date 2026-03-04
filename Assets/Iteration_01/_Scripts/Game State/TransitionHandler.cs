using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class TransitionHandler
{
    Image _blackFadeImg;
    float _blackFadeTime = 0.5f;
    CinemachineCamera _camera;
    float _cameraEffectTime = 0.5f;
    public float TransitionTime = 1f; 

    public TransitionHandler(TransitionHandlerData data)
    {
        _blackFadeImg = data.BlackFadeImg;
        _camera = data.Camera;
    }
    public IEnumerator OnTransition()
    {
        UseCameraEffect(true);
        yield return new WaitForSeconds(_cameraEffectTime / 2);

        _blackFadeImg.DOFade(1,_blackFadeTime);
        yield return new WaitForSeconds(0.5f);

        _blackFadeImg.DOFade(0,_blackFadeTime);
        yield return new WaitForSeconds(_cameraEffectTime / 2);

        UseCameraEffect(false);
    }

    void UseCameraEffect(bool effectState)
    {
        if(effectState)
        {
            // Turn on effect
        }
        else
        {
            // Turn off effect
        }
    }   
}

    [Serializable]
    public struct TransitionHandlerData
    {
        public Image BlackFadeImg;
        public CinemachineCamera Camera;
    }  