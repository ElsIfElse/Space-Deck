using System.Collections;
using UnityEngine;

public abstract class GameState
{
    public GameStateManager _gameStateManager;
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void Tick();

    public void Initialize(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
    }
}