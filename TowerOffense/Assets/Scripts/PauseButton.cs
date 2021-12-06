using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject pauseMenu;
    public Canvas pauseMenuCanvas;
    public PauseMenuButtons PauseMenuButtons;
    
    public float fixedDeltaTime;
    public float cachedDeltaTime;
    public float cachedTime;

    public UiHandler UIHandlerScript;

    public void Awake()
    {
        //pauseMenuCanvas = UIHandlerScript.PauseMenuCanvas;
        pauseMenu = GameObject.Find("PauseMenuCanvas");
        pauseMenuCanvas = pauseMenu.GetComponent<Canvas>();
    }

    public void Start()
    {
        //cache timescale
        fixedDeltaTime = Time.fixedDeltaTime;
        cachedTime = Time.timeScale;
        cachedDeltaTime = Time.deltaTime;
        
        //print($"current timescale is {Time.timeScale}");
        //print($"current deltatime is {Time.deltaTime} and current fixed delta is {Time.fixedDeltaTime}");
    }
    public void PauseGame()
    {
        //cache timescale again just for safety
        //this.fixedDeltaTime = Time.fixedDeltaTime;
        //cachedDeltaTime = Time.fixedDeltaTime;
        //fixedDeltaTime = cachedDeltaTime;

        pauseMenuCanvas.enabled = true;
        
        //timescale 0
        Time.timeScale = 0.0f;
        
        //print($"pause. current timescale is {Time.timeScale}");
        //print($"pause. current deltatime is {Time.deltaTime} and current fixed delta is {Time.fixedDeltaTime}");
    }
}
