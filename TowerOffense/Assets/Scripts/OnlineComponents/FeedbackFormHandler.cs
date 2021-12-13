using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;


public class FeedbackFormHandler : MonoBehaviour
{
    public ToggleGroup moodToggleGroup;
    public ToggleGroup formTypeToggleGroup;
    public TMP_InputField writtenFeedbackInputField;
    public GameObject feedbackForm;


    public void ToggleFeedbackForm()
    {
        if (!feedbackForm.activeSelf)
        {
            feedbackForm.SetActive(true);
            feedbackForm.transform.localScale = Vector3.zero;
            feedbackForm.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);

            Time.timeScale = 0;

            foreach (Button button in GameObject.Find("GameButtons").GetComponentsInChildren<Button>())
            {
                button.interactable = false;
                if (button.GetComponent<EventTrigger>())
                {
                    button.GetComponent<EventTrigger>().enabled = false;
                }
            }
        }
        else
        {
            feedbackForm.transform.DOScale(0, 0.5f).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() => feedbackForm.SetActive(false));
            Time.timeScale = 1;

            foreach (Button button in GameObject.Find("GameButtons").GetComponentsInChildren<Button>())
            {
                button.interactable = true;
                if (button.GetComponent<EventTrigger>())
                {
                    button.GetComponent<EventTrigger>().enabled = true;
                }
            }
        }
    }

    public void SendFeedbackForm()
    {
        FeedbackForm feedbackForm = new FeedbackForm();
        feedbackForm._id = DateTime.Now.ToString(("dd_MM_yyyy_HH_mm_ss"));
        feedbackForm.date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        feedbackForm.PlayerMood = ReturnMoodPicked();
        feedbackForm.FeedbackType = ReturnFormTypePicked();
        feedbackForm.FeedbackText = writtenFeedbackInputField.text;
        feedbackForm.WaveNumber = FindObjectOfType<WaveManager>().currentWave;
        feedbackForm.CurrentAmountOfMoney = GlobalDataHandler.instance.currentPlayerCoins;
        feedbackForm.EnemiesKilled = GlobalDataHandler.instance.enemiesKilled;
        feedbackForm.EnemiesSpawned = GlobalDataHandler.instance.enemiesSpawned;
        feedbackForm.TowerHealth = GameObject.Find("Tower").GetComponent<BuildingStats>().currentHealth;

        FindObjectOfType<DBLink>().SendFeedbackForm(feedbackForm);
    }

    string ReturnMoodPicked()
    {
        switch (moodToggleGroup.GetFirstActiveToggle().name.ToLower())
        {
            case "happytoggle":
                return "happy";
            case "sadtoggle":
                return "sad";
            case "neutraltoggle":
                return "neutral";
            default:
                throw new ArgumentException("No mood picked");
        }
    }
    string ReturnFormTypePicked()
    {
        switch (formTypeToggleGroup.GetFirstActiveToggle().name.ToLower())
        {
            case "bugtoggle":
                return "bug";
            case "suggestiontoggle":
                return "suggestion";
            case "sayhitoggle":
                return "I just want to say hi";
            case "othertoggle":
                return "other";
            default:
                throw new ArgumentException("No form type picked");
        }
    }
    public void AssignReferences()
    {
        if (moodToggleGroup == null)
        {
            moodToggleGroup = GameObject.Find("SmileysParent").GetComponent<ToggleGroup>();
        }
        if (formTypeToggleGroup == null)
        {
            formTypeToggleGroup = GameObject.Find("FormTypeToggleGroup").GetComponent<ToggleGroup>();
        }
        if (writtenFeedbackInputField == null)
        {
            writtenFeedbackInputField = GameObject.Find("WrittenFeedbackPanel").GetComponentInChildren<TMP_InputField>();
        }
    }
}

[Serializable]
public class FeedbackForm
{
    public string _id;
    public string date;
    public string PlayerMood;
    public string FeedbackType;
    public string FeedbackText;
    public int WaveNumber;
    public int CurrentAmountOfMoney;
    public int EnemiesKilled;
    public int EnemiesSpawned;
    public int TowerHealth;
}


