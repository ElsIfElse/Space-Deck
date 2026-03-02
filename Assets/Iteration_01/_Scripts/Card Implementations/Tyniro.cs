using System.Collections;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Tyniro",menuName = "Data/Cards/Tyniro")]
public class Tyniro : BaseCardData
{   
    // Ingame Card Effect
    public override IEnumerator CardEffect(CardVfx cardVfx, Card card = null)
    {
        GainValueSequence(cardVfx,card);
        AudioManager.Instance.Play(AudioType.CardPlayed);
        yield return null;
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
}