using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CardMover : MonoBehaviour
{
    #region Simpleton Init
    public static CardMover Instance;

    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }
    #endregion

    public Dictionary<CardPositionType,Transform> _cardPositions = new();
    public Dictionary<CardPositionType,Vector3> _cardRotations = new();

    public Transform DeckPosition;
    public Transform HandPosition;
    public Transform FieldPosition;
    public Transform DiscardPilePosition;

    public Vector3 HandRotation;
    public Vector3 DeckRotation;
    public Vector3 FieldRotation;
    public Vector3 DiscardPileRotation;

    HandManager HandManager;

    void CreateDictionary_Position()
    {
        _cardPositions[CardPositionType.Deck] = DeckPosition.transform;
        _cardPositions[CardPositionType.Hand] = HandPosition.transform;
        _cardPositions[CardPositionType.Field] = FieldPosition.transform;
        _cardPositions[CardPositionType.Discard] = DiscardPilePosition.transform;
    }
    void CreateDictionary_Rotation()
    {
        _cardRotations[CardPositionType.Deck] = DeckRotation;
        _cardRotations[CardPositionType.Hand] = HandRotation;
        _cardRotations[CardPositionType.Field] = FieldRotation;
        _cardRotations[CardPositionType.Discard] = DiscardPileRotation;
    }

    // public CardPositionType MoveCard(CardPositionType currentPosition,Transform transform)
    // {
    //     if(currentPosition == CardPositionType.Deck)
    //     {
    //         DeckToHand(transform,CardPositionType.Hand);
    //         return CardPositionType.Hand;
    //     }
    //     else if(currentPosition == CardPositionType.Hand)
    //     {
    //         HandToField(transform,CardPositionType.Field);
    //         return CardPositionType.Field;
    //     }
    //     else if(currentPosition == CardPositionType.Field)
    //     {
    //         FieldToDiscardPile(transform,CardPositionType.Discard);
    //         return CardPositionType.Discard;
    //     }
    //     else
    //     {
    //         DiscardPileToDeck(transform,CardPositionType.Deck);
    //         return CardPositionType.Deck;
    //     }
    // }
    public void MoveCard(Transform moveTo,Transform cardToMove,CardPositionType cardPositionType, float cardCount = 0)
    {
        // cardToMove.DOMove(_cardPositions[cardPositionType].position+new Vector3(cardCount*0.01f,0,cardCount*0.05f),1 / GameStateManager.Instance.GlobalValues.AnimationSpeed).SetEase(Ease.InOutSine);
        cardToMove.DOMove(moveTo.position+new Vector3(cardCount*0.01f,0,cardCount*0.05f),1 / GameStateManager.Instance.GlobalValues.AnimationSpeed).SetEase(Ease.InOutSine);
        cardToMove.DORotate(_cardRotations[cardPositionType],0.5f / GameStateManager.Instance.GlobalValues.AnimationSpeed);
    }

    public void MoveCardToHand(GameplayCardSlot slot,Transform cardToMove)
    {
        cardToMove.DOMove(slot.originalPosition,1 / GameStateManager.Instance.GlobalValues.AnimationSpeed).SetEase(Ease.InOutSine).OnComplete(() => cardToMove.transform.localPosition = Vector3.zero);
        cardToMove.DORotate(slot.originalRotation,0.5f / GameStateManager.Instance.GlobalValues.AnimationSpeed);   
    }

    public void DeckToHand(Transform cardTransform,CardPositionType moveToType)
    {
        cardTransform.DOMove(_cardPositions[moveToType].position,1);
        cardTransform.DOLookAt(Camera.main.transform.position,1);
    }

    public void HandToField(Transform cardTransform,CardPositionType moveToType)
    {
        cardTransform.DOMove(_cardPositions[moveToType].position,1);
        cardTransform.DOLookAt(Camera.main.transform.position,1);
    }

    public void FieldToDiscardPile(Transform cardTransform,CardPositionType moveToType)
    {
        cardTransform.DOMove(_cardPositions[moveToType].position,1);
        cardTransform.DOLookAt(Camera.main.transform.position,1);
    }

    public void DiscardPileToDeck(Transform cardTransform,CardPositionType moveToType)
    {
        cardTransform.DOMove(_cardPositions[moveToType].position,1);
        cardTransform.DOLookAt(Camera.main.transform.position,1);
    }

    void Start()
    {
        CreateDictionary_Position();
        CreateDictionary_Rotation();
    }

}