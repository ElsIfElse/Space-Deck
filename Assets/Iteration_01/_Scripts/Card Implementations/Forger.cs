using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Forger",menuName = "Data/Cards/Forger")]
public class Forger : BaseCardData
{
    public int StrengtheningAmount_01;
    public int StrengtheningAmount_02;
    public bool IsSecondUpgradeUnlocked = false;
    
    public override IEnumerator CardEffect(CardVfx cardVfx, Card card = null)
    {
        GainValueSequence(cardVfx,card);
        yield return new WaitForSeconds(1f / GameStateManager.Instance.GlobalValues.AnimationSpeed);
        
        ActionManager.Instance.CardEffects.StrengthenRandomCard(StrengtheningAmount_01);
        AudioManager.Instance.Play(AudioType.ForgerBell);

        if(!IsSecondUpgradeUnlocked) yield break;

        yield return new WaitForSeconds(1f / GameStateManager.Instance.GlobalValues.AnimationSpeed);
        ActionManager.Instance.CardEffects.StrengthenRandomCard(StrengtheningAmount_02);
        AudioManager.Instance.Play(AudioType.ForgerBell);

        yield return null;
    }
    public override void SetDescription_Effect_01()
    {
        EffectDescription_01 = MenuManager.Instance.CardEffectDescriptions.EffectDescription_Forgero(StrengtheningAmount_01,1);
    }
    public override void SetDescription_Effect_02()
    {
        EffectDescription_02 = MenuManager.Instance.CardEffectDescriptions.EffectDescription_Forgero(StrengtheningAmount_02,2);
    }

    public override void UpgradeCardEffectValue_01()
    {
        StrengtheningAmount_01 ++;
    }
    public override void UpgradeCardEffectValue_02()
    {
        StrengtheningAmount_02 ++;
    }
    public override void SetUpgradeDescription(MenuSlot menuSlot,int upgradeIndex,string description = "")
    {
        CardUpgrades[upgradeIndex].SetUpgradeDescription(description);
    }

    public override void Upgrade_01(MenuSlot menuSlot)
    {
        Upgrade upgrade = CardUpgrades[0];

        // If you cannot afford the return
        if(!CanAfford(upgrade,menuSlot)) return;

        // Spend the currency
        SpendCurrency(upgrade);

        // Increase the stat
        UpgradeCardEffectValue_01();

        // Increase the upgrade cost of the upgrade
        IncreaseUpgradeCost(upgrade,2);

        // Set the new effect discription with the updated value
        SetDescription_Effect_01();
        OnUpgrade_Post(menuSlot);
    }
    public override void Upgrade_02(MenuSlot menuSlot)
    {
        Upgrade upgrade = CardUpgrades[1];

        if(!IsSecondUpgradeUnlocked)
        {
            if(!CanAfford(upgrade,menuSlot,false)) return;
            SpendCurrency(upgrade);
            SetUpgradeDescription(menuSlot,1,"Increase the strengthening effect by 1.");
            EffectDescription_02 = MenuManager.Instance.CardEffectDescriptions.EffectDescription_Forgero(StrengtheningAmount_02,2);
            upgrade.CurrencyType = CurrencyType.Primary; 
            upgrade.UpgradeCost = 2;
            IsSecondUpgradeUnlocked = true;
            MenuUiManager.Instance.MenuCardUpgradeUiHandler.SetUpgradePanelsData(this,menuSlot);
            OnUpgrade_Post(menuSlot);
        }
        else
        {
            if(!CanAfford(upgrade,menuSlot)) return;
            SpendCurrency(upgrade);
            UpgradeCardEffectValue_02();
            IncreaseUpgradeCost(upgrade,2);
            SetDescription_Effect_02();
            OnUpgrade_Post(menuSlot);
        }

    }

}