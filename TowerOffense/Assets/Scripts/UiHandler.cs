using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TowerOffense;
using Febucci.UI;

public class UiHandler : MonoBehaviour
{
    public Button placeTurretButton;
    public Button placeWallButton;
    public Button startWaveButton;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI wavesText;
    public TextMeshProUGUI infoText;

    public static UiHandler instance;
//todo display wave nummer

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
    }


    private void Start()
    {
        placeTurretButton.onClick.AddListener(() => { FindObjectOfType<Selection>().EnterPlacingTurret(); });
        startWaveButton.onClick.AddListener(() => { GlobalStateManager.Instance.NextWave(); });
        SetInfo("Start your preparation, and click \"Next wave\" when you are ready");
        //  placeWallButton.onClick.AddListener(() => { GameManager.instance.PlaceWall(); });
        //  startWaveButton.onClick.AddListener(() => { GameManager.instance.StartWave(); });
    }


    public void SetInfo(string info)
    {
        infoText.GetComponent<TextAnimatorPlayer>().ShowText(info);
    }
    public void SetWaveNumber(int waveNumber)
    {
        wavesText.text = "Wave: " + waveNumber;
    }
    public void UpdateCoins()
    {
        moneyText.text = "Money: " + GlobalDataHandler.instance.currentPlayerCoins;
    }
}
