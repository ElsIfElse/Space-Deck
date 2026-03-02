using TMPro;
using UnityEngine;

public class ManaHandler : MonoBehaviour
{
    public int MaxMana;
    public int CurrentMana;

    public void Initialize()
    {
        CurrentMana = MaxMana;
        UpdateManaCounterText();
    }

    public void SpendMana(int amount)
    {
        CurrentMana -= amount;
        UpdateManaCounterText();
        GameplayUiManager.Instance.ManaUiHandler.UseManaEffect();
    }

    public void GainMana(int amount)
    {
        CurrentMana += amount;
        UpdateManaCounterText();
        GameplayUiManager.Instance.ManaUiHandler.GainManaEffect();
    }

    public void ResetMana()
    {
        CurrentMana = MaxMana;
        UpdateManaCounterText();
    }

    public bool HasEnoughMana(int amount) => CurrentMana >= amount;

    public void UpdateManaCounterText() => GameplayUiManager.Instance.ManaUiHandler.UpdateCurrentMana(CurrentMana);

}