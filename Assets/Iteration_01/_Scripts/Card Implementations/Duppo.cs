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

    public override void LoadData(SavedDataClass savedData, int index = default)
    {
        DuppoSaveData data = savedData.DuppoSaveData;

        CardValue = data.CardValue;
        ManaCost = data.CardCost;
        CardUpgrades[0].UpgradeCost = data.UpgradeCost_01;
    }    
    public DuppoSaveData GetSaveData()
    {
        DuppoSaveData saveData = new DuppoSaveData();
        saveData.CardValue = CardValue;
        saveData.CardCost = ManaCost;
        saveData.UpgradeCost_01 = CardUpgrades[0].UpgradeCost;
        return saveData;
    }
}