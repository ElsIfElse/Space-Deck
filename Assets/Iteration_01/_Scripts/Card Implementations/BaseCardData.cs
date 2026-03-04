using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Simple Card",menuName = "Data/Cards/Card")]
public class BaseCardData : ScriptableObject
{
    public string CardName;
    public string EffectDescription_01;
    public string EffectDescription_02;
    public int ManaCost;
    public int CardValue;
    public Sprite CardFront;

    public List<Upgrade> CardUpgrades = new();
    public CardType CardType;
    
    public bool CanUseUpgrade_01 = true;
    public bool CanUseUpgrade_02 = true;

    // The base card effect during gameplay when the card is played
    public virtual IEnumerator CardEffect(CardVfx cardVfx, Card card = null)
    {
        GainValueSequence(cardVfx,card);
        yield return null;
    }

    public virtual void SetDescription_Effect_01()
    {
        
    }
    public virtual void SetDescription_Effect_02()
    {
         
    }
    public virtual void SetUpgradeDescription(MenuSlot menuSlot,int upgradeIndex,string description = "")
    {
        
    }

    // Function that is being executed when the card's [1] upgrade is selected
    /// <summary>
    /// Afford check. Spend currency. Increase stat. Increase upgrade cost. Set new effect discription with updated value.
    /// </summary>

    public virtual void Upgrade_01(MenuSlot menuSlot)
    {
        // If you cannot afford the return
        // Spend the currency
        // Increase the stat
        // Increase the upgrade cost of the upgrade
        // Set the new effect discription with the updated value
    }

    // Function that is being executed when the card's [2] upgrade is selected
    public virtual void Upgrade_02(MenuSlot menuSlot)
    {
        // If you cannot afford the return
        // Spend the currency
        // Increase the stat
        // Increase the upgrade cost of the upgrade
        // Set the new effect discription with the updated value
    }

    // Function that is being called on the menu slot initialization
    // Sets the upgrade effects to the upgrade panels
    public virtual void SetCardUpgrades(MenuSlot menuSlot)
    {
        if(CardUpgrades.Count == 0) return;

        if(CardUpgrades.Count > 0) CardUpgrades[0].UpgradeEffect = () => Upgrade_01(menuSlot);
        if(CardUpgrades.Count > 1) CardUpgrades[1].UpgradeEffect = () => Upgrade_02(menuSlot);
    }


    // Checks if the player has enough currency
    // If yes - execution continues
    // If not - execution is stopped and the Warning function is called --> OnCantDo()
    public bool CanAfford(Upgrade upgrade,MenuSlot menuSlot,bool primary = true)
    {
        if(primary)
        {
            if(MenuManager.Instance.CurrencyHandler.CurrencyCount_Primary() >= upgrade.UpgradeCost && CanUseUpgrade_01)
            {
                return true;
            }
            else
            {
                OnCantDo(menuSlot);
                return false;
            }
        }
        else
        {
            if(MenuManager.Instance.CurrencyHandler.CurrencyCount_Secondary() >= upgrade.UpgradeCost && CanUseUpgrade_01)
            {
                return true;
            }
            else
            {
                OnCantDo(menuSlot);
                return false;
            }
        }
    }

    // On not affordable upgrade purchase
    // Plays audio
    // Plays animation
    public void OnCantDo(MenuSlot menuSlot)
    {
        AudioManager.Instance.Play(AudioType.CantDoThat);
        ActionManager.Instance.CardVfx.CantDoThatEffect(null, menuSlot);
    }


    // Function that is being executed when the upgrade was successfully
    // Updates the menucardslot values
    // Updates the upgrade panel's cost
    // Plays the upgrade animation
    
    /// <summary>
    /// Updates the slot's cost and value texts. Updatest the upgrade panel's cost text. Plays the upgrade animation
    /// </summary>
    public void OnUpgrade_Post(MenuSlot menuSlot)
    {
        menuSlot.UpdateSlotValues();
        MenuUiManager.Instance.MenuCardUpgradeUiHandler.SetUpgradePanelsData(this, menuSlot);
        ActionManager.Instance.CardVfx.CardUpgradedEffect(menuSlot._cardGameObject);
        AudioManager.Instance.Play(AudioType.Upgrade,0,true);
    }

    // Plays audio
    // Adds value points
    // Plays card animation
    public void GainValueSequence(CardVfx cardVfx, Card card)
    {
        AudioManager.Instance.Play(AudioType.CardEffect,0,true);
        PointCounterManager.Instance.AddPoints(card.CardValue);
        cardVfx.UseCardEffect(card);
    }

    public void SpendCurrency(Upgrade upgrade)
    {
        int cost = upgrade.UpgradeCost;
        if(upgrade.CurrencyType == CurrencyType.Primary) MenuManager.Instance.CurrencyHandler.SpendCurrency_Primary(cost);
        else MenuManager.Instance.CurrencyHandler.SpendCurrency_Secondary(cost);
    }

    public void IncreaseUpgradeCost(Upgrade upgrade,int amount)
    {
        upgrade.UpgradeCost += amount;
    }

    public virtual void UpgradeCardEffectValue_01()
    {
        
    }
    public virtual void UpgradeCardEffectValue_02()
    {
        
    }
}