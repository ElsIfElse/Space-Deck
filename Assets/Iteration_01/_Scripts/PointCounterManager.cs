using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointCounterManager : MonoBehaviour
{
    #region Simpleton Init
    public static PointCounterManager Instance;
    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }
    #endregion

    public int CurrentPoints;
    public int PointsNeededForWin;

    public void Initialize()
    {
        CurrentPoints = 0;

        if(MenuUiManager.Instance.ChooseLevelPanelUiHandler.ChoosenMap != null) Debug.Log("Choosen map is: " + MenuUiManager.Instance.ChooseLevelPanelUiHandler.ChoosenMap.MapName);
        else Debug.Log("No map choosen");

        PointsNeededForWin = MenuUiManager.Instance.ChooseLevelPanelUiHandler.ChoosenMap.TargetPoints;
        UpdatePointCounterText();
    }

    public void AddPoints(int amount)
    {
        CurrentPoints += amount;
        UpdatePointCounterText();
        IndicatePointGain(amount);
    }

    void UpdatePointCounterText() => GameplayUiManager.Instance.PointUiHandler.UpdatePointCounterText(CurrentPoints, PointsNeededForWin);
    void IndicatePointGain(int amount) => GameplayUiManager.Instance.PointUiHandler.GainPointSequence(amount);
    public void ResetPoints() => CurrentPoints = 0;

}