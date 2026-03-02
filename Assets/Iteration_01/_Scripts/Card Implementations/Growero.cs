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
}