using System.Collections.Generic;
using UnityEngine;

public class GlobalValues
{
    float _animationSpeed = 1; public float AnimationSpeed => _animationSpeed;
    int _animationSpeedIndex = 0;

    List<float> _animationSpeeds = new();

    public GlobalValues()
    {
        _animationSpeeds.Add(1);
        _animationSpeeds.Add(1.5f);
        _animationSpeeds.Add(2);
        _animationSpeeds.Add(3);
    }

    public void ChangeAnimationSpeed()
    {
        _animationSpeedIndex++;
        if(_animationSpeedIndex >= _animationSpeeds.Count) _animationSpeedIndex = 0;
        _animationSpeed = _animationSpeeds[_animationSpeedIndex];
        GameplayUiManager.Instance.AnimationSpeedUiHandler.SetAnimationSpeedButtonText(_animationSpeed.ToString());
    }
}