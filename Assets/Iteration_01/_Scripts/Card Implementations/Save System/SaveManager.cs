using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using UnityEngine;
using UnityEngine.Events;

public class SaveManager : MonoBehaviour
{
    #region Singleton
    public static SaveManager Instance;
    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }
    #endregion

    string SAVE_KEY = "SpaceDeck_Save";
    
    public void SaveData()
    {
        Debug.Log("Saving data..");

        List<TyniroSaveData> tyniroList = new List<TyniroSaveData>();
        List<MediumoSaveData> mediumoList = new List<MediumoSaveData>();
        

        foreach(BaseCardData card in PlayerDeckHandler.Instance.RuntimeCards)
        {
            if(card.CardType == CardType.Tyniro)
            {
                tyniroList.Add((card as Tyniro).GetSaveData());
            }
        }
        foreach(BaseCardData card in PlayerDeckHandler.Instance.RuntimeCards)
        {
            if(card.CardType == CardType.Mediumo)
            {
                mediumoList.Add((card as Mediumo).GetSaveData());
            }
        }

        // Create save class object
        SavedDataClass dataToSave = new()
        {
            // Add data to object
            PrimaryCurrency = MenuManager.Instance.CurrencyHandler.CurrencyCount_Primary(),
            SecondaryCurrency = MenuManager.Instance.CurrencyHandler.CurrencyCount_Secondary(),
            TotalPrimaryCurrencySpent = MenuManager.Instance.CurrencyHandler.TotalPrimaryCurrencySpentCount(),
            
            DiscardoSaveData = (PlayerDeckHandler.Instance.GetCard(CardType.Discardo) as Discardo).GetSaveData(),
            DuppoSaveData = (PlayerDeckHandler.Instance.GetCard(CardType.Duppo) as Duppo).GetSaveData(),
            ForgeroSaveData = (PlayerDeckHandler.Instance.GetCard(CardType.Forgero) as Forger).GetSaveData(),
            GainoSaveData = (PlayerDeckHandler.Instance.GetCard(CardType.Gaino) as Gaino).GetSaveData(),
            GroweroSaveData = (PlayerDeckHandler.Instance.GetCard(CardType.Growero) as Growero).GetSaveData(),
            HanderooSaveData = (PlayerDeckHandler.Instance.GetCard(CardType.Handeroo) as Handeroo).GetSaveData(),
            PlayedoSaveData = (PlayerDeckHandler.Instance.GetCard(CardType.Playedo) as Playedo).GetSaveData(),
            OlForgieSaveData = (PlayerDeckHandler.Instance.GetCard(CardType.OlForgie) as OlForgie).GetSaveData(),
            MorcardelSaveData = (PlayerDeckHandler.Instance.GetCard(CardType.Morcardel) as Morcardel).GetSaveData(),

            TyniroSaveDatas = tyniroList,
            MediumoSaveDatas = mediumoList, 
        };

        // Create JSON
        string jsonData = JsonUtility.ToJson(dataToSave);

        // Save
        PlayerPrefs.SetString(SAVE_KEY, jsonData);
        PlayerPrefs.Save();

        // Indicate success by finding key or not
        if(PlayerPrefs.HasKey(SAVE_KEY)) Debug.Log("Data Saved!");
        else Debug.Log("Failed to save data!");
    }

    public SavedDataClass LoadData()
    {
        Debug.Log("Loading data..");

        if(!PlayerPrefs.HasKey(SAVE_KEY))
        {
            Debug.Log("No data found with key!");
            return null;
        }

        // Get json data
        string jsonData = PlayerPrefs.GetString(SAVE_KEY);

        // Create savedata object from it
        SavedDataClass data = JsonUtility.FromJson<SavedDataClass>(jsonData);
        return data;
    }

    public void OnNoDataFound()
    {
        
    }
}