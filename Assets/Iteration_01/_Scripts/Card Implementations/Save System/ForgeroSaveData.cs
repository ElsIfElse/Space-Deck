using System;
using System.Collections.Generic;

[Serializable]
public class ForgeroSaveData
{
    public int Id;
    public int CardValue;
    public int CardCost;
    
    public int UpgradeAmount_01;
    public int UpgradeAmount_02;

    public int UpgradeCost_01; 
    public int UpgradeCost_02;

    public List<Upgrade> Upgrades = new();

    public string EffectDescription_01;
    public string EffectDescription_02;

    public bool IsSecondUpgradeUnlocked;
}