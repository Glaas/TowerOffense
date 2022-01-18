using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System.Collections;
using System.Text;
using System;


public class ABTestingHandler : MonoBehaviour
{
    public string PlayerGroup;
    string gameDataDB = "http://pouchdb.gd-ue.de/rmtctl_shadowsquid_gamedata";
    string onlineStatusDoc = "/onlinestatus";
    JSONNode status;
    OnlineStatus onlineStatus;
    string rev;

#if UNITY_STANDALONE_WIN
    public void Awake()
    {
        //random player group A or B
        PlayerGroup = UnityEngine.Random.Range(0, 2) == 0 ? "a" : "b";
        Debug.Log("Player group: " + PlayerGroup);
        StartCoroutine(GetOnlineStatus());
    }
#endif
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
                status[PlayerGroup]++;

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
            Debug.Log("Feedback sent successfully !");
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
[Serializable]
public class OnlineStatus
{
    public string _rev = "";
    public string a = "0";
    public string b = "0";
}
