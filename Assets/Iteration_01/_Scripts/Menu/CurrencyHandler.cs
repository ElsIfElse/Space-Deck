using System.Runtime.CompilerServices;

public class CurrencyHandler
{
    int _primaryCurrency;
    int _secondaryCurrency;
    int _totalUpgradesPurchased;

    public CurrencyHandler()
    {
        _primaryCurrency = 0;
        _secondaryCurrency = 0;
        _totalUpgradesPurchased = 0;

        MenuUiManager.Instance.MenuCurrencyUiHandler.UpdateCurrencyText_Primary(_primaryCurrency);
        MenuUiManager.Instance.MenuCurrencyUiHandler.UpdateCurrencyText_Secondary(_secondaryCurrency);
    }

    public void OnFirstLoad()
    {
        SetCurrency_Primary(5);
        SetCurrency_Secondary(0);
    }

    public void OnLoadSaveData(SavedDataClass data)
    {
        SetCurrency_Primary(data.PrimaryCurrency);
        SetCurrency_Secondary(data.SecondaryCurrency);
        
        _totalUpgradesPurchased++;
    }

    public int TotalUpgradesPurchased() => _totalUpgradesPurchased;

    #region Primary Currency
    public void AddCurrency_Primary(int amount)
    {
        _primaryCurrency += amount;
        MenuUiManager.Instance.MenuCurrencyUiHandler.UpdateCurrencyText_Primary(_primaryCurrency);
    }

    public void SetCurrency_Primary(int amount)
    {
        _primaryCurrency = amount; 
        MenuUiManager.Instance.MenuCurrencyUiHandler.UpdateCurrencyText_Primary(_primaryCurrency);
    }
    public void SpendCurrency_Primary(int amount)
    {
        _primaryCurrency -= amount;
        _totalUpgradesPurchased += amount;

        if(_totalUpgradesPurchased >= 3)
        {
            AddCurrency_Secondary(_totalUpgradesPurchased / 3);
            _totalUpgradesPurchased %= 3;
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
    public void SetCurrency_Secondary(int amount)
    {
        _secondaryCurrency = amount;
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