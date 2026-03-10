using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ManaGiver", menuName = "Data/Cards/ManaGiver")]
public class Gaino : BaseCardData,ILockedCard
{
    public int ManaToGain;

    public bool isCardLocked {get => IsCardLocked; set => IsCardLocked = value;}
    [SerializeField] private int _unlockCost; public int UnlockCost { get => _unlockCost; set => _unlockCost = value; }

    public override IEnumerator CardEffect(CardVfx cardVfx, Card card = null)
    {
        if(CardValue > 0)
        {
            GainValueSequence(cardVfx,card);
            yield return new WaitForSeconds(1f / GameStateManager.Instance.GlobalValues.AnimationSpeed);
        }

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


    public override void Upgrade_02(MenuSlot menuSlot)
    {
        Upgrade upgrade = CardUpgrades[0];
        if(!CanAfford(upgrade,menuSlot)) return;
        CanUseUpgrade_01 = false;
        ManaCost--;
        SpendCurrency(upgrade);
        OnUpgrade_Post(menuSlot);
    }
    public override void Upgrade_01(MenuSlot menuSlot)
    {
        Upgrade upgrade = CardUpgrades[1];
        if(!CanAfford(upgrade,menuSlot)) return;
        SpendCurrency(upgrade);
        IncreaseUpgradeCost(upgrade,2);
        CardValue++;
        SetDescription_Effect_01();
        OnUpgrade_Post(menuSlot);
    }

    public override void LoadData(SavedDataClass savedData, int index = default)
    {
        GainoSaveData data = savedData.GainoSaveData;

        CardValue = data.CardValue;
        ManaCost = data.CardCost;
        ManaToGain = data.ManaToGain;

        CardUpgrades[0].UpgradeCost = data.UpgradeCost_01;
        CardUpgrades[1].UpgradeCost = data.UpgradeCost_02;
    }

    public GainoSaveData GetSaveData()
    {
        GainoSaveData saveData = new GainoSaveData();
        saveData.CardValue = CardValue;
        saveData.CardCost = ManaCost;
        saveData.ManaToGain = ManaToGain;

        saveData.UpgradeCost_01 = CardUpgrades[0].UpgradeCost;
        saveData.UpgradeCost_02 = CardUpgrades[1].UpgradeCost;

        return saveData;
    }

}