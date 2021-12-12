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
    public GameObject PauseMenuUI;
    public Canvas PauseMenuCanvas;

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