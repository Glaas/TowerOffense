using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using NaughtyAttributes;
using System.IO;

public class GetDataFromWebSpreadsheet : MonoBehaviour
{
    public string googleSheetsAPIURL = "https://sheets.googleapis.com/v4/spreadsheets";
    public string sheetID = "1Zl2wV337Mjls1dJSckvE-ThFl8MGrbNDyDtYWj_aD0E";
    public string APIKey = "AIzaSyAeBeTsGl9dUuq2S9Y9fO28qZ8ZJhL2i70";
    public string worksheetName = "Sheet1";
    public string startCell, endCell;



    public enum Get_Type { SINGLE_CELL, RANGE_OF_CELLS };


    void Start()
    {

        var builtString = new StringBuilder();
        builtString.Append(googleSheetsAPIURL);
        builtString.Append("/" + sheetID);
        builtString.Append("/values");
        builtString.Append("/" + worksheetName + "!" + startCell + ":" + endCell);
        builtString.Append("?prettyPrint=true");
        builtString.Append("&");
        builtString.Append("key=" + APIKey);

        print("URL Requested = " + builtString.ToString());


        // Initiate the web request
        StartCoroutine(GetRequest(builtString.ToString()));
    }


    // Taken from the Unity Documentation:
    // https://docs.unity3d.com/ScriptReference/Networking.UnityWebRequest.Get.html
    IEnumerator GetRequest(string uri)
    {

        UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        yield return webRequest.SendWebRequest();

        string[] pages = uri.Split('/');
        int page = pages.Length - 1;

        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(pages[page] + ": Error: " + webRequest.error);
            //Debug.Log(": Error: " + webRequest.error);
        }
        else
        {
            Debug.Log("Received: " + webRequest.downloadHandler.text + "\nPage title : " + pages[page] + "\n\n\n");

            //Create a JSON object from received string data
            JSONNode jsonNode = SimpleJSON.JSON.Parse(webRequest.downloadHandler.text);
            var myJ = JSON.Parse(jsonNode["values"]);

            print(myJ);

            //WriteAsFile(myJ, nameof(myJ));
        }

    }
    //TODO put that in an editor script duh
    // public static void WriteAsFile(JSONNode node, string nodeName = "")
    // {
    //     var myDate = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
    //     var path = UnityEditor.AssetDatabase.GenerateUniqueAssetPath("Assets/SavedFiles/" + nodeName + myDate + ".json");

    //     File.WriteAllText(path, node.ToString());
    //     print("Saved to: " + path + myDate + ".json");
    // }
}
[System.Serializable]
public class MySpreadsheetData
{
    public string playerName;
    public int lives;
    public float health;

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}