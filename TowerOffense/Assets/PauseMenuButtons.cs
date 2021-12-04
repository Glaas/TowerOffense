using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuButtons : MonoBehaviour
{
    public GameObject feedbackCanvas;

    public void Start()
    {
        feedbackCanvas = GameObject.Find("FeedbackCanvas");
        feedbackCanvas.SetActive(false);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void FeedbackButton()
    {
        feedbackCanvas.SetActive(true);
    }
}
