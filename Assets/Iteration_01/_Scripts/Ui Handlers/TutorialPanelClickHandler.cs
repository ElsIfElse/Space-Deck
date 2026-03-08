using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Panel Clicked");
        GameplayUiManager.Instance.TutorialUiHandler.ShowNextPanel();
    }
}