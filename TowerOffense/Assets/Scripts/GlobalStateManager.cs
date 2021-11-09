using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class GlobalStateManager : MonoBehaviour
{
    public static GlobalStateManager Instance;
    public GameObject startWaveButton;
    private void Awake() //IMPORTANT entry point of the program
    {
        Instance = this;
    }
    private void Start()
    {
        startWaveButton = GameObject.Find("StartWave");
        startWaveButton.SetActive(false);
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
                UiHandler.instance.SetInfo("Generating World...");
                break;
            case GameState.PLAYER_PREPARATION:
                print("Start your preparation, and click \"Next wave\" when you are ready");
                UiHandler.instance.SetInfo("Start your preparation, and click \"Next wave\" when you are ready");
                startWaveButton.SetActive(true);
                break;
            case GameState.WAVE:
                print("Entering Wave");
                UiHandler.instance.SetInfo("Wave incoming !");
                startWaveButton.SetActive(false);
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

    public void NextWave()
    {
        gameState = GameState.WAVE;
        IterateGameState();
    }
}

