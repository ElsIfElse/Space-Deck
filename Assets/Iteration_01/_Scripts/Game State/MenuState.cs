using System.Collections;
using UnityEngine;

public class MenuState : GameState
{
    public override void OnEnter()
    {
        AudioManager.Instance.PlayScore(AudioType.Score_01) ;
        MenuManager.Instance.MenuSlotHandler.ShowMenuSlots();
        MenuUiManager.Instance.ShowMenuUi();
    }

    public override void OnExit()
    {
        MenuManager.Instance.MenuSlotHandler.HideMenuSlots();
        MenuUiManager.Instance.HideMenuUi();
    }

    public override void Tick()
    {
        
    }
}