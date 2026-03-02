using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AmountOfCardsPlayedCard", menuName = "Data/Cards/AmountOfCardsPlayedCard")]
public class Playedo : BaseCardData
{
    int _effectMultiplier = 1;
    public override IEnumerator CardEffect(CardVfx cardVfx, Card card = null)
    {
        // base.CardEffect(cardVfx, card);
        card.CardValue = ActionManager.Instance.CardsPlayedThisTurn * _effectMultiplier;
        GainValueSequence(cardVfx,card);
        yield return null;
    }

    public override void SetDescription_Effect_01()
    {
        EffectDescription_01 = MenuManager.Instance.CardEffectDescriptions.EffectDescription_Playedo(_effectMultiplier);
    }

    public override void Upgrade_01(MenuSlot menuSlot)
    {
        Upgrade upgrade = CardUpgrades[0];

        if(!CanAfford(upgrade,menuSlot)) return;
        SpendCurrency(upgrade);
        IncreaseUpgradeCost(upgrade,2);
        _effectMultiplier++;
        SetDescription_Effect_01();
        OnUpgrade_Post(menuSlot);
    }
}