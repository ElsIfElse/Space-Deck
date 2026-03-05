using System.Collections;
using UnityEngine;

public class MenuState : GameState
{
    public override void OnEnter()
    {
        AudioManager.Instance.PlayScore(AudioType.Score_01) ;
        MenuManager.Instance.MenuSlotHandler.ShowMenuSlots(true);
        MenuUiManager.Instance.ShowMenuUi();
    }

    public override IEnumerator OnExit()
    {
        _gameStateManager.StartCoroutine(_gameStateManager.TransitionHandler.OnTransition());
        yield return new WaitForSeconds(_gameStateManager.TransitionHandler.TransitionTime);
        MenuManager.Instance.MenuSlotHandler.HideMenuSlots();
        MenuUiManager.Instance.HideMenuUi();
    }

    public override void Tick()
    {
        
    }
}