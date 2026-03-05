using System.Collections.Generic;
using UnityEngine;

public class PlayerDeckHandler : MonoBehaviour
{
    #region Singleton Init
    public static PlayerDeckHandler Instance;
    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }
    #endregion

    public List<BaseCardData> PlayerCards; 
    public List<BaseCardData> RuntimeCards;

    public List<BaseCardData> LockedCards;
    public List<BaseCardData> RuntimeLockedCards;

    public void Initialize()
    {
        RuntimeCards = new List<BaseCardData>();
        foreach (BaseCardData card in PlayerCards)
        {
            BaseCardData runtimeCard = Instantiate(card); 
            runtimeCard.name = card.name;                 
            RuntimeCards.Add(runtimeCard);
        }

        RuntimeLockedCards = new List<BaseCardData>();
        foreach (BaseCardData card in LockedCards)
        {
            BaseCardData runtimeCard = Instantiate(card); 
            runtimeCard.name = card.name;                 
            RuntimeLockedCards.Add(runtimeCard);
        }
    }

    public void AddCardToDeck(BaseCardData card) => RuntimeCards.Add(card);
    public void MoveCardFromLockedToDeck(BaseCardData card)
    {
        RuntimeLockedCards.Remove(card);
        RuntimeCards.Add(card);
    }

    void OnDestroy()
    {
        // Clean up cloned assets to avoid memory leaks
        foreach (BaseCardData card in RuntimeCards)
        {
            Destroy(card);
        }
    }

} 