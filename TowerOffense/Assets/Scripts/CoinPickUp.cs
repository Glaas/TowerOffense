using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public int coinValue = 0;
    private void OnMouseDown()
    {
        print("Coin picked up");
        //change pitch of CoinSFX

        GameObject.Find("CoinSFX").GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
        GameObject.Find("CoinSFX").GetComponent<AudioSource>().Play();
        GlobalDataHandler.instance.AddMoney(coinValue);
        UiHandler.instance.UpdateCoins();

        Destroy(gameObject);
    }
}
