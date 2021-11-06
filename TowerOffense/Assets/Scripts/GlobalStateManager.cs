using System;
using NaughtyAttributes;
using UnityEngine;

public class GlobalStateManager : MonoBehaviour
{
    public static GlobalStateManager Instance;
    private void Awake() //IMPORTANT entry point of the program
    {
        Instance = this;
    }
    private void Start()
    {
        gameState = GameState.GENERATE_WORLD;
        IterateGameState();
    }
    public enum GameState
    {
        GENERATE_WORLD,
        PLAYER_PREPARATION,
        WAVE,
        PLAYER_WIN,
        PLAYER_LOSE
    }
    public GameState gameState;


    [Button("Cycle through")]
    public void IterateGameState()
    {
        switch (gameState)
        {
            case GameState.GENERATE_WORLD:
                FindObjectOfType<WorldBuilder>().GeneratingWorld();
                break;
            case GameState.PLAYER_PREPARATION:
                print("World generated, entering Player Preparation");
                break;
            case GameState.WAVE:
                print("Preparation finished, entering Wave");
                FindObjectOfType<WaveManager>().StartWave();
                break;
            case GameState.PLAYER_WIN:
                print("Player wins !");
                break;
            case GameState.PLAYER_LOSE:
                print("Player loses! Oh no !");
                break;
        }
    }
}
