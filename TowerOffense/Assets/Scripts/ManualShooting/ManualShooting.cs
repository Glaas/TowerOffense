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
    
    public void TargetingMode()
    {
        //start coroutine 10 seconds
        
        
        //during coroutine:
        timeRemaining = 1000;
        timerIsRunning = true;
        //TODO change cursor to kill enemies
        //TODO some sort of cue that the thing has started
        //TODO deactivate kill enemies otherwise

        //after coroutine:
        //reset turretsdespawned counter (only locally) to 0
    }

    public void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                targetingMode = true;
                print($"targetingMode is " + targetingMode);
            }
            else
            {
                targetingMode = false;
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
}