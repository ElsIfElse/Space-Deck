using System.Collections;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Growero",menuName = "Data/Cards/Growero")]
public class Growero : BaseCardData
{   
    // Ingame Card Effect
    public override IEnumerator CardEffect(CardVfx cardVfx, Card card = null)
    {
        GainValueSequence(cardVfx,card);
        AudioManager.Instance.Play(AudioType.CardPlayed);
        yield return null;
    }

    public override void SetDescription_Effect_01()
    {
        EffectDescription_01 = MenuManager.Instance.CardEffectDescriptions.EffectDescription_Growero(ActionManager.Instance.CardEffects.GroweroGrowAmount);
    }

    // Upgrade menu
    public override void Upgrade_01(MenuSlot menuSlot)
    {
        Upgrade upgrade = CardUpgrades[0];
        if(!CanAfford(upgrade,menuSlot)) return;
        SpendCurrency(upgrade);
        IncreaseUpgradeCost(upgrade,2);
        ActionManager.Instance.CardEffects.GroweroGrowAmount++;
        SetDescription_Effect_01();
        OnUpgrade_Post(menuSlot);
    }
    public override void Upgrade_02(MenuSlot menuSlot)
    {
        Upgrade upgrade = CardUpgrades[1];
        if(!CanAfford(upgrade,menuSlot)) return;
        SpendCurrency(upgrade);
        IncreaseUpgradeCost(upgrade,2);
        CardValue++;
        OnUpgrade_Post(menuSlot);
    }

    public override void LoadData(SavedDataClass savedData, int index = default)
    {
        GroweroSaveData data = savedData.GroweroSaveData;

        CardValue = data.CardValue;
        ManaCost = data.CardCost;

        CardUpgrades[0].UpgradeCost = data.UpgradeCost_01;
        CardUpgrades[1].UpgradeCost = data.UpgradeCost_02;

        ActionManager.Instance.CardEffects.GroweroGrowAmount = data.GroweroGrowAmount;
    }

    public GroweroSaveData GetSaveData()
    {
        GroweroSaveData saveData = new GroweroSaveData();
        saveData.CardValue = CardValue;
        saveData.CardCost = ManaCost;
        saveData.GroweroGrowAmount = ActionManager.Instance.CardEffects.GroweroGrowAmount;

        saveData.UpgradeCost_01 = CardUpgrades[0].UpgradeCost;
        saveData.UpgradeCost_02 = CardUpgrades[1].UpgradeCost;

        return saveData;
    }
}