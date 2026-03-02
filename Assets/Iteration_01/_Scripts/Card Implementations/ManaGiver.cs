using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ManaGiver", menuName = "Data/Cards/ManaGiver")]
public class ManaGiver : BaseCardData
{
    public int ManaToGain;
    public override IEnumerator CardEffect(CardVfx cardVfx, Card card = null)
    {
        GainValueSequence(cardVfx,card);
        yield return new WaitForSeconds(1f / GameStateManager.Instance.GlobalValues.AnimationSpeed);

        ActionManager.Instance.ManaHandler.GainMana(ManaToGain);
        AudioManager.Instance.Play(AudioType.ManaGained,0,true);
    }

    public override void SetDescription_Effect_01()
    {
        EffectDescription_01 = MenuManager.Instance.CardEffectDescriptions.EffectDescription_Gaino(ManaToGain);
    }
    // public void UpdateDescription(int amountOfManaToGain)
    // {
    //     EffectDescription_01 = "Gain " + amountOfManaToGain + " O<size=15>2</size>.";
    // }


    public override void Upgrade_01(MenuSlot menuSlot)
    {
        Upgrade upgrade = CardUpgrades[0];
        if(!CanAfford(upgrade,menuSlot)) return;
        CanUseUpgrade_01 = false;
        ManaCost--;
        SpendCurrency(upgrade);
        OnUpgrade_Post(menuSlot);
    }
    public override void Upgrade_02(MenuSlot menuSlot)
    {
        Upgrade upgrade = CardUpgrades[1];
        if(!CanAfford(upgrade,menuSlot)) return;
        SpendCurrency(upgrade);
        IncreaseUpgradeCost(upgrade,2);
        CardValue++;
        SetDescription_Effect_01();
        OnUpgrade_Post(menuSlot);
    }
}