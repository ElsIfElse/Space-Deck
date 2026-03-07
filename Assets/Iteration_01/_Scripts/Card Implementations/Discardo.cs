using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Discardo", menuName = "Data/Cards/Discardo", order = 1)]
public class Discardo : BaseCardData
{
    int _effectMultiplier = 1;
    public override IEnumerator CardEffect(CardVfx cardVfx, Card card = null)
    {
        int originalValue = card.CardValue;
        GainValueSequence(cardVfx,card);
        yield return new WaitForSeconds(1f / GameStateManager.Instance.GlobalValues.AnimationSpeed);

        card.CardValue = ActionManager.Instance.DiscardPileManager.DeckCount() * _effectMultiplier;
        GainValueSequence(cardVfx,card);

        card.CardValue = originalValue;
        yield return null;
    }

    public override void SetDescription_Effect_01()
    {
        EffectDescription_01 = MenuManager.Instance.CardEffectDescriptions.EffectDescription_Discardo(_effectMultiplier);
    }

    public override void Upgrade_01(MenuSlot menuSlot)
    {
        Upgrade upgrade = CardUpgrades[0];

        if(!CanAfford(CardUpgrades[0],menuSlot)) return;
        SpendCurrency(upgrade);
        IncreaseUpgradeCost(upgrade,5);
        _effectMultiplier++;
        SetDescription_Effect_01();
        OnUpgrade_Post(menuSlot);
    }

    public override void LoadData(SavedDataClass savedData, int index = default)
    {
        DiscardoSaveData data = savedData.DiscardoSaveData;

        CardValue = data.CardValue;
        ManaCost = data.CardCost;
        _effectMultiplier = data.EffectMultiplier;
        
        CardUpgrades[0].UpgradeCost = data.UpgradeCost_01;
    }

    public DiscardoSaveData GetSaveData()
    {
        DiscardoSaveData saveData = new DiscardoSaveData();
        saveData.CardValue = CardValue;
        saveData.CardCost = ManaCost;
        saveData.EffectMultiplier = _effectMultiplier;
        saveData.UpgradeCost_01 = CardUpgrades[0].UpgradeCost;
        return saveData;
    }
}