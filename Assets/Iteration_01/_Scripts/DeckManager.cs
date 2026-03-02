using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Transform DeckPosition; 
    public List<Card> CardsInDeck = new();
    public int DeckCount() => CardsInDeck.Count;

    public Card RemoveTopCard()
    {
        if(DeckCount() == 0) return null;
        Card topCard = CardsInDeck[0];
        CardsInDeck.RemoveAt(0);
        GameplayUiManager.Instance.DeckUiHandler.UpdateTextCounterText(DeckCount());
        return topCard;
    }  
    public void AddCardToDeck(Card card)
    {
        CardsInDeck.Add(card);
        GameplayUiManager.Instance.DeckUiHandler.UpdateTextCounterText(DeckCount());
    }

    public void ShuffleDeck()
    {
        for(int i = 0; i < DeckCount(); i++)
        {
            Card tempCard = CardsInDeck[i];
            int randomNum = Random.Range(i,DeckCount());
            CardsInDeck[i] = CardsInDeck[randomNum];
            CardsInDeck[randomNum] = tempCard;
        }
    }

    public void RemoveAndDestroyAllCardsInDeck()
    {
        if(DeckCount() == 0) return;
        foreach(Card card in CardsInDeck)
        {
            Destroy(card.gameObject);
        }
        CardsInDeck.Clear();
        GameplayUiManager.Instance.DeckUiHandler.UpdateTextCounterText(DeckCount());
    }
}
