using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataHandler : MonoBehaviour
{
    //make instance
    public static GlobalDataHandler instance;
    public int playerStartingCoins;
    public int currentPlayerCoins = 0;
    public int enemiesKilled = 0;
    public int enemiesSpawned = 0;
    public int amountOfBuildingsPlaced = 0;
    public int turretsDestroyed = 0;


    //make singleton
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {

        InitData();
        UiHandler.instance.UpdateCoins();
    }

    void InitData()
    {
        turretsDestroyed = 0;
        enemiesKilled = 0;
        currentPlayerCoins = playerStartingCoins;
    }
    public void AddMoney(int amount)
    {
        currentPlayerCoins += amount;
        UiHandler.instance.UpdateCoins();
    }
    public void SubtractMoney(int amount) => currentPlayerCoins -= amount;
}
