using System;
using TMPro;
using UnityEngine;

public class MenuCardDetailUiHandler : IUiHandler
{
    GameObject _cardDetailPanel;
    TextMeshProUGUI _cardNameText;
    TextMeshProUGUI _cardManaCostText;
    TextMeshProUGUI _cardValueText;
    TextMeshProUGUI _cardEffectDescriptionText_01;
    TextMeshProUGUI _cardEffectDescriptionText_02;

    public MenuCardDetailUiHandler(MenuCardDetailUiHandlerData data)
    {
        _cardDetailPanel = data.CardDetailPanel;
        _cardNameText = data.CardNameText;
        _cardManaCostText = data.CardManaCostText;
        _cardValueText = data.CardValueText;

        _cardEffectDescriptionText_01 = data.CardEffectDescription_01;
        _cardEffectDescriptionText_02 = data.CardEffectDescription_02;
    }

    public void ShowCardDetail(BaseCardData data)
    {
        _cardDetailPanel.SetActive(true);
        SetDisplayText(data);
    }

    public void HideCardDetail() => _cardDetailPanel.SetActive(false); 

    public void SetDisplayText(BaseCardData data)
    {
        _cardNameText.text = $"<b><u><size=22>{data.CardName}</size></u></b>";
        _cardManaCostText.text = $"<u>Cost</u>: {data.ManaCost}";
        _cardValueText.text = $"<u>Value</u>: {data.CardValue}";

        if(data.EffectDescription_01 != null && data.EffectDescription_01 != "") _cardEffectDescriptionText_01.text = "<u>Primary effect</u>: " + data.EffectDescription_01;
        else _cardEffectDescriptionText_01.text = "";
        if(data.EffectDescription_02 != null && data.EffectDescription_02 != "") _cardEffectDescriptionText_02.text = "<u>Secondary effect</u>: " + data.EffectDescription_02;
        else _cardEffectDescriptionText_02.text = "";
    }

    public void SetState(bool state)
    {
        if (state)
        {
            _cardDetailPanel.SetActive(true);
        }
        else
        {
            _cardDetailPanel.SetActive(false);
        }
    }
}

[Serializable]
public struct MenuCardDetailUiHandlerData
{
    public GameObject CardDetailPanel;
    public TextMeshProUGUI CardNameText;
    public TextMeshProUGUI CardManaCostText;
    public TextMeshProUGUI CardValueText;
    public TextMeshProUGUI CardEffectDescription_01;
    public TextMeshProUGUI CardEffectDescription_02;
}