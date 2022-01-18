using System.Text;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Networking;
using SimpleJSON;

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

    }
    public void SetResolution()
    {
        int choice = resolutionDropdown.value;
        Resolution resolution = _resolutions[choice];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void RestartScene()
    {
        TogglePauseMenu();
        SceneManager.UnloadSceneAsync("UIScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        StartCoroutine(GetOnlineStatus());
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

            resolutionDropdown.value = _resolutions.Length - 1;
            resolutionDropdown.RefreshShownValue();
        }
        else if (!fullscreenToggle.isOn)
        {
            Resolution storedResolution = _resolutions[resolutionDropdown.value];
            Screen.fullScreen = fullscreenToggle.isOn;

            Screen.SetResolution(storedResolution.width, storedResolution.height, fullscreenToggle.isOn);
            resolutionDropdown.RefreshShownValue();
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
    string gameDataDB = "http://pouchdb.gd-ue.de/rmtctl_shadowsquid_gamedata";
    string onlineStatusDoc = "/onlinestatus";
    JSONNode status;
    OnlineStatus onlineStatus;
    string rev;

    public IEnumerator GetOnlineStatus()
    {
        string URL = gameDataDB + onlineStatusDoc;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(URL))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = URL.Split('/');
            int page = pages.Length - 1;

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
                PrintResponse(webRequest);
            }
            else
            {
                // Show results as text
                string textReturned = pages[page] + ":\nReceived: " + webRequest.downloadHandler.text;

                // Create a JSON object from received string data
                status = SimpleJSON.JSON.Parse(webRequest.downloadHandler.text);
                string PlayerGroup = FindObjectOfType<ABTestingHandler>().PlayerGroup;
                status[PlayerGroup]--;

                onlineStatus = new OnlineStatus();
                onlineStatus._rev = status["_rev"];
                onlineStatus.a = status["a"];
                onlineStatus.b = status["b"];
                print(JsonUtility.ToJson(onlineStatus));
                StartCoroutine(UpdateOnlineStatus());

            }
        }
    }
    IEnumerator UpdateOnlineStatus()
    {
        var postRequest = CreateRequest(gameDataDB + onlineStatusDoc, RequestType.PUT, onlineStatus);

        yield return postRequest.SendWebRequest();

        if (postRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(postRequest.error);
            PrintResponse(postRequest);
        }
        else
        {
            Debug.Log("Online status sent successfully !");
            Application.Quit();

        }
    }
    private UnityWebRequest CreateRequest(string path, RequestType type = RequestType.GET, object data = null, bool printResults = false)
    {
        var request = new UnityWebRequest(path, type.ToString());

        if (data != null)
        {
            var bodyRaw = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }

        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        return request;
    }
    void PrintResponse(UnityWebRequest request)
    {
        StringBuilder sb = new StringBuilder();
        foreach (System.Collections.Generic.KeyValuePair<string, string> dict in request.GetResponseHeaders())
        {
            sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
        }

        // Print Headers
        Debug.Log(sb.ToString());

        // Print Body
        Debug.Log("Body of request = " + request.downloadHandler.text);

        //print body of request
        Debug.Log("Data : " + Encoding.UTF8.GetString(request.uploadHandler.data));
    }
}
