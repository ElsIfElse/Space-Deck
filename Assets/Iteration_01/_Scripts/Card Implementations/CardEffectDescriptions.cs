using System.Collections.Generic;
using UnityEngine.UI;

public class CardEffectDescriptions
{
    public Dictionary <CardType, CardDescription> EffectDescriptions;

    public CardEffectDescriptions()
    {
        Initialize();
    }

    void Initialize()
    {
        EffectDescriptions = new(){
            {CardType.Tyniro,new()},
            {CardType.Mediumo,new()},
            {CardType.Growero,new()},
            {CardType.Discardo,new()},
            {CardType.Morcardel,new()},
            {CardType.Gaino,new()},
            {CardType.Playedo,new()},
            {CardType.Duppo,new()},
            {CardType.Forgero,new()},
            {CardType.OlForgie,new()},
            {CardType.Handeroo,new()}
        };

        SetBaseDescriptions();
    }

    void SetBaseDescriptions()
    {
        EffectDescription_Forgero(1,1);
        EffectDescription_Growero(2);
        EffectDescription_Duppo();
        EffectDescription_Playedo(1);
        EffectDescription_Discardo(1);
        EffectDescription_Morcardel();
        EffectDescription_Gaino(1);
        EffectDescription_OlForgie(1);
        EffectDescription_Handeroo(0);
    }

    public string EffectDescription_Forgero(int valueIncrease,int descriptionIndex)
    {
        if(descriptionIndex == 1)
        {
            EffectDescriptions[CardType.Forgero].Effect_01_Description = $"Increase the value of a random card in your hand by <b>{valueIncrease}.</b>";
            return EffectDescriptions[CardType.Forgero].Effect_01_Description;  
        }

        EffectDescriptions[CardType.Forgero].Effect_02_Description = $"Increase the value of a random card in your hand by <b>{valueIncrease}.</b>";
        return EffectDescriptions[CardType.Forgero].Effect_02_Description;  
    }
    public string EffectDescription_Growero(int valueIncrease)
    {
        EffectDescriptions[CardType.Growero].Effect_01_Description = $"If this card is in your hand at the end of the turn it gains <b>{valueIncrease} value.</b>";
        return EffectDescriptions[CardType.Growero].Effect_01_Description;
    }
    public string EffectDescription_Duppo()
    {
        EffectDescriptions[CardType.Duppo].Effect_01_Description = $"The next card played will have it's value gain and effect played twice. This effect also carries over to the next turn.";
        return EffectDescriptions[CardType.Duppo].Effect_01_Description;
    }
    public string EffectDescription_Playedo(int valueIncrease)
    {
        EffectDescriptions[CardType.Playedo].Effect_01_Description = $"On top of the card's value You also gain the amount of cards played this turn * <b>{valueIncrease}</b>.";
        return EffectDescriptions[CardType.Playedo].Effect_01_Description;
    }
    public string EffectDescription_Discardo(int valueIncrease)
    {
        EffectDescriptions[CardType.Discardo].Effect_01_Description = $"On top of the card's value you also gain the amount of cards in the discard pile * <b>{valueIncrease}</b>.";
        return EffectDescriptions[CardType.Discardo].Effect_01_Description;
    }
    public string EffectDescription_Morcardel()
    {
        EffectDescriptions[CardType.Morcardel].Effect_01_Description = $"Draw a card.";
        return EffectDescriptions[CardType.Morcardel].Effect_01_Description;
    }
    public string EffectDescription_Gaino(int manaGainCount)
    {
        EffectDescriptions[CardType.Gaino].Effect_01_Description = $"Gain {manaGainCount} <sprite name=oxygen>.";
        return EffectDescriptions[CardType.Gaino].Effect_01_Description;
    }
    public string EffectDescription_OlForgie(int upgradeValue)
    {
        EffectDescriptions[CardType.OlForgie].Effect_01_Description = $"Upgrade all cards value in hand by <b>{upgradeValue}</b>.";
        return EffectDescriptions[CardType.OlForgie].Effect_01_Description; 
    }
    public string EffectDescription_Handeroo(int additionalCardsToBeDrawn)
    {
        EffectDescriptions[CardType.Handeroo].Effect_01_Description = $"Discard all cards from hand and draw the same amount from the deck pile + <b>{additionalCardsToBeDrawn}</b>.";
        return EffectDescriptions[CardType.Handeroo].Effect_01_Description;
    }
    public string EffectDescription_Tyniro()
    {
        return $"When this card is played and your hand is empty this card's value is counted 2x this turn.";
    }
}

public class CardDescription
{
    public string Effect_01_Description;
    public string Effect_02_Description;
}
    