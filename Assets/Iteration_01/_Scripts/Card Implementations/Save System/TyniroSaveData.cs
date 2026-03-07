using System;
using System.Collections.Generic;

[Serializable]
public class TyniroSaveData
{
    public int Id;
    public int CardValue;
    public int CardCost;
    
    public int UpgradeAmount_01;
    public int UpgradeCost_01; 

    public bool IsSecondUpgradeUnlocked;
    public List<Upgrade> Upgrades;
    public string Effect_01_Description;
}