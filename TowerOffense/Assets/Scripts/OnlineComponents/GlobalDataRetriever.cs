using System.Data.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class GlobalDataRetriever : MonoBehaviour
{
    public static GlobalDataRetriever instance;
    public JSONNode spreadsheetData;

    const string ENEMY_HEALTH_AMOUNT = "enemymaxhealth";
    public int enemyHealthAmount;
    const string TOWER_MAX_HEALTH_AMOUNT = "towermaximumhealth";
    public int towerMaxHealthAmount;
    const string TURRET_MAX_DURABILITY = "turretdurability";
    public int turretMaxDurability;
    const string COIN_VALUE = "coinvalue";
    public int coinValue;
    const string ENEMY_SPEED = "enemyspeed";
    public float enemySpeed;

    public void UpdateValues()
    {
        spreadsheetData = GetComponent<DBLink>().gameValues;

        enemyHealthAmount = spreadsheetData[ENEMY_HEALTH_AMOUNT].AsInt;
        towerMaxHealthAmount = spreadsheetData[TOWER_MAX_HEALTH_AMOUNT].AsInt;
        turretMaxDurability = spreadsheetData[TURRET_MAX_DURABILITY].AsInt;
        coinValue = spreadsheetData[COIN_VALUE].AsInt;
        enemySpeed = spreadsheetData[ENEMY_SPEED].AsFloat;



    }
    private void OnEnable()
    {
        DBLink.OnRequestComplete += Handler;
    }
    private void OnDisable()
    {
        DBLink.OnRequestComplete -= Handler;
    }

    void Handler(object s, bool res)
    {
        if (res) UpdateValues();
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
    private void Start()
    {
        StartUpdateValueLoop();
    }
    void StartUpdateValueLoop()
    {
        if (!IsInvoking("MakeRequest"))
        {
            InvokeRepeating("MakeRequest", 0, .5f);
        }
    }
    void MakeRequest()
    {
        GetComponent<DBLink>().MakeRequest();
    }
}
