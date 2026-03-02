using System;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class MenuCurrencyUiHandler : IUiHandler
{
    GameObject _currencyPanel;
    TextMeshProUGUI _currencyText_Primary;
    TextMeshProUGUI _currencyText_Secondary;

    public MenuCurrencyUiHandler(MenuCurrencyUiData currencyUiData)
    {
        _currencyText_Primary = currencyUiData.CurrencyText_Primary;
        _currencyText_Secondary = currencyUiData.CurrencyText_Secondary;
        _currencyPanel = currencyUiData.CurrencyPanel;
    }

    public void SetState(bool state)
    {
        if (state)
        {
            _currencyPanel.SetActive(true);
        }
        else
        {
            _currencyPanel.SetActive(false);
        }
    }

    public void UpdateCurrencyText_Primary(int currency)
    {
        _currencyText_Primary.text = "<sprite index=0>" + currency.ToString();
    }
    public void UpdateCurrencyText_Secondary(int currency)
    {
        _currencyText_Secondary.text = "<sprite=0 name=stone_02>" + currency.ToString();
    }
}

[Serializable]
public struct MenuCurrencyUiData
{
    public GameObject CurrencyPanel;
    public TextMeshProUGUI CurrencyText_Primary;
    public TextMeshProUGUI CurrencyText_Secondary;
}