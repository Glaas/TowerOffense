using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualShooting : MonoBehaviour
{
    public GameObject enemyShotEffect;
    public bool targetingMode;

    public float timeRemaining = 0;
    public bool timerIsRunning = false;

    private void Start()
    {
        targetingMode = false;
        timeRemaining = 0;
        timerIsRunning = false;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                targetingMode = true;
            }
            else
            {
                targetingMode = false;
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    public void TargetingMode()
    {
        //start coroutine 10 seconds
        
        
        //during coroutine:
        timeRemaining = 10;
        timerIsRunning = true;
        //change cursor to kill enemies
        //some sort of cue that the thing has started
        //deactivate kill enemies otherwise

        //after coroutine:
        //reset turretsdespawned counter (only locally) to 0
    }
    
    private void OnMouseDown()
    {
        if (targetingMode && CompareTag("Enemy"))
        {
            print("Enemy targeted");

            //GameObject.Find("CoinSFX").GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
            //GameObject.Find("CoinSFX").GetComponent<AudioSource>().Play();
            var obj = Instantiate(enemyShotEffect, transform.position, Quaternion.identity);
            Destroy(obj, 1.2f);

            Destroy(gameObject);
        }
    }
}