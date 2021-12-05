using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject pauseMenu;
    public PauseMenuButtons PauseMenuButtons;
    
    public float fixedDeltaTime;
    public float cachedDeltaTime;
    public float cachedTime;

    public void Start()
    {
        pauseMenu = PauseMenuButtons.pauseMenu;
        
        PauseMenuButtons.pauseMenu.SetActive(false);
        
        //cache timescale
        this.fixedDeltaTime = Time.fixedDeltaTime;
        cachedTime = Time.timeScale;
        
        print($"current timescale is {Time.timeScale}");
        print($"current deltatime is {Time.deltaTime} and current fixed delta is {Time.fixedDeltaTime}");
    }
    public void PauseGame()
    {
        //cache timescale again just for safety
        //this.fixedDeltaTime = Time.fixedDeltaTime;
        //cachedDeltaTime = Time.fixedDeltaTime;
        //fixedDeltaTime = cachedDeltaTime;

        pauseMenu.SetActive(true);
        
        //timescale 0
        Time.timeScale = 0.0f;
        
        print($"pause. current timescale is {Time.timeScale}");
        print($"pause. current deltatime is {Time.deltaTime} and current fixed delta is {Time.fixedDeltaTime}");
    }
}
