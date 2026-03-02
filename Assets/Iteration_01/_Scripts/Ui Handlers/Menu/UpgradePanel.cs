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

    void OnUpgradeButtonClick(Upgrade upgradeData, MenuSlot slot)
    {
        upgradeData.UpgradeEffect();
        slot.UpdateSlotValues();
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