using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataHandler : MonoBehaviour
{
    //make instance
    public static GlobalDataHandler instance;
    public int enemiesKilled = 0;
    public int currentPlayerCoins = 0;
    public int amountOfBuildingsPlaced = 0;


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

    void InitData()
    {
        enemiesKilled = 0;
        currentPlayerCoins = 0;
    }
    public void AddMoney(int amount) => currentPlayerCoins += amount;
    public void SubtractMoney(int amount) => currentPlayerCoins -= amount;
}
