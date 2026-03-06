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

    public override void LoadData(SavedDataClass data, int index = 0)
    {
        TyniroSaveData saved = data.TyniroSaveDatas[index];

        CardId = saved.Id;
        CardValue = saved.CardValue;
        ManaCost = saved.CardCost;
        CardUpgrades[0].UpgradeCost = saved.UpgradeCost_01;
    }

    public TyniroSaveData GetSaveData()
    {
        TyniroSaveData data = new TyniroSaveData();

        data.Id = CardId;
        data.CardCost = ManaCost;
        data.CardValue = CardValue;
        data.UpgradeCost_01 = CardUpgrades[0].UpgradeCost;

        return data;
    }
}