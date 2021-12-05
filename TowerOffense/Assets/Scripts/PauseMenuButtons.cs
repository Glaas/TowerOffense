using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuButtons : MonoBehaviour
{
    public GameObject feedbackCanvas;
    public GameObject pauseMenu;
    public GameObject connectionActiveSprite;

    public PauseButton PauseButtonScript;

    public void Awake()
    {
        feedbackCanvas = GameObject.Find("FeedbackCanvas");
        feedbackCanvas.SetActive(false);

        connectionActiveSprite = GameObject.Find("ConnectionActive");

        pauseMenu = GameObject.Find("PauseMenuCanvas");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void FeedbackButton()
    {
        feedbackCanvas.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        
        //timescale back to normal (from cached timescale in pausebutton)
        Time.fixedDeltaTime = PauseButtonScript.cachedDeltaTime;
        Time.timeScale = PauseButtonScript.cachedTime;
        
        print($"current timescale is {Time.timeScale}");
        print($"current deltatime is {Time.deltaTime} and current fixed delta is {Time.fixedDeltaTime}");
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void CheckConnection()
    {
        //check the connection to the spreadsheet
        //if the spreadsheet has been successfully accessed within the last 10 seconds, connection is active
        //so the script that fetches from the connection needs to send something to this script
        
        //if connection is inactive, disable ConnectionActive sprite
        
    }
}
