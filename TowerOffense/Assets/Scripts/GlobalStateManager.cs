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
                print("Generating World...");
                break;
            case GameState.PLAYER_PREPARATION:
                print("Entering Player Preparation");
                break;
            case GameState.WAVE:
                print("Entering Wave");
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

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 100, 200, 100), "End Player preparation")){

            print("End Player preparation, starting next wave");
            Instance.gameState = GameState.WAVE;
            IterateGameState();
        }
    }
}

