using System;
using UnityEngine;

public class FeedbackFormHandler : MonoBehaviour
{
    public GameObject feedbackForm;
    private void Start()
    {
        if (feedbackForm == null)
        {
            feedbackForm = GameObject.Find("FeedbackForm");
        }
        feedbackForm.SetActive(false);

    }

    void ToggleFeedbackForm()
    {
        feedbackForm.SetActive(!feedbackForm.activeSelf);
    }

    public void SendFeedbackForm()
    {
        FeedbackForm feedbackForm = new FeedbackForm();
        feedbackForm._id = DateTime.Now.ToString(("dd_MM_yyyy_HH_mm_ss"));
        feedbackForm.PlayerMood = "neutral";
        feedbackForm.FeedbackType = "feedback";
        feedbackForm.FeedbackText = "lorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua";
        feedbackForm.WaveNumber = FindObjectOfType<WaveManager>().currentWave;
        feedbackForm.CurrentAmountOfMoney = GlobalDataHandler.instance.currentPlayerCoins;
        feedbackForm.EnemiesKilled = GlobalDataHandler.instance.enemiesKilled;
        feedbackForm.EnemiesSpawned = GlobalDataHandler.instance.enemiesSpawned;
        feedbackForm.TowerHealth = GameObject.Find("Tower").GetComponent<BuildingStats>().currentHealth;

        FindObjectOfType<DBLink>().SendFeedbackForm(feedbackForm);


    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 60), "Toggle form"))
        {
            ToggleFeedbackForm();
        }
        if (GUI.Button(new Rect(10, 70, 100, 60), "Submit"))
        {
            SendFeedbackForm();
        }
    }
}
public enum PlayerMood
{
    Happy,
    Neutral,
    Sad

}
public enum FeedbackType
{
    Bug,
    Suggestion,
    Complaint,
    Other
}

[Serializable]
public class FeedbackForm
{
    public string _id;
    public string PlayerMood;
    public string FeedbackType;
    public string FeedbackText;
    public int WaveNumber;
    public int CurrentAmountOfMoney;
    public int EnemiesKilled;
    public int EnemiesSpawned;
    public int TowerHealth;
}


