using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class GetDataFromEthercalc : MonoBehaviour
{
    string uri;
    bool updateDone = true;
    float timeElapsed = 0.0f;
    float timeToWait = 2.0f;

    [SerializeField]
    string correspondingCell;

    public object valueRetrieved;

    void Start()
    {
        Debug.Log("Go to http://gd-ue.de:8000/pcwrsc9ifu1o to control the game.");
        FetchVariableFromWeb(correspondingCell);
    }

    public void FetchVariableFromWeb(string cell)
    {
        StartCoroutine(UpdateVariablesFromWeb(cell));

    }
    // Taken from the Unity Documentation:
    // https://docs.unity3d.com/ScriptReference/Networking.UnityWebRequest.Get.html
    public IEnumerator UpdateVariablesFromWeb(string cell)
    {
        if (cell == "")
        {
            Debug.LogWarning("No corresponding cell specified. Please specify a cell in the inspector. Setting the value to A1 by default. This will likely cause a parsing error that will be solved by setting the desired value in the inspector.");
            cell = "A1";
        }
        uri = "http://gd-ue.de:8000/_/pcwrsc9ifu1o/cells/" + cell;

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
                Debug.Log(pages[page] + ":\nReceived response:  \n" + webRequest.downloadHandler.text);

                // Create a JSON object from received string data
                JSONNode jsonNode = SimpleJSON.JSON.Parse(webRequest.downloadHandler.text);

                valueRetrieved = jsonNode["datavalue"];
            }


            // Notify timer to run again.
            //
            updateDone = true;
        }

        // Update is called once per frame
        // void Update()
        // {
        //     // Only start waiting for next call when the previous one is complete
        //     //
        //     if (updateDone == true)
        //     {
        //         timeElapsed += Time.deltaTime;

        //         if (timeElapsed >= timeToWait)
        //         {
        //             updateDone = false;

        //             // Initiate the web request
        //             StartCoroutine(UpdateVariablesFromWeb());

        //             timeElapsed = 0.0f;
        //         }
        //     }
        // }
    }
}