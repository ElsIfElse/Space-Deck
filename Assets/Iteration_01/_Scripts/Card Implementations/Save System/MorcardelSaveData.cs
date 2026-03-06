using System;

[Serializable]
public class MorcardelSaveData
{
    public int Id;
    public int CardValue;
    public int CardCost;
    
    public int UpgradeAmount_01;
    public int UpgradeCost_01; 

    public int AmountOfCardsToBeDrawn;

    public bool CanUseUpgrade_01;
}