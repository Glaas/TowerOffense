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
        SetUpResolution();
        Screen.SetResolution(1280, 720, false);



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
}
