using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public Canvas pauseMenuCanvas;
    public PauseMenuButtons PauseMenuButtons;
    public UiHandler UIHandlerScript;

    public GameObject pauseMenu;

    public float cachedTime;


    public void Awake()
    {
        pauseMenu = GameObject.Find("PauseMenuCanvas");
    }

    public void Start()
    {
        //cache timescale
        cachedTime = Time.timeScale;
    }
    public void PauseGame()
    {
        pauseMenuCanvas.enabled = true;
        //timescale 0
        Time.timeScale = 0.0f;
    }
}
