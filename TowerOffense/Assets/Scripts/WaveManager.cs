using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NaughtyAttributes;
using UnityEditor;
using System.Linq;


public class WaveManager : MonoBehaviour
{
    public GameObject slasherPrefab;
    List<GameObject> spawnersObjects;
    public List<Wave> waves;
    public int currentWave = 0;
    public int wavesTotal;

    private void Start()
    {
        wavesTotal = waves.Count;
        spawnersObjects = new List<GameObject>();
        for (int i = 0; i < GameObject.Find("--Spawners--").transform.childCount; i++)
        {
            var obj = GameObject.Find("--Spawners--").transform.GetChild(i).gameObject;
            spawnersObjects.Add(obj);
            obj.name = "Spawner : " + i;
        }
    }

    public void StartWave()
    {
        StartCoroutine("StartWaveCorout", waves[currentWave]);
    }

    IEnumerator StartWaveCorout(Wave wave)
    {
        UiHandler.instance.SetInfo("Wave incoming !");
        UiHandler.instance.SetWaveNumber("Wave : " + (currentWave + 1));
        if (currentWave == wavesTotal)
        {
            print("Last wave !");
            UiHandler.instance.SetInfo("Last wave !");
            UiHandler.instance.SetWaveNumber("Final wave");

        }
        foreach (Drop drop in wave.Drops)
        {
            print($"Starting drop, which has a TimeInSeconds of {drop.timeInSecondsUntilNextDrop} and spawns {drop.enemyAmount} enemies");
            for (int i = 0; i < drop.enemyAmount; i++)
            {
                var spawnerChosen = spawnersObjects[UnityEngine.Random.Range(0, spawnersObjects.Count)].GetComponent<EnemySpawner>();
                spawnerChosen.StartPortalSequence(slasherPrefab);
            }
            yield return new WaitForSeconds(drop.timeInSecondsUntilNextDrop);

            while (FindObjectOfType<EnemyStats>())
            {
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(drop.timeInSecondsUntilNextDrop);
        }

        while (FindObjectOfType<EnemyStats>())
        {
            // hold while theres enemies alive
            yield return new WaitForSeconds(1);
        }
        currentWave++;

        if (currentWave == wavesTotal)
        {
            print("player won !");
            UiHandler.instance.SetInfo("You won !");

            GlobalStateManager.Instance.gameState = GlobalStateManager.GameState.PLAYER_WIN;
            GlobalStateManager.Instance.IterateGameState();
            yield break;
        }

        print("Finished ! Switching to player preparation");
        GlobalStateManager.Instance.gameState = GlobalStateManager.GameState.PLAYER_PREPARATION;
        GlobalStateManager.Instance.IterateGameState();
    }

    public string fileName = "Save current Waves to file";
    [Button("Save")]
    public void CreateMyAsset()
    {
        WavesSO asset = ScriptableObject.CreateInstance<WavesSO>();
        asset.waves = waves.ConvertAll(x => x);

        string name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath($"Assets/{fileName}.asset");
        AssetDatabase.CreateAsset(asset, name);
        AssetDatabase.SaveAssets();
        Debug.Log($"Created asset {asset} at {name}");
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;

    }
}




[Serializable]
public class Wave
{
    public List<Drop> Drops;


}
[Serializable]
public class Drop
{
    public int enemyAmount;
    public float timeInSecondsUntilNextDrop;
    public bool isLastDrop;

}
