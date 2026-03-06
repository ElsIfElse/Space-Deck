using System;

[Serializable]
public class HanderooSaveData
{
    public int Id;
    public int CardValue;
    public int CardCost;
    
    public int UpgradeAmount_01;
    public int UpgradeCost_01; 

    public int AdditionalCardsToBeDrawn;

    public bool IsCardLocked;
    public int UnlockCost;
}