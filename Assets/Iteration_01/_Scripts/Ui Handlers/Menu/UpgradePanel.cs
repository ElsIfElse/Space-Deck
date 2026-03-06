using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour,IUiHandler
{
    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private TextMeshProUGUI _upgradeNameText;
    [SerializeField] private TextMeshProUGUI _upgradeCostText;
    [SerializeField] private TextMeshProUGUI _upgradeDescriptionText;
    [SerializeField] private Button _upgradeButton;
    
    public void SetPanelData(Upgrade upgradeData, MenuSlot slot, CurrencyType currencyType)
    {
        _upgradeNameText.text = upgradeData.UpgradeName;
        if(currencyType == CurrencyType.Primary )_upgradeCostText.text = "<sprite=0>" + upgradeData.UpgradeCost.ToString();
        else _upgradeCostText.text = "<sprite name=stone_02>" + upgradeData.UpgradeCost.ToString();
        _upgradeDescriptionText.text = upgradeData.UpgradeDescription;

        _upgradeButton.onClick.RemoveAllListeners();
        _upgradeButton.onClick.AddListener(()=> OnUpgradeButtonClick(upgradeData, slot));
    }
    public void SetPanelData_Unlock(ILockedCard upgradeData, MenuSlot menuSlot)
    {
        _upgradeNameText.text = "Add card to deck."; 
        _upgradeCostText.text = "<sprite name=stone_02>" + upgradeData.UnlockCost.ToString();
        _upgradeDescriptionText.text = "";

        _upgradeButton.onClick.RemoveAllListeners();
        _upgradeButton.onClick.AddListener(()=>
        {
            if(MenuManager.Instance.CurrencyHandler.CurrencyCount_Secondary() >= upgradeData.UnlockCost)
            {
                OnUnlockButtonClick(upgradeData);
                AudioManager.Instance.Play(AudioType.ForgerBell);
            }
            else
            {
                AudioManager.Instance.Play(AudioType.CantDoThat);
                ActionManager.Instance.CardVfx.CantDoThatEffect(null,menuSlot);
            }
        });
    }

    void OnUpgradeButtonClick(Upgrade upgradeData, MenuSlot slot)
    {
        upgradeData.UpgradeEffect();
        slot.UpdateSlotValues();
    }

    void OnUnlockButtonClick(ILockedCard upgradeData)
    {
       PlayerDeckHandler.Instance.MoveCardFromLockedToDeck(upgradeData as BaseCardData); 
       MenuManager.Instance.MenuSlotHandler.SetMenuSlots(false);
       MenuUiManager.Instance.MenuCardUpgradeUiHandler.SetState(false);
       upgradeData.isCardLocked = false;
    }

    public void SetState(bool state)
    {
        if (state)
        {
            _upgradePanel.SetActive(true);
        }
        else
        {
            _upgradePanel.SetActive(false);
        }
    }

}