using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Handeroo", menuName = "Data/Cards/Handeroo")]
public class Handeroo : BaseCardData, ILockedCard
{
    [SerializeField] private bool _isCardLocked; public bool IsCardLocked {get => _isCardLocked; set => _isCardLocked = value;}
    [SerializeField] private int _unlockCost; public int UnlockCost { get => _unlockCost; set => _unlockCost = value; }

    public int AdditionalCardsToBeDrawn;

    public override IEnumerator CardEffect(CardVfx cardVfx, Card card = null)
    {
        GainValueSequence(cardVfx,card);
        yield return new WaitForSeconds(1f / GameStateManager.Instance.GlobalValues.AnimationSpeed);
        yield return ActionManager.Instance.StartRoutine(ActionManager.Instance.CardEffects.ChangeHand(AdditionalCardsToBeDrawn));
    }

    public override void SetDescription_Effect_01()
    {
        EffectDescription_01 = MenuManager.Instance.CardEffectDescriptions.EffectDescription_Handeroo(AdditionalCardsToBeDrawn);
    }

    public override void Upgrade_01(MenuSlot menuSlot)
    {
        Upgrade upgrade = CardUpgrades[0];
        if(!CanAfford(upgrade,menuSlot)) return;
        SpendCurrency(upgrade);
        AdditionalCardsToBeDrawn++;
        IncreaseUpgradeCost(upgrade,2);
        SetDescription_Effect_01();
        OnUpgrade_Post(menuSlot);
    }

}