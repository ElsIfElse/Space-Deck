using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int MaxTurn;
    public int CurrentTurn;

    public void Initialize()
    {
        CurrentTurn = 1;
        UpdateTurntext();
    }
    public void ChangeTurn()
    {
        if(CurrentTurn == MaxTurn) return;
        CurrentTurn++;
        UpdateTurntext();
    }

    void UpdateTurntext() => GameplayUiManager.Instance.TurnUiHandler.UpdateTurnCounter(CurrentTurn, MaxTurn);
}
