using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "OlForgie", menuName = "Data/Cards/OlForgie")]
public class OlForgie : BaseCardData, ILockedCard
{
    public bool isCardLocked {get => IsCardLocked; set => IsCardLocked = value;}
    [SerializeField] private int _unlockCost; public int UnlockCost { get => _unlockCost; set => _unlockCost = value; }
    public int ValueUpgradeAmount;

    public override IEnumerator CardEffect(CardVfx cardVfx, Card card = null)
    {
        GainValueSequence(cardVfx,card);
        yield return new WaitForSeconds(1f / GameStateManager.Instance.GlobalValues.AnimationSpeed);
        yield return ActionManager.Instance.StartRoutine(ActionManager.Instance.CardEffects.StrenghtenAllCardsInHand(ValueUpgradeAmount));
    }

    public override void SetDescription_Effect_01()
    {
        EffectDescription_01 = MenuManager.Instance.CardEffectDescriptions.EffectDescription_OlForgie(ValueUpgradeAmount);
    }

    public override void Upgrade_01(MenuSlot menuSlot)
    {
        Upgrade upgrade = CardUpgrades[0];
        if(!CanAfford(upgrade,menuSlot)) return;
        SpendCurrency(upgrade);
        ValueUpgradeAmount++;
        IncreaseUpgradeCost(upgrade,2);
        SetDescription_Effect_01();
        OnUpgrade_Post(menuSlot);
    }

    public override void LoadData(SavedDataClass data, int index = default)
    {
        OlForgieSaveData saveData = data.OlForgieSaveData;

        CardValue = saveData.CardValue;
        ManaCost = saveData.CardCost;
        ValueUpgradeAmount = saveData.ValueUpgradeAmount;

        CardUpgrades[0].UpgradeCost = saveData.UpgradeCost_01;
        IsCardLocked = saveData.IsCardLocked;
        UnlockCost = saveData.UnlockCost;
    }

    public OlForgieSaveData GetSaveData()
    {
        OlForgieSaveData saveData = new OlForgieSaveData();
        saveData.CardValue = CardValue;
        saveData.CardCost = ManaCost;
        saveData.ValueUpgradeAmount = ValueUpgradeAmount;

        saveData.UpgradeCost_01 = CardUpgrades[0].UpgradeCost;
        saveData.IsCardLocked = IsCardLocked;
        saveData.UnlockCost = UnlockCost;
        return saveData;
    }

}