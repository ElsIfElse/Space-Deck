using System.Runtime.CompilerServices;

public class CurrencyHandler
{
    int _primaryCurrency;
    int _secondaryCurrency;
    int _totalPrimaryCurrencySpent;

    public CurrencyHandler()
    {
        _primaryCurrency = 0;
        _secondaryCurrency = 0;
        _totalPrimaryCurrencySpent = 0;

        MenuUiManager.Instance.MenuCurrencyUiHandler.UpdateCurrencyText_Primary(_primaryCurrency);
        MenuUiManager.Instance.MenuCurrencyUiHandler.UpdateCurrencyText_Secondary(_secondaryCurrency);
    }

    #region Primary Currency
    public void AddCurrency_Primary(int amount)
    {
        _primaryCurrency += amount;
        MenuUiManager.Instance.MenuCurrencyUiHandler.UpdateCurrencyText_Primary(_primaryCurrency);
    }
    public void SpendCurrency_Primary(int amount)
    {
        _primaryCurrency -= amount;
        _totalPrimaryCurrencySpent += amount;

        if(_totalPrimaryCurrencySpent >= 5)
        {
            AddCurrency_Secondary(_totalPrimaryCurrencySpent / 5);
            _totalPrimaryCurrencySpent %= 5;
        }

        MenuUiManager.Instance.MenuCurrencyUiHandler.UpdateCurrencyText_Primary(_primaryCurrency);
    }
    public int CurrencyCount_Primary() => _primaryCurrency;
    #endregion

    #region Secondary Currency
    public void AddCurrency_Secondary(int amount)
    {
        _secondaryCurrency += amount;
        MenuUiManager.Instance.MenuCurrencyUiHandler.UpdateCurrencyText_Secondary(_secondaryCurrency);
    }
    public void SpendCurrency_Secondary(int amount)
    {
        _secondaryCurrency -= amount;
        MenuUiManager.Instance.MenuCurrencyUiHandler.UpdateCurrencyText_Secondary(_secondaryCurrency);
    }
    public int CurrencyCount_Secondary() => _secondaryCurrency;
    
    #endregion
}