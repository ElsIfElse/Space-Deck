using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class Card : MonoBehaviour
{
    public string CardName;
    public string CardEffectDescription_01;
    public string CardEffectDescription_02;
    public int CardValue;
    public int ManaCost;
    public CardType CardType;

    public TextMeshPro ManaCostText;
    public TextMeshPro ValueCostText;
    public MeshRenderer MeshRenderer;
    Func<CardVfx,Card,IEnumerator> CardAction;

    virtual public IEnumerator CardEffect(CardVfx cardVfx)
    {
        if(CardAction != null) yield return StartCoroutine(CardAction(cardVfx,this));
    }

    public void SetCard(BaseCardData data)
    {
        SetcardValue(data.CardValue);
        SetCardManaCost(data.ManaCost);
        SetCardVisual(data.CardFront);
        SetCardName(data.CardName);
        SetcardType(data.CardType);
        SetCardEffectDescriptions(data);
        CardAction = data.CardEffect;
    }

    void SetCardVisual(Sprite sprite) => MeshRenderer.material.SetTexture("_BaseMap",sprite.texture);
    void SetCardManaCost(int value)
    {
        ManaCost = value;
        UpdateManaText();
    }
    
    public void SetcardValue(int value)
    {
        CardValue = value;
        UpdateValueText();
    }
    void SetCardName(string name)
    {
        CardName = name;
    }
    void SetCardEffectDescriptions(BaseCardData data)
    {
        if(CardType == CardType.Forgero)
        {
            Debug.Log($"Description 1: {data.EffectDescription_01}");
            Debug.Log($"Description 2: {data.EffectDescription_02}");
        }

        // data.SetDescription_Effect_01();
        CardEffectDescription_01 = data.EffectDescription_01;
        CardEffectDescription_02 = data.EffectDescription_02;
    }
    void SetcardType(CardType type) => CardType = type;

    void UpdateManaText() => ManaCostText.text = ManaCost.ToString();
    public void UpdateValueText() => ValueCostText.text = CardValue.ToString();
}