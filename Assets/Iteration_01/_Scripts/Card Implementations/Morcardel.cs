using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DrawCardCard", menuName = "Data/Cards/Draw Card")]
public class Morcardel : BaseCardData
{
    public int amountOfCardsToBeDrawn;
    public override IEnumerator CardEffect(CardVfx cardVfx, Card card = null)
    {
        if(CardValue > 0)
        {
            GainValueSequence(cardVfx,card);
            yield return new WaitForSeconds(1f / GameStateManager.Instance.GlobalValues.AnimationSpeed);
        }

        ActionManager.Instance.DrawCard(amountOfCardsToBeDrawn,false);
    }

    public override void SetDescription_Effect_01()
    {
        EffectDescription_01 = MenuManager.Instance.CardEffectDescriptions.EffectDescription_Morcardel();
    }

    public override void Upgrade_01(MenuSlot menuSlot)
    {
        Upgrade upgrade = CardUpgrades[0];
        if(!CanAfford(upgrade,menuSlot)) return;
        SpendCurrency(upgrade);
        CanUseUpgrade_01 = false;
        ManaCost--;
        IncreaseUpgradeCost(upgrade,2);
        SetDescription_Effect_01();
        OnUpgrade_Post(menuSlot);
    }

    public override void LoadData(SavedDataClass data, int index = default)
    {
        MorcardelSaveData saveData = data.MorcardelSaveData;

        CardValue = saveData.CardValue;
        ManaCost = saveData.CardCost;
        amountOfCardsToBeDrawn = saveData.AmountOfCardsToBeDrawn;

        CardUpgrades[0].UpgradeCost = saveData.UpgradeCost_01;
    }

    public MorcardelSaveData GetSaveData()
    {
        MorcardelSaveData saveData = new MorcardelSaveData();
        saveData.CardValue = CardValue;
        saveData.CardCost = ManaCost;
        saveData.AmountOfCardsToBeDrawn = amountOfCardsToBeDrawn;

        saveData.UpgradeCost_01 = CardUpgrades[0].UpgradeCost;
        return saveData;
    }
}