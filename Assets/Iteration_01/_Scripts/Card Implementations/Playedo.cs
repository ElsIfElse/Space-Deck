using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AmountOfCardsPlayedCard", menuName = "Data/Cards/AmountOfCardsPlayedCard")]
public class Playedo : BaseCardData
{
    int _effectMultiplier = 1;
    public override IEnumerator CardEffect(CardVfx cardVfx, Card card = null)
    {
        int originalValue = card.CardValue;
        if(card.CardValue > 0) GainValueSequence(cardVfx,card);
        {
            GainValueSequence(cardVfx,card);
            yield return new WaitForSeconds(1f / GameStateManager.Instance.GlobalValues.AnimationSpeed);
        }

        card.CardValue = ActionManager.Instance.CardEffects.CardsPlayedThisTurn() * _effectMultiplier;
        GainValueSequence(cardVfx,card);
        card.CardValue = originalValue;
        yield return null;
    }

    public override void SetDescription_Effect_01()
    {
        EffectDescription_01 = MenuManager.Instance.CardEffectDescriptions.EffectDescription_Playedo(_effectMultiplier);
    }

    public override void Upgrade_01(MenuSlot menuSlot)
    {
        Upgrade upgrade = CardUpgrades[0];

        if(!CanAfford(upgrade,menuSlot)) return;
        SpendCurrency(upgrade);
        IncreaseUpgradeCost(upgrade,2);
        _effectMultiplier++;
        SetDescription_Effect_01();
        OnUpgrade_Post(menuSlot);
    }

    public override void LoadData(SavedDataClass data, int index = default)
    {
        PlayedoSaveData saveData = data.PlayedoSaveData;

        CardValue = saveData.CardValue;
        ManaCost = saveData.CardCost;
        _effectMultiplier = saveData.EffectMultiplier;

        CardUpgrades[0].UpgradeCost = saveData.UpgradeCost_01;
        SetDescription_Effect_01();
    }

    public PlayedoSaveData GetSaveData()
    {
        PlayedoSaveData saveData = new PlayedoSaveData();
        saveData.CardValue = CardValue;
        saveData.CardCost = ManaCost;
        saveData.EffectMultiplier = _effectMultiplier;

        saveData.UpgradeCost_01 = CardUpgrades[0].UpgradeCost;
        return saveData;
    }
}