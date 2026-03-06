using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Mediumo",menuName = "Data/Cards/Mediumo")]
public class Mediumo : BaseCardData
{   
    public override IEnumerator CardEffect(CardVfx cardVfx, Card card = null)
    {
        GainValueSequence(cardVfx,card);
        AudioManager.Instance.Play(AudioType.CardPlayed);
        yield return null;
    }

    public override void SetDescription_Effect_01()
    {
        // CardDescription = "Strengthen a random card in your hand by <b>" + StrengtheningAmount + "</b>";
    }

    public override void Upgrade_01(MenuSlot menuSlot)
    {
        if(!CanAfford(CardUpgrades[0],menuSlot)) return;

        CardValue ++;
        MenuManager.Instance.CurrencyHandler.SpendCurrency_Primary(CardUpgrades[0].UpgradeCost);
        CardUpgrades[0].UpgradeCost += 2;

        OnUpgrade_Post(menuSlot);
        SetDescription_Effect_01();
    }

    public MediumoSaveData GetSaveData()
    {
        MediumoSaveData data = new MediumoSaveData();

        data.Id = CardId;
        data.CardCost = ManaCost;
        data.CardValue = CardValue;
        data.UpgradeCost_01 = CardUpgrades[0].UpgradeCost;

        return data;
    }

    public override void LoadData(SavedDataClass data, int index = 0)
    {
        MediumoSaveData saved = data.MediumoSaveDatas[index];

        CardId = saved.Id;
        CardValue = saved.CardValue;
        ManaCost = saved.CardCost;
        CardUpgrades[0].UpgradeCost = saved.UpgradeCost_01;
    }
}