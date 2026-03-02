using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DrawCardCard", menuName = "Data/Cards/Draw Card")]
public class Morcardel : BaseCardData
{
    public int amountOfCardsToBeDrawn;
    public override IEnumerator CardEffect(CardVfx cardVfx, Card card = null)
    {
        GainValueSequence(cardVfx,card);
        yield return new WaitForSeconds(1f / GameStateManager.Instance.GlobalValues.AnimationSpeed);

        ActionManager.Instance.RunRoutine(ActionManager.Instance.DrawCard(amountOfCardsToBeDrawn));
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
}