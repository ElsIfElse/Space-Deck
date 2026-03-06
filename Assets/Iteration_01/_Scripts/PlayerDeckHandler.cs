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

    public List<BaseCardData> AllCards;
    public List<BaseCardData> RuntimeAllCards;

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

    public void OnFirstLoad()
    {
        RuntimeAllCards = new List<BaseCardData>();
        RuntimeCards = new List<BaseCardData>();
        RuntimeLockedCards = new List<BaseCardData>();

        foreach (BaseCardData card in AllCards)
        {
            BaseCardData runtimeCard = Instantiate(card);
            runtimeCard.name = card.name;
            RuntimeAllCards.Add(runtimeCard);
        }

        AddCardsToDecks();
    }

    public void OnLoadingSavedData(SavedDataClass data)
    {
        // Instantiate all cards first
        RuntimeAllCards = new List<BaseCardData>();
        foreach(BaseCardData card in AllCards)
        {
            BaseCardData runtimeCard = Instantiate(card);
            runtimeCard.name = card.name;
            RuntimeAllCards.Add(runtimeCard);
        }

        int tyniroIndex = 0;
        int mediumoIndex = 0;
        
        // Apply saved data onto runtime instances
        foreach(BaseCardData card in RuntimeAllCards)
        { 
            switch(card.CardType)
            {
                case CardType.Forgero:   card.LoadData(data); break;
                case CardType.Morcardel: card.LoadData(data); break;
                case CardType.OlForgie:  card.LoadData(data); break;
                case CardType.Duppo:     card.LoadData(data); break;
                case CardType.Handeroo:  card.LoadData(data); break;
                case CardType.Playedo:   card.LoadData(data); break;
                case CardType.Discardo:  card.LoadData(data); break;
                case CardType.Growero:   card.LoadData(data); break;
                case CardType.Gaino:     card.LoadData(data); break;

            case CardType.Tyniro:    card.LoadData(data, tyniroIndex++);  break;
            case CardType.Mediumo:   card.LoadData(data, mediumoIndex++); break;
            }
        }

        // Sort into correct lists based on restored IsCardLocked
        AddCardsToDecks();
    }

    void AddCardsToDecks()
    {
        RuntimeCards = new List<BaseCardData>();
        RuntimeLockedCards = new List<BaseCardData>();

        foreach(BaseCardData card in RuntimeAllCards)
        {
            if(card.IsCardLocked) RuntimeLockedCards.Add(card);
            else RuntimeCards.Add(card);
        }
    }

    public void AddCardToDeck(BaseCardData card) => RuntimeCards.Add(card);
    
    public void MoveCardFromLockedToDeck(BaseCardData card)
    {
        RuntimeLockedCards.Remove(card);
        RuntimeCards.Add(card);
    }

    public BaseCardData GetCard(CardType cardType)
    {
        foreach(BaseCardData card in RuntimeCards)
            if(card.CardType == cardType) return card;

        foreach(BaseCardData card in RuntimeLockedCards)
            if(card.CardType == cardType) return card;

        return null;
    }

    void OnDestroy()
    {
        foreach (BaseCardData card in RuntimeAllCards)
            Destroy(card);
    }
}