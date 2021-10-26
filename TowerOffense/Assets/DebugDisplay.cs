using UnityEngine;
using TMPro;

public class DebugDisplay : MonoBehaviour
{
    public TextMeshProUGUI RText;
    public TextMeshProUGUI GText;
    public TextMeshProUGUI BText;

    public InfoBox infoBox;
    public Selection selection;

    public static int nbOfEnemiesCurrentlyAlive;
    public static int enemiesKilled;
    public static int enemiesSpawned;
    public string stats;
    public string worldData;

    private void Awake()
    {
        RText = GameObject.Find("R").GetComponentInChildren<TextMeshProUGUI>();
        RText.text = string.Empty;
        GText = GameObject.Find("G").GetComponentInChildren<TextMeshProUGUI>();
        GText.text = string.Empty;
        BText = GameObject.Find("B").GetComponentInChildren<TextMeshProUGUI>();
        BText.text = string.Empty;

        infoBox = FindObjectOfType<InfoBox>();
        selection = FindObjectOfType<Selection>();

    }


    private void Start()
    {
        InitDebugInfo();
    }
    private void Update()
    {
        var enemyArr = FindObjectsOfType<EnemyStats>();
        nbOfEnemiesCurrentlyAlive = enemyArr.Length;


        stats =
        $"Enemies currently alive = {nbOfEnemiesCurrentlyAlive}\n" +
        $"Enemies killed = {enemiesKilled}\n" +
        $"Enemies spawned = {enemiesSpawned}\n";


        string s = string.Empty;
        if (selection.selectMode == Selection.SELECT_MODE.SINGLE)
        {
            s = string.Empty;
        }
        else if (selection.selectMode == Selection.SELECT_MODE.MULTIPLE)
        {
            s = selection.pattern.ToString();
            s += "\n(Press LCTRL+TAB to switch)\n";
        }

        worldData =
            $"Selection mode : {selection.selectMode}\n" +
            "(Press TAB to switch)\n" +
            $"Selection pattern : {s}\n ";
        RText.text = stats;
        GText.text = worldData;
    }

    public void InitDebugInfo()
    {
        nbOfEnemiesCurrentlyAlive = 0;
        enemiesKilled = 0;
        enemiesSpawned = 0;
    }
}
