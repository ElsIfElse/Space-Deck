using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Doubler",menuName = "Data/Cards/Doubler")]
public class Duppo : BaseCardData
{
    public override IEnumerator CardEffect(CardVfx cardVfx, Card card = null)
    {
        GainValueSequence(cardVfx,card);
        yield return new WaitForSeconds(1f / GameStateManager.Instance.GlobalValues.AnimationSpeed);
        
        ActionManager.Instance.CardEffects.DoubleNextTurn = true;
        Debug.Log("Double Next Turn");
    }

    public override void Upgrade_01(MenuSlot menuSlot)
    {
        Upgrade upgrade = CardUpgrades[0];

        if(!CanAfford(upgrade,menuSlot)) return;
        SpendCurrency(upgrade); 
        CardValue++;
        IncreaseUpgradeCost(upgrade,2);
        OnUpgrade_Post(menuSlot);
    }
}