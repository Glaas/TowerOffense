using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuButtons : MonoBehaviour
{
    public GameObject feedbackFormParent;
    public GameObject pauseMenu;
    public GameObject connectionActiveSprite;
    public Toggle fullscreenToggle;

    public PauseButton PauseButtonScript;
    public UiHandler UIHandlerScript;

    public TMP_Dropdown resolutionDropdown;
    public Resolution[] _resolutions;

    public void Awake()
    {
        feedbackFormParent = GameObject.Find("FeedbackForm");
        GetComponent<FeedbackFormHandler>().AssignReferences();
        feedbackFormParent.SetActive(false);

        //TODO remake awake function and assign everything correctly

        // if (fullscreenToggle == null)
        // {
        //     fullscreenToggle = GameObject.Find("ToggleFullscreen").GetComponent<Toggle>();
        // }

        // connectionActiveSprite = GameObject.Find("ConnectionActive");

        // //pauseMenuCanvas = UIHandlerScript.PauseMenuCanvas;
        // pauseMenu = GameObject.Find("PauseMenuCanvas");
    }

    public void Start()
    {
        resolutionDropdown = GetComponentInChildren<TMP_Dropdown>();

        _resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void RestartScene()
    {
        ClosePauseMenu();

        SceneManager.UnloadSceneAsync("UIScene");

        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //UnityEngine.SceneManagement.SceneManager.LoadScene("UIScene", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        Application.Quit();
        print("Quit");
    }

    public void FeedbackButton()
    {
        feedbackFormParent.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        //TODO close pause menu
        Time.timeScale = 1f;


    }

    public void SetFullscreen()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
        print("full screen is now " + Screen.fullScreen);
    }

    public void CheckConnection()
    {
        //check the connection to the spreadsheet
        //if the spreadsheet has been successfully accessed within the last 10 seconds, connection is active
        //so the script that fetches from the connection needs to send something to this script

        //if connection is inactive, disable ConnectionActive sprite

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
