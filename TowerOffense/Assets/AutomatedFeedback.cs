using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System.Text;
using System;

public class AutomatedFeedback : MonoBehaviour
{
    public enum RequestType { GET = 0, POST = 1, PUT = 2 };

    string feedbackFormsDB = "http://pouchdb.gd-ue.de/rmtctl_shadowsquid_automated_feedback";

    public void SendCurrentState()
    {
        FeedbackForm form = new FeedbackForm();
        form._id = DateTime.Now.ToString(("dd_MM_yyyy_HH_mm_ss"));
        form.date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        form.PlayerMood = "neutral";
        form.FeedbackType = "other";
        form.FeedbackText = string.Empty;
        form.WaveNumber = FindObjectOfType<WaveManager>().currentWave;
        form.CurrentAmountOfMoney = GlobalDataHandler.instance.currentPlayerCoins;
        form.EnemiesKilled = GlobalDataHandler.instance.enemiesKilled;
        form.EnemiesSpawned = GlobalDataHandler.instance.enemiesSpawned;
        form.TowerHealth = GameObject.Find("Tower").GetComponent<BuildingStats>().currentHealth;
        StartCoroutine(SendFeedbackFormCorout(form));
    }


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

