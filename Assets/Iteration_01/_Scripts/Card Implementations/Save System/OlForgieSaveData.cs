using System;

[Serializable]
public class OlForgieSaveData
{
    public int Id;
    public int CardValue;
    public int CardCost;
    
    public int UpgradeAmount_01;
    public int UpgradeCost_01; 

    public bool IsCardLocked;
    public int UnlockCost;
    public int ValueUpgradeAmount;
}