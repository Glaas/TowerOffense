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

    public void Start()
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

    public void PauseButton()
    {
        //cache timescale
        //when pausemenu active, timescale = 0
        //afterwards go back to cached timescale (which is gonna be 1 lol)
    }

    public void CheckConnection()
    {
        //check the connection to the spreadsheet
        //if the spreadsheet has been successfully accessed within the last 10 seconds, connection is active
        //so the script that fetches from the connection needs to send something to this script
        
        //if connection is inactive, disable ConnectionActive sprite
        
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
    }
}
