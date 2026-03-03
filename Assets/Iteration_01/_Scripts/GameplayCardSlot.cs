using System.Diagnostics.Contracts;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameplayCardSlot : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Card CurrentCardInSlot;
    public Transform SlotTransform => transform;
    Tween hoverTween_Upwards;
    Tween hoverTween_Rotation;
    Tween LightTween;
    public float HoverMoveDistance;
    public Vector3 originalPosition;
    public Vector3 originalRotation;
    public Light SlotLight;     
    

    void Start()
    {
        originalPosition = gameObject.transform.position;
        originalRotation = gameObject.transform.rotation.eulerAngles;
    }


    public bool IsSlotEmpty()
    {
        if(CurrentCardInSlot == null) return true;
        return false;
    }

    public Card CardInSlot() => CurrentCardInSlot;
    public void AddCardToSlot(Card card)
    {
        CurrentCardInSlot = card;
    }
    public void RemoveCardFromSlot()
    {
        if(IsSlotEmpty()) return;
        CurrentCardInSlot.transform.SetParent(null,true); 
        CurrentCardInSlot = null;
    }
    
    public void RemoveAndDestroyCardFromSlot()
    {
        if(IsSlotEmpty()) return;
        Card card = CurrentCardInSlot;
        gameObject.transform.position = originalPosition;
        CurrentCardInSlot.transform.SetParent(null,true);  
        CurrentCardInSlot = null;
        Destroy(card.gameObject);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(!ActionManager.Instance.CanInteract) return; 
        if(IsSlotEmpty()) return;
        ResetCardSlotPosition();
        ActionManager.Instance.OnPlayCard(this);    
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(IsSlotEmpty()) return;
        hoverTween_Upwards?.Kill();
        LightTween?.Kill();
        hoverTween_Rotation?.Kill();
        if(IsSlotEmpty()) return;
        LightTween = SlotLight.DOIntensity(18,0.5f);
        if(ActionManager.Instance.CanInteract) hoverTween_Upwards = gameObject.transform.DOMoveY(originalPosition.y + HoverMoveDistance,0.2f);

        if(!ActionManager.Instance.CanInteract) hoverTween_Rotation = gameObject.transform.DORotate(new Vector3(0, 15, 0),0.1f).OnComplete(() => 
        hoverTween_Rotation = gameObject.transform.DORotate(new Vector3(0, -15, 0),0.1f)).OnComplete(() => 
        hoverTween_Rotation = gameObject.transform.DORotate(originalRotation,0.2f));

        CardDetailDisplayer.Instance.ShowDetailDisplay(gameObject.transform,CurrentCardInSlot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverTween_Upwards?.Kill();
        LightTween?.Kill();
        // hoverTween_Rotation?.Kill();
        if(IsSlotEmpty()) return;
        LightTween = SlotLight.DOIntensity(0,0.5f);
        hoverTween_Upwards = gameObject.transform.DOMoveY(originalPosition.y,0.2f);
        CardDetailDisplayer.Instance.HideDetailDisplay();
    }

    public void ResetCardSlotPosition()
    {
        LightTween?.Kill();
        hoverTween_Upwards?.Kill();
        hoverTween_Rotation?.Kill();
        gameObject.transform.rotation = Quaternion.Euler(originalRotation);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,originalPosition.y,gameObject.transform.position.z);
        LightTween = SlotLight.DOIntensity(0,0.5f);
        CardDetailDisplayer.Instance.HideDetailDisplay();
    }

    public void ParentCardInSlot()
    {
        CurrentCardInSlot.gameObject.transform.SetParent(gameObject.transform,true);
    }

    public void SaveNewBasePosition()
    {
        originalPosition = gameObject.transform.position;
    }
} 