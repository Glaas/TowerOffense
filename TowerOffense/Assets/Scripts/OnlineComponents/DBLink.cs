using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using NaughtyAttributes;
using SimpleJSON;

public class DBLink : MonoBehaviour
{
    public enum RequestType { GET = 0, POST = 1, PUT = 2 };

    public static event EventHandler<bool> OnRequestComplete;

    [SerializeField]
    [ReadOnly]
    string gameDataDB = "http://pouchdb.gd-ue.de/rmtctl_shadowsquid_gamedata";
    [SerializeField]
    [ReadOnly]
    string gameValuesDoc = "/gamevalues";
    public string keyToRetrieve;
    public JSONNode gameValues;
    [SerializeField]
    [ReadOnly]
    string feedbackFormsDB = "http://pouchdb.gd-ue.de/rmtctl_shadowsquid_feedbackforms";

    #region GET
    [Button("Make request")]
    public void MakeRequest()
    {
        StartCoroutine(GetGameValues());
    }
    public IEnumerator GetGameValues()
    {
        string URL = gameDataDB + gameValuesDoc;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(URL))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = URL.Split('/');
            int page = pages.Length - 1;

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
                OnRequestComplete(this, false);
            }
            else
            {
                // Show results as text
                string textReturned = pages[page] + ":\nReceived: " + webRequest.downloadHandler.text;
                print(textReturned);

                // Create a JSON object from received string data
                gameValues = SimpleJSON.JSON.Parse(webRequest.downloadHandler.text);
                OnRequestComplete(this, true);
            }
        }
    }
    #endregion
    #region POSTFEEDBACK

    IEnumerator SendFeedbackFormCorout(FeedbackForm form)
    {
        var postRequest = CreateRequest(feedbackFormsDB, RequestType.POST, form);

        yield return postRequest.SendWebRequest();

        if (postRequest.result != UnityWebRequest.Result.Success) Debug.Log(postRequest.error);
        else
        {
            Debug.Log("Feedback sent successfully !");
        }
    }
    public void SendFeedbackForm(FeedbackForm form)
    {
        StartCoroutine(SendFeedbackFormCorout(form));

    }

    #endregion

    #region helpers
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
#endregion

