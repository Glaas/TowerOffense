using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TowerOffense;
using Febucci.UI;
using UnityEngine.EventSystems;

public class UiHandler : MonoBehaviour
{
    public Button baseTurretButton;
    public Button modifTurretButton;
    public Button startWaveButton;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI wavesText;
    public TextMeshProUGUI infoText;
    public GameObject PauseMenuUI;
    public Canvas PauseMenuCanvas;
    public List<Button> gameButtonsParent;

    public static UiHandler instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        gameButtonsParent.Clear();
        foreach (Button button in GameObject.Find("GameButtonsParent").GetComponentsInChildren<Button>())
        {
            gameButtonsParent.Add(button);
        }
    }
    private void Start()
    {
        foreach (Button button in gameButtonsParent)
        {
            button.onClick.AddListener(() =>
            {
                if (GlobalDataHandler.instance.currentPlayerCoins < button.GetComponent<BuildingTypeHolder>().buildingData.cost)
                {
                    SetInfo("Not enough money");
                    EventSystem.current.SetSelectedGameObject(null);
                    GlobalSoundManager.instance.PlayError();
                    return;
                }
                SelectionDataBuffer.selectedBuildingData = button.GetComponent<BuildingTypeHolder>().buildingData;
                FindObjectOfType<Selection>().EnterPlacingTurret();
            });
        }
        startWaveButton.onClick.AddListener(() => { GlobalStateManager.Instance.NextWave(); });
    }


    public void SetInfo(string info)
    {
        infoText.GetComponent<TextAnimatorPlayer>().ShowText(info);
    }
    public void SetWaveNumber(string waveText)
    {
        wavesText.text = waveText;
    }
    public void UpdateCoins()
    {
        moneyText.text = "Money: " + GlobalDataHandler.instance.currentPlayerCoins;
    }
}
