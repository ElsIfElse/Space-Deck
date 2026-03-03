using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

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

    public float DeckToSlotMoveTime {get{return 1f / GameStateManager.Instance.GlobalValues.AnimationSpeed;}}
    public float HandToFieldMoveTime {get{return 1f / GameStateManager.Instance.GlobalValues.AnimationSpeed;}}
    public float FieldToDiscardPileMoveTime {get{return 1f / GameStateManager.Instance.GlobalValues.AnimationSpeed;}}
    public float HandToDiscardPileMoveTime {get{return 1f / GameStateManager.Instance.GlobalValues.AnimationSpeed;}}
    public float DiscardPileToDeckMoveTime {get{return 0.75f / GameStateManager.Instance.GlobalValues.AnimationSpeed;}}

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

    public void MoveCardToSlotPosition(Card cardToMove,GameplayCardSlot slot)
    {
        cardToMove.gameObject.transform.DOMove(slot.gameObject.transform.position,DeckToSlotMoveTime).SetEase(Ease.InOutSine);
        cardToMove.gameObject.transform.DORotate(slot.originalRotation,DeckToSlotMoveTime);

    }
    public void MoveCardFromHandToField(Card cardToMove)
    {
        cardToMove.gameObject.transform.DOMove(_cardPositions[CardPositionType.Field].position,HandToFieldMoveTime).SetEase(Ease.InOutSine);
        cardToMove.gameObject.transform.DORotate(Vector3.zero,HandToFieldMoveTime);
    }
    public void MoveCardFromFieldToDiscardPile(Card cardToMove,int offsetCount)
    {
        Vector3 targetPosition = _cardPositions[CardPositionType.Discard].position+new Vector3(offsetCount*0.01f,0,-offsetCount*0.05f);
        cardToMove.gameObject.transform.DOMove(targetPosition,FieldToDiscardPileMoveTime).SetEase(Ease.InOutSine);
        cardToMove.gameObject.transform.DORotate(DiscardPileRotation,FieldToDiscardPileMoveTime/2);
    }
    public void MoveCardFromHandToDiscardPile(Card cardToMove, int offsetCount)
    {
        Vector3 targetPosition = _cardPositions[CardPositionType.Discard].position+new Vector3(offsetCount*0.01f,0,-offsetCount*0.05f);
        cardToMove.gameObject.transform.DOMove(targetPosition,HandToDiscardPileMoveTime).SetEase(Ease.InOutSine);
        cardToMove.gameObject.transform.DORotate(DiscardPileRotation,HandToDiscardPileMoveTime/2);
    }
    public void MoveCardFromDiscardPileToDeck(Card cardToMove, int cardCount)
    {
        Vector3 targetPosition = _cardPositions[CardPositionType.Deck].position+new Vector3(cardCount*0.01f,0,cardCount*0.05f);
        cardToMove.gameObject.transform.DOMove(targetPosition,DiscardPileToDeckMoveTime).SetEase(Ease.InOutSine);
        cardToMove.gameObject.transform.DORotate(DeckRotation,DiscardPileToDeckMoveTime);
    }

    void Start()
    {
        CreateDictionary_Position();
        CreateDictionary_Rotation();
    }

}