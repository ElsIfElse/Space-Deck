using System;

[Serializable]
public class Upgrade
{
    public int UpgradeCost;
    public string UpgradeDescription;
    public string UpgradeName;
    public Action UpgradeEffect;
    public CurrencyType CurrencyType;

    public void SetUpgradeCost(int cost) => UpgradeCost = cost;
    public void SetUpgradeName(string name) => UpgradeName = name;
    public void SetUpgradeDescription(string description) => UpgradeDescription = description;
}