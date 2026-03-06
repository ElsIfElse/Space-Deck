using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuCardUpgradeUiHandler : IUiHandler
{
    List<UpgradePanel> _upgradePanels = new();
    public MenuCardUpgradeUiHandler(MenuCardUpgradeUiHandlerData data)
    {
        _upgradePanels = data.UpgradePanels;
    }

    public void SetState(bool state)
    {
        if(state)
        {
            foreach(UpgradePanel panel in _upgradePanels) panel.SetState(true);     
        }
        else
        {
            foreach(UpgradePanel panel in _upgradePanels) panel.SetState(false); 
        }
    }

    public void HandleCardClick(BaseCardData data, MenuSlot slot)
    {
        // SetState(true);
        SetUpgradePanelsData(data,slot);
    }

    public void SetUpgradePanelsData(BaseCardData data, MenuSlot slot)
    {
        if(data is ILockedCard && (data as ILockedCard).isCardLocked == true)
        {
            HandleLockedCard(data as ILockedCard,slot);
            return;
        }

        if(data.CardUpgrades.Count == 0)
        {
            SetState(false);
            return;
        }
        int panelIndex = 0;

        foreach(Upgrade upgrade in data.CardUpgrades)
        {
            _upgradePanels[panelIndex].SetState(true);
            _upgradePanels[panelIndex].SetPanelData(upgrade,slot,upgrade.CurrencyType);
            panelIndex++;
        }

        for(int i = panelIndex; i < _upgradePanels.Count; i++)
        {
            _upgradePanels[i].SetState(false);
        }
    }

    void HandleLockedCard(ILockedCard lockedCard, MenuSlot slot)
    {
        _upgradePanels[0].SetState(true);
        _upgradePanels[0].SetPanelData_Unlock(lockedCard, slot);
        return;   
    }
}

[Serializable]
public class MenuCardUpgradeUiHandlerData
{
    public List<UpgradePanel> UpgradePanels = new();
}