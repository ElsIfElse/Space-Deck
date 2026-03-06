using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Discardo", menuName = "Data/Cards/Discardo", order = 1)]
public class Discardo : BaseCardData
{
    int _effectMultiplier = 1;
    public override IEnumerator CardEffect(CardVfx cardVfx, Card card = null)
    {
        card.CardValue = ActionManager.Instance.DiscardPileManager.DeckCount() * _effectMultiplier;
        GainValueSequence(cardVfx,card);
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
        IncreaseUpgradeCost(upgrade,2);
        _effectMultiplier++;
        SetDescription_Effect_01();
        OnUpgrade_Post(menuSlot);
    }
}