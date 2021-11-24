using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class GlobalDataRetriever : MonoBehaviour
{
    public static GlobalDataRetriever instance;
    public JSONNode spreadsheetData;

    const string ENEMY_HEALTH_AMOUNT = "B3";
    public int enemyHealthAmount;
    const string TOWER_MAX_HEALTH_AMOUNT = "B4";
    public int towerMaxHealthAmount;
    const string TOWER_MAX_DURABILITY = "B5";
    public int towerMaxDurability;
    const string COIN_VALUE = "B6";
    public int coinValue;
    const string ENEMY_SPEED = "B7";
    public float enemySpeed;

    public void UpdateValues()
    {
        print("updating values");
        spreadsheetData = FindObjectOfType<GetDataFromEthercalc>().spreadsheetRetrieved;

        enemyHealthAmount = spreadsheetData[ENEMY_HEALTH_AMOUNT]["datavalue"].AsInt;
        towerMaxHealthAmount = spreadsheetData[TOWER_MAX_HEALTH_AMOUNT]["datavalue"].AsInt;
        towerMaxDurability = spreadsheetData[TOWER_MAX_DURABILITY]["datavalue"].AsInt;
        coinValue = spreadsheetData[COIN_VALUE]["datavalue"].AsInt;
        enemySpeed = spreadsheetData[ENEMY_SPEED]["datavalue"].AsFloat;



    }
    private void OnEnable()
    {
        GetDataFromEthercalc.OnSpreadsheetFetch += UpdateValues;
    }
    private void OnDisable()
    {
        GetDataFromEthercalc.OnSpreadsheetFetch -= UpdateValues;
    }
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

}
