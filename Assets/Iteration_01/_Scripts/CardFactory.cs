using TMPro;
using UnityEngine;

public class CardFactory : MonoBehaviour
{
    #region Simpleton Init
    public static CardFactory Instance;
    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }
    #endregion

    public GameObject CardPrefab;

    public GameObject CreateNewCard()
    {
        GameObject newCard = Instantiate(CardPrefab);
        return newCard;
    }
}