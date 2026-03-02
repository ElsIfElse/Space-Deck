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

    public void Initialize()
    {
        RuntimeCards = new List<BaseCardData>();
        foreach (BaseCardData card in PlayerCards)
        {
            BaseCardData runtimeCard = Instantiate(card); 
            runtimeCard.name = card.name;                 
            RuntimeCards.Add(runtimeCard);
        }
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