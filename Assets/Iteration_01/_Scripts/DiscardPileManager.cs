using System.Collections.Generic;
using UnityEngine;

public class DiscardPileManager : MonoBehaviour
{
    public List<Card> _discardedPileList = new(); public List<Card> DiscardedPileList => _discardedPileList;

    public void AddCardToDisardedPile(Card card)
    {
        _discardedPileList.Add(card);
        GameplayUiManager.Instance.DiscardPileUiHandler.UpdateDiscardPileCountText(DeckCount());
    }
    public int DeckCount() => _discardedPileList.Count;
    public Card RemoveCardFromDiscardedPile()
    {
        Card cardToRemove = _discardedPileList[0];
        _discardedPileList.RemoveAt(0);
        GameplayUiManager.Instance.DiscardPileUiHandler.UpdateDiscardPileCountText(DeckCount());
        return cardToRemove;
    }

    public void ShuffleDeck()
    {
        for(int i = 0; i < DeckCount(); i++)
        {
            Card tempCard = _discardedPileList[i];
            int randomNum = Random.Range(i,DeckCount());
            _discardedPileList[i] = _discardedPileList[randomNum];
            _discardedPileList[randomNum] = tempCard;
        }
    }
    public int DiscardedCardPileCount() => _discardedPileList.Count;

    public void RemoveAndDestroyAllCardsInDiscardedPile()
    {
        if(DeckCount() == 0) return;
        
        foreach(Card card in _discardedPileList)
        {
            Destroy(card.gameObject);
        }
        
        _discardedPileList.Clear();
    }
}