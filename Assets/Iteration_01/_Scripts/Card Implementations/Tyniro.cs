using System.Collections;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Tyniro",menuName = "Data/Cards/Tyniro")]
public class Tyniro : BaseCardData
{   
    public bool IsSecondUpgradeUnlocked;
    // Ingame Card Effect
    public override IEnumerator CardEffect(CardVfx cardVfx, Card card = null)
    {
        if(ActionManager.Instance.HandManager.NotEmptySlots().Count == 0 && IsSecondUpgradeUnlocked)
        {
            GainValueSequence(cardVfx,card);
            AudioManager.Instance.Play(AudioType.CardPlayed);
            yield return new WaitForSeconds(1f / GameStateManager.Instance.GlobalValues.AnimationSpeed);
            AudioManager.Instance.Play(AudioType.ManaGained,0,true);
        }

        GainValueSequence(cardVfx,card);
        AudioManager.Instance.Play(AudioType.CardPlayed);
        
        yield return null;
    }

    public override void SetDescription_Effect_01()
    {
        Debug.Log("Second upgrade unlocked. Setting description.");
        EffectDescription_01 = MenuManager.Instance.CardEffectDescriptions.EffectDescription_Tyniro();
        Debug.Log($"Description 1: {EffectDescription_01}");
    }

    // Upgrade menu
    public override void Upgrade_01(MenuSlot menuSlot)
    {
        Upgrade upgrade = CardUpgrades[0];
        if(!CanAfford(upgrade,menuSlot)) return;
        SpendCurrency(upgrade);
        IncreaseUpgradeCost(upgrade,2);
        CardValue++;
        OnUpgrade_Post(menuSlot);
    }
    public override void Upgrade_02(MenuSlot menuSlot)
    {
        Upgrade upgrade = CardUpgrades[1];

        if(!CanAfford(upgrade,menuSlot,false)) return;
        SpendCurrency(upgrade);
        IsSecondUpgradeUnlocked = true;
        CardUpgrades.Remove(upgrade);
        MenuUiManager.Instance.MenuCardUpgradeUiHandler.SetUpgradePanelsData(this,menuSlot);
        SetDescription_Effect_01();
        OnUpgrade_Post(menuSlot);
    }

    public override void LoadData(SavedDataClass savedData, int index = 0)
    {
        Debug.Log($"Tyniro effect description loaded: [{savedData.TyniroSaveDatas[index].Effect_01_Description}] | Tyniro is second upgrade unlocked loaded: [{savedData.TyniroSaveDatas[index].IsSecondUpgradeUnlocked}] | Index loaded: [{index}]");
        TyniroSaveData data = savedData.TyniroSaveDatas[index];

        CardId = data.Id;
        CardValue = data.CardValue;
        ManaCost = data.CardCost;
        CardUpgrades.Clear();
        CardUpgrades.AddRange(data.Upgrades);
        CardUpgrades[0].UpgradeCost = data.UpgradeCost_01;
        IsSecondUpgradeUnlocked = data.IsSecondUpgradeUnlocked;
        EffectDescription_01 = data.Effect_01_Description;

        // SetDescription_Effect_01();
    }

    public TyniroSaveData GetSaveData()
    {
        TyniroSaveData data = new TyniroSaveData();
        Debug.Log($"Tyniro effect description save: [{EffectDescription_01}] | Tyniro is second upgrade unlocked save: [{IsSecondUpgradeUnlocked}]");
        data.Id = CardId;
        data.CardCost = ManaCost;
        data.CardValue = CardValue;
        data.UpgradeCost_01 = CardUpgrades[0].UpgradeCost;
        data.IsSecondUpgradeUnlocked = IsSecondUpgradeUnlocked;
        data.Upgrades = CardUpgrades;
        data.Effect_01_Description = IsSecondUpgradeUnlocked ? MenuManager.Instance.CardEffectDescriptions.EffectDescription_Tyniro() : "";
        return data;
    }
}