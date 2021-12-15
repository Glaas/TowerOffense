using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualShooting : MonoBehaviour
{
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
        //start coroutine
        timeRemaining = 1000;
        timerIsRunning = true;
  
        //todo during coroutine:
        //change cursor to kill enemies
        //some sort of cue that the thing has started

        //todo after coroutine:
        //cache number of turrets that are now despawned
        //once that number + 10 happens, activate button again
    }
}