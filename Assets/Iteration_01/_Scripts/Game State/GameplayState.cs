using System.Collections;
using UnityEngine;

public class GameplayState : GameState
{
    public override void OnEnter()
    {
        Map currentMap = MenuUiManager.Instance.ChooseLevelPanelUiHandler.ChoosenMap;
        GameplayUiManager.Instance.ShowGameplayUi();
        GameplayUiManager.Instance.StarMover.SetImageSprite(currentMap.MapSprite);
        ActionManager.Instance.StartGame();

        GameStateManager.Instance.StartCoroutine(GameStateManager.Instance.TransitionHandler.CameraEffect_Off());
    }

    public override IEnumerator OnExit()
    {
        Debug.Log("Exiting Gameplay State");
        GameplayUiManager.Instance.HideGameplayUi();
        ActionManager.Instance.RemoveAllActiveCardsFromScene();
        yield return null;
    }

    public override void Tick()
    {
        
    }
}