using System;
using System.Collections.Generic;

[Serializable]
public class SavedDataClass
{
    public int PrimaryCurrency;
    public int SecondaryCurrency;
    public int TotalUpgradesPurchased;
    public bool HasTutorialBeenPlayed;

    public List<TyniroSaveData> TyniroSaveDatas = new List<TyniroSaveData>();
    public List<MediumoSaveData> MediumoSaveDatas = new List<MediumoSaveData>();
    
    public DiscardoSaveData DiscardoSaveData;
    public DuppoSaveData DuppoSaveData;
    public ForgeroSaveData ForgeroSaveData;
    public GainoSaveData GainoSaveData;
    public GroweroSaveData GroweroSaveData;
    public HanderooSaveData HanderooSaveData;
    public PlayedoSaveData PlayedoSaveData;
    public OlForgieSaveData OlForgieSaveData;
    public MorcardelSaveData MorcardelSaveData;
}