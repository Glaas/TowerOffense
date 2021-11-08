using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TowerOffense;

public class UiHandler : MonoBehaviour
{
    public Button placeTurretButton;
    public Button placeWallButton;
    public Button startWaveButton;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI wavesText;
    public TextMeshProUGUI infoText;

    private void Start()
    {
        print(placeTurretButton.onClick.GetPersistentEventCount());
        placeTurretButton.onClick.AddListener(() => { FindObjectOfType<Selection>().EnterPlacingTurret(); });
        //  placeWallButton.onClick.AddListener(() => { GameManager.instance.PlaceWall(); });
        //  startWaveButton.onClick.AddListener(() => { GameManager.instance.StartWave(); });
    }

    public void WasCalled()
    {
        print("Was called");
    }
}
