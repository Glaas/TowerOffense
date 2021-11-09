using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NaughtyAttributes;

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

    [Button("StartWave")]
    public void StartWave()
    {
        StartCoroutine("StartWaveCorout", waves[currentWave]);
    }

    IEnumerator StartWaveCorout(Wave wave)
    {
        UiHandler.instance.SetInfo("Wave incoming !");
        if (currentWave == wavesTotal)
        {
            print("Last wave !");
            UiHandler.instance.SetInfo("Last wave !");

        }
        foreach (Drop drop in wave.Drops)
        {
            print($"Starting drop, which has a TimeInSeconds of {drop.timeInSecondsUntilNextDrop} and spawns {drop.enemyAmount} enemies");

            for (int i = 0; i < drop.enemyAmount; i++)
            {
                if (drop.isLastDrop) print("Last drop");
                var spawnerChosen = spawnersObjects[UnityEngine.Random.Range(0, spawnersObjects.Count)].GetComponent<EnemySpawner>();
                spawnerChosen.StartPortalSequence(slasherPrefab);
            }
            yield return new WaitForSeconds(drop.timeInSecondsUntilNextDrop);

            while (FindObjectOfType<EnemyStats>())
            {
                print("Enemies are still alive");
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(drop.timeInSecondsUntilNextDrop);
        }

        while (FindObjectOfType<EnemyStats>())
        {
            print("Enemies are still alive");
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
}




[Serializable]
public class Wave
{
    public List<Drop> Drops;

    [Button("Save Wave to file")]
    public void SaveWaveToFile()
    {
        // SomeFancyCode
    }
}
[Serializable]
public class Drop
{
    public int enemyAmount;
    public float timeInSecondsUntilNextDrop;
    public bool isLastDrop;

}
