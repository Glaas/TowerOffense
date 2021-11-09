using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GlobalStateManager : MonoBehaviour
{
    public static GlobalStateManager Instance;
    public GameObject startWaveButton;

    public GameObject winScreen;

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

        winScreen = GameObject.Find("WinScreen");
        winScreen.SetActive(false);
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
                UiHandler.instance.SetWaveNumber("");
                AdjustDirLight(.5f, .1f);
                break;
            case GameState.PLAYER_PREPARATION:
                print("Start your preparation, and click \"Next wave\" when you are ready");
                UiHandler.instance.SetInfo("Start your preparation, and click \"Next wave\" when you are ready");
                startWaveButton.SetActive(true);
                AdjustDirLight(.80f, 2f);
                break;
            case GameState.WAVE:
                //TODO dim the directional light
                AdjustDirLight(.20f, 2f);
                print("Entering Wave");
                UiHandler.instance.SetInfo("Wave incoming !");
                startWaveButton.SetActive(false);
                FindObjectOfType<WaveManager>().StartWave();
                break;
            case GameState.PLAYER_WIN:
                print("Player wins !");
                winScreen.SetActive(true);
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
    public void AdjustDirLight(float intensity, float duration)
    {
        GameObject.Find("DirLight").GetComponent<Light>().DOIntensity(intensity, duration);
    }
}

