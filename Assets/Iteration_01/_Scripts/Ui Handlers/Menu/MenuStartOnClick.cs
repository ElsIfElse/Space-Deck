using UnityEngine;
using UnityEngine.EventSystems;

public class MenuStarOnClick : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        MenuUiManager.Instance.MenuCardUpgradeUiHandler.SetState(false);
        MenuManager.Instance.MenuSlotHandler.TurnOffSlotLight();
        MenuUiManager.Instance.SettingsMenuHandler.SetState(false);
    }
}