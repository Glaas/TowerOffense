using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IngameDebugConsole;

public class CustomConsoleCommands : MonoBehaviour
{

    private void Start()
    {
        DebugLogConsole.AddCommand<int>("gold", "add money, specify an amount", AddMoney);
        DebugLogConsole.AddCommand<int>("spawn", "spawn x number of enemies", SpawnEnemy);
        DebugLogConsole.AddCommand("exit", "exit the game", ExitGame);

    }
    public static void AddMoney(int amount)
    {
        GlobalDataHandler.instance.AddMoney(amount);
        Debug.Log("Added " + amount + " to the player's money through debug console");
    }

    public static void SpawnEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            FindObjectOfType<EnemySpawner>().StartPortalSequence(FindObjectOfType<WaveManager>().slasherPrefab);
        }
        Debug.Log("Spawned " + amount + " enemies through debug console");
    }
    public static void ExitGame()
    {
        Application.Quit();
    }
}
