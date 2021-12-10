using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class PauseMenuButtons : MonoBehaviour
{
    public GameObject feedbackFormParent;
    public GameObject pauseMenuParent;
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

        if (fullscreenToggle == null)
        {
            fullscreenToggle = GameObject.Find("ToggleFullscreen").GetComponent<Toggle>();
        }

        // connectionActiveSprite = GameObject.Find("ConnectionActive");

        pauseMenuParent = GameObject.Find("PauseMenuParent");
    }

    public void Start()
    {
        SetUpResolution();
        Screen.SetResolution(1280, 720, false);
        pauseMenuParent.SetActive(false);

    }
    public void SetUpResolution()
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

        resolutionDropdown.AddOptions(options.ToList());

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution()
    {
        int choice = resolutionDropdown.value;
        Resolution resolution = _resolutions[choice];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        print("Resolution set to: " + resolution.width + " x " + resolution.height);
    }

    public void RestartScene()
    {
        TogglePauseMenu();
        SceneManager.UnloadSceneAsync("UIScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    public void TogglePauseMenu()
    {
        pauseMenuParent.SetActive(!pauseMenuParent.activeSelf);

        foreach (Button button in GameObject.Find("GameButtons").GetComponentsInChildren<Button>())
        {
            button.interactable = !pauseMenuParent.activeSelf;
            if (button.GetComponent<EventTrigger>())
            {
                button.GetComponent<EventTrigger>().enabled = !pauseMenuParent.activeSelf;
            }

        }
        Time.timeScale = Time.timeScale == 0 ? 1f : 0f;
    }

    public void SetFullscreen()
    {
        if (fullscreenToggle.isOn)
        {
            Screen.fullScreen = fullscreenToggle.isOn;
            Resolution storedResolution = _resolutions[_resolutions.Length];

            Screen.SetResolution(Screen.width, Screen.height, fullscreenToggle.isOn);
        }
        else if (!fullscreenToggle.isOn)
        {
            Resolution storedResolution = _resolutions[resolutionDropdown.value];
            Screen.fullScreen = fullscreenToggle.isOn;

            Screen.SetResolution(storedResolution.width, storedResolution.height, fullscreenToggle.isOn);
        }
        print("Value sent to fullscreen is " + fullscreenToggle.isOn);
        print("full screen is now " + Screen.fullScreen);

    }

    public void CheckConnection()
    {
        //check the connection to the spreadsheet
        //if the spreadsheet has been successfully accessed within the last 10 seconds, connection is active
        //so the script that fetches from the connection needs to send something to this script

        //if connection is inactive, disable ConnectionActive sprite

    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }
}
