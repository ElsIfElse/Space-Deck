using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Handeroo", menuName = "Data/Cards/Handeroo")]
public class Handeroo : BaseCardData, ILockedCard
{
    public bool isCardLocked {get => IsCardLocked; set => IsCardLocked = value;}
    [SerializeField] private int _unlockCost; public int UnlockCost { get => _unlockCost; set => _unlockCost = value; }

    public int AdditionalCardsToBeDrawn;

    public override IEnumerator CardEffect(CardVfx cardVfx, Card card = null)
    {
        if(CardValue > 0)
        {
            GainValueSequence(cardVfx,card);
            yield return new WaitForSeconds(1f / GameStateManager.Instance.GlobalValues.AnimationSpeed);
        }
        
        yield return ActionManager.Instance.StartRoutine(ActionManager.Instance.CardEffects.ChangeHand(AdditionalCardsToBeDrawn));
    }

    public override void SetDescription_Effect_01()
    {
        EffectDescription_01 = MenuManager.Instance.CardEffectDescriptions.EffectDescription_Handeroo(AdditionalCardsToBeDrawn);
    }

    public override void Upgrade_01(MenuSlot menuSlot)
    {
        Upgrade upgrade = CardUpgrades[0];
        if(!CanAfford(upgrade,menuSlot)) return;
        SpendCurrency(upgrade);
        AdditionalCardsToBeDrawn++;
        IncreaseUpgradeCost(upgrade,2);
        SetDescription_Effect_01();
        OnUpgrade_Post(menuSlot);
    }

    public override void LoadData(SavedDataClass data, int index = default)
    {
        HanderooSaveData saveData = data.HanderooSaveData;

        CardValue = saveData.CardValue;
        ManaCost = saveData.CardCost;
        AdditionalCardsToBeDrawn = saveData.AdditionalCardsToBeDrawn;

        CardUpgrades[0].UpgradeCost = saveData.UpgradeCost_01;
        IsCardLocked = saveData.IsCardLocked;
        UnlockCost = saveData.UnlockCost;
        SetDescription_Effect_01();
    }

    public HanderooSaveData GetSaveData()
    {
        HanderooSaveData saveData = new HanderooSaveData();

        saveData.CardValue = CardValue;
        saveData.CardCost = ManaCost;
        saveData.AdditionalCardsToBeDrawn = AdditionalCardsToBeDrawn;

        saveData.UpgradeCost_01 = CardUpgrades[0].UpgradeCost;
        saveData.IsCardLocked = IsCardLocked;
        saveData.UnlockCost = UnlockCost;

        return saveData;
    }

}