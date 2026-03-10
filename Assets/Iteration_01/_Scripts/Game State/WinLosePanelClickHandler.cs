using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class WinLoseClickHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameStateManager.Instance.StartCoroutine(OnClick());
    }

    IEnumerator OnClick()
    {
        yield return GameStateManager.Instance.StartCoroutine(GameStateManager.Instance.TransitionHandler.CameraEffect_On());
        GameStateManager.Instance.TransitionHandler.SetAllPanelState(false);
        GameStateManager.Instance.StartCoroutine(GameStateManager.Instance.ChangeState(GameStateEnum.Menu)); 
    }
}