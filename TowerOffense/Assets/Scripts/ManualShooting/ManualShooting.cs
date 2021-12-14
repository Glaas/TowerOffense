using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualShooting : MonoBehaviour
{
    public GameObject enemyShotEffect;
    private void OnMouseDown()
    {
        print("Enemy targeted");

        //GameObject.Find("CoinSFX").GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
        //GameObject.Find("CoinSFX").GetComponent<AudioSource>().Play();
        var obj = Instantiate(enemyShotEffect, transform.position, Quaternion.identity);
        Destroy(obj, 1.2f);

        Destroy(gameObject);
    }
}