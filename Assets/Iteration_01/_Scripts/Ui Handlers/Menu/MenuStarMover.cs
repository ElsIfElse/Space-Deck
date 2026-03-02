using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MenuStarMover : IUiHandler

{
    Image _bgImage;
    public float rotationSpeed = 160f;
    Tween _tween;

    public MenuStarMover(MenuStarMoverData data, MenuUiManager uiManager)
    {
        _bgImage = data.BgImage;
        MoveBg();
    }
    public void MoveBg()
    {
        _bgImage.gameObject.SetActive(true);
       _tween = _bgImage.transform.DORotate(new Vector3(0, 0, 360), rotationSpeed, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }

    public void StopBg()
    {
        _tween?.Kill();
        _bgImage.gameObject.SetActive(false);
    }

    public void SetState(bool state)
    {
        if(state)
        {
            MoveBg();
        }
        else
        {
            StopBg();
        }
    }
}

[Serializable]
public struct MenuStarMoverData
{
    public Image BgImage;
}