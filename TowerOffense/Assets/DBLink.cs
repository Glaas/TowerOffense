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

    public string URL;
    public string argument;
    [TextArea]
    public string textReturned;
    [TextArea]
    public string node;
    [Button("Make request")]
    void MakeRequest()
    {
        StartCoroutine(GetDocument());
    }
    public IEnumerator GetDocument()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(URL))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = URL.Split('/');
            int page = pages.Length - 1;

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                // Show results as text
                textReturned = pages[page] + ":\nReceived: " + webRequest.downloadHandler.text;
                print(textReturned);

                // Create a JSON object from received string data
                JSONNode jsonNode = SimpleJSON.JSON.Parse(webRequest.downloadHandler.text);
                node = jsonNode[argument];
            }
        }
    }
    [Button("Clear")]
    void Clear()
    {
        textReturned = "";
        node = "";
    }

    public string GETREQUEST;
    public string playerName;
    public int score;

    public int randomId;
    [Button("Post document")]
    public void PostDocument()
    {
        StartCoroutine(PostDocumentCoroutine());
    }
    IEnumerator PostDocumentCoroutine()
    {
        PlayerData playerData = new PlayerData() { _id = UnityEngine.Random.Range(0, 1000).ToString(), name = playerName, score = score, tags = new string[] { "tag1", "tag2" }, comments = new string[][] { new string[] { "comment1", "comment2" }, new string[] { "comment1", "comment2" } } };
        playerData.comments= new string[][] { new string[] { "comment1", "comment2" }, new string[] { "comment1", "comment2" } };


        var postRequest = CreateRequest(GETREQUEST, RequestType.POST, playerData);

        yield return postRequest.SendWebRequest();

        if (postRequest.result != UnityWebRequest.Result.Success) Debug.Log(postRequest.error);
        else
        {
            Debug.Log("Form upload complete!");

            var deserializedPostData = JsonUtility.FromJson<PostResult>(postRequest.downloadHandler.text);
            print("Deserialized data = " + deserializedPostData.success);
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
public class PlayerData
{
    public string _id = "mydoc";
    public string name;
    public int score;
    public string[] tags;
    public string[][] comments;
}
public class PostResult
{
    public string success { get; set; }
}

