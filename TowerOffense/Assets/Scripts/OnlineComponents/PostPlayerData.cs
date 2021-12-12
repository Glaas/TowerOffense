using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PostPlayerData : MonoBehaviour
{
    public string playerName;
    public string age;

    private void Start()
    {
        StartCoroutine(PostData());
    }

    public IEnumerator PostData()
    {

        string uri = "http://gd-ue.de:8000/";
        WWWForm form = new WWWForm();

        string mystr = "hey, est ist mi";
        var x = Uri.EscapeUriString(mystr);
        form.AddField("age", "age");
        form.AddField("name", 314);



        string[] mys = new string[2] { "seb", "alfred" };

        using (UnityWebRequest www = UnityWebRequest.Post(uri + "_/2081522lyuc9", x))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error: " + www.error);
            }
            else
            {
                Debug.Log("Success: " + www.downloadHandler.text);
            }
        }


    }
}

