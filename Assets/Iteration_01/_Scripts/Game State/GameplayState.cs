using System.Collections;
using UnityEngine;

public class GameplayState : GameState
{
    public override void OnEnter()
    {
        GameplayUiManager.Instance.ShowGameplayUi();
        ActionManager.Instance.StartGame();
    }

    public override void OnExit()
    {
        GameplayUiManager.Instance.HideGameplayUi();
        ActionManager.Instance.RemoveAllActiveCardsFromScene();
    }

    public override void Tick()
    {
        
    }
}