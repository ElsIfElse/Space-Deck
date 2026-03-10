using System.Collections;
using UnityEngine;

public class MenuState : GameState
{
    public override void OnEnter()
    {   
        MenuManager.Instance.CurrencyHandler.AddCurrency_Primary(PointCounterManager.Instance.CurrentPoints / 5);
        PointCounterManager.Instance.ResetPoints();
        SaveManager.Instance.SaveData();
        AudioManager.Instance.PlayScore(AudioType.Score_01) ;
        MenuManager.Instance.MenuSlotHandler.ShowMenuSlots(true);
        MenuUiManager.Instance.ShowMenuUi();
        
        GameStateManager.Instance.StartCoroutine(GameStateManager.Instance.TransitionHandler.CameraEffect_Off());
    }

    public override IEnumerator OnExit()
    {
        yield return GameStateManager.Instance.StartCoroutine(GameStateManager.Instance.TransitionHandler.CameraEffect_On());
        MenuManager.Instance.MenuSlotHandler.HideMenuSlots();
        MenuManager.Instance.MenuSlotHandler.TurnOffSlotLight();
        MenuUiManager.Instance.HideMenuUi();
    }

    public override void Tick()
    {
        
    }
}