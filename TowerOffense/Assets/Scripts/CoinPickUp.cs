using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public int coinValue = 0;
    private void OnMouseDown()
    {
        print("Coin picked up");
        GlobalDataHandler.instance.AddMoney(coinValue);
        Destroy(gameObject);
    }
}
