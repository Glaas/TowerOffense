using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateManager : MonoBehaviour
{
    public enum GameState
    {
        GENERATE_WORLD,
        ASSIGN_REFERENCES,
        INITIALIZE_VALUES,
        PLAYER_PREPARATION,
        WAVE,
        PLAYER_WIN,
        PLAYER_LOSE
    }
    public GameState gameState;

    private void Awake() {
        
    }
}
