using System;
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
    public GameObject connectionStatusParent;
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
        SetupAvailableResolutionsList();

        if (fullscreenToggle == null)
        {
            fullscreenToggle = GameObject.Find("ToggleFullscreen").GetComponent<Toggle>();
        }

        if (connectionStatusParent == null)
        {
            connectionStatusParent = GameObject.Find("ConnectionStatusGroup");
        }

        pauseMenuParent = GameObject.Find("PauseMenuParent");
    }

    public void Start() => pauseMenuParent.SetActive(false);

    public void SetupAvailableResolutionsList()
    {
        resolutionDropdown = GetComponentInChildren<TMP_Dropdown>();

        //FIXME - fetching resolutions from all screens instead of main one.
        _resolutions = Screen.resolutions.Distinct().ToArray();

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options.ToList());
        resolutionDropdown.RefreshShownValue();
        print("Current resolution is " + Screen.currentResolution.width + " x " + Screen.currentResolution.height);
        print("Scaled Game resolution is " + Camera.main.scaledPixelHeight + " x " + Camera.main.scaledPixelWidth);
        print("Unscaled Game resolution is " + Camera.main.pixelHeight + " x " + Camera.main.pixelWidth);
        print("Camera rect is " + Camera.main.rect);
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
            Resolution storedResolution = _resolutions[_resolutions.Length - 1];

            Screen.SetResolution(_resolutions[_resolutions.Length - 1].width, _resolutions[_resolutions.Length - 1].height, fullscreenToggle.isOn);
        }
        else if (!fullscreenToggle.isOn)
        {
            Resolution storedResolution = _resolutions[resolutionDropdown.value];
            Screen.fullScreen = fullscreenToggle.isOn;

            Screen.SetResolution(storedResolution.width, storedResolution.height, fullscreenToggle.isOn);
        }
        print("Value sent to fullscreen is " + fullscreenToggle.isOn);
        print("full screen is now " + Screen.fullScreen);
        print("game resolution is now " + Screen.currentResolution.width + " x " + Screen.currentResolution.height);

    }

    public void CheckConnection(object e, bool connectionStatus)
    {
        if (connectionStatus)
        {
            connectionStatusParent.GetComponentInChildren<Image>().color = Color.green;
            connectionStatusParent.GetComponentInChildren<TextMeshProUGUI>().text = "Connection status: Online";
        }
        else
        {
            connectionStatusParent.GetComponentInChildren<Image>().color = Color.red;
            connectionStatusParent.GetComponentInChildren<TextMeshProUGUI>().text = "Connection status: Offline";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    private void OnEnable()
    {
        DBLink.OnRequestComplete += CheckConnection;
    }
    void OnDisable()
    {
        DBLink.OnRequestComplete -= CheckConnection;
    }
}
