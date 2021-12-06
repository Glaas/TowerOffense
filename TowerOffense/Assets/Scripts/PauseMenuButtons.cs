using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuButtons : MonoBehaviour
{
    public GameObject feedbackCanvas;
    public GameObject pauseMenu;
    public Canvas pauseMenuCanvas;
    public GameObject connectionActiveSprite;

    public PauseButton PauseButtonScript;
    public UiHandler UIHandlerScript;

    public void Awake()
    {
        feedbackCanvas = GameObject.Find("FeedbackCanvas");
        feedbackCanvas.SetActive(false);

        connectionActiveSprite = GameObject.Find("ConnectionActive");
        
        //pauseMenuCanvas = UIHandlerScript.PauseMenuCanvas;
        pauseMenu = GameObject.Find("PauseMenuCanvas");
        pauseMenuCanvas = pauseMenu.GetComponent<Canvas>();
    }

    public void RestartScene()
    {
        ClosePauseMenu();

        SceneManager.UnloadSceneAsync("UIScene");
        
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //UnityEngine.SceneManagement.SceneManager.LoadScene("UIScene", UnityEngine.SceneManagement.LoadSceneMode.Additive);
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
        //timescale back to normal (from cached timescale in pausebutton)
        //Time.fixedDeltaTime = PauseButtonScript.fixedDeltaTime;
        //Time.timeScale = PauseButtonScript.cachedTime;
        Time.timeScale = 1f;

        //print($"current timescale is {Time.timeScale}");
        //print($"current deltatime is {Time.deltaTime} and current fixed delta is {Time.fixedDeltaTime}");
        
        pauseMenuCanvas.enabled = false;
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
