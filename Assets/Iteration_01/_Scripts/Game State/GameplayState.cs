using System.Collections;
using UnityEngine;

public class GameplayState : GameState
{
    public override void OnEnter()
    {
        GameplayUiManager.Instance.ShowGameplayUi();
        ActionManager.Instance.StartGame();
    }

    public override IEnumerator OnExit()
    {
        Debug.Log("Exiting Gameplay State");
        _gameStateManager.StartCoroutine(_gameStateManager.TransitionHandler.OnTransition());
        yield return new WaitForSeconds(_gameStateManager.TransitionHandler.TransitionTime);
        GameplayUiManager.Instance.HideGameplayUi();
        ActionManager.Instance.RemoveAllActiveCardsFromScene();
    }

    public override void Tick()
    {
        
    }
}