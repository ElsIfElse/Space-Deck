
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StarUiHandler : IUiHandler
{
    public Image StarImg;
    public Image StarFrameImg;
    public float rotationSpeed = 120f;
    public float scaleSpeed = 10f;
    GameplayUiManager _uiManager;
    Tween _scaleTween;
    Tween _rotateTween;
    IEnumerator _scaleRoutine;

    public StarUiHandler(StarMoverData data, GameplayUiManager uiManager)
    {
        StarImg = data.StarImg;
        _uiManager = uiManager;
        StarFrameImg = data.StarFrameImg;
    }

    void RotateStars()
    {
        _rotateTween = StarImg.transform.DORotate(new Vector3(0, 0, 360), rotationSpeed, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }

    IEnumerator SetStarScale(GameplayUiManager uiManager)
    {
        _scaleTween = StarImg.transform.DOScale(1.25f, rotationSpeed).SetEase(Ease.Linear);
        yield return new WaitForSeconds(rotationSpeed);
        _scaleTween = StarImg.transform.DOScale(1f, rotationSpeed/1.1f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(rotationSpeed/1.1f);
        uiManager.RunRoutine(SetStarScale(uiManager));
    }

    public void StartMovement()
    {
        RotateStars();

        _scaleRoutine = SetStarScale(_uiManager);
        _uiManager.RunRoutine(_scaleRoutine);   
    }

    public void StopMovement()
    {
        _scaleTween?.Kill();
        _rotateTween?.Kill();

        if(_scaleRoutine != null )_uiManager.StopCoroutine(_scaleRoutine);
        _scaleRoutine = null;
    }

    public void HideStars()
    {
        StopMovement();
        StarImg.gameObject.SetActive(false);
        StarFrameImg.gameObject.SetActive(false);
    }

    public void ShowStars()
    {
        StarImg.gameObject.SetActive(true);
        StarFrameImg.gameObject.SetActive(true);
        StartMovement();
    }

    public void SetState(bool state)
    {
        if (state)
        {
            ShowStars();
        }
        else
        {
            HideStars();
        }
    }
}

[Serializable]
public struct StarMoverData
{
    public Image StarImg;
    public Image StarFrameImg;
}