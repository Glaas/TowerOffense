using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class GetDataFromWebSpreadsheet : MonoBehaviour
{
    void Start()
    {
        // Initiate the web request
        StartCoroutine(GetRequest("https://www.ethercalc.org/_/7xbtpaxxhc69/cells/A1"));        
    }

    // Taken from the Unity Documentation:
    // https://docs.unity3d.com/ScriptReference/Networking.UnityWebRequest.Get.html
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                // Create a JSON object from received string data
                JSONNode jsonNode = SimpleJSON.JSON.Parse(webRequest.downloadHandler.text);

                // Get a single element from JSON data
                Debug.Log ("datavalue of the cell is: "+ jsonNode["datavalue"].ToString());

            }
        }
    }
}