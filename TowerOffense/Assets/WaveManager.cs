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
        foreach (Drop drop in wave.Drops)
        {
            print($"Starting drop, which has a TimeInSeconds of {drop.timeInSecondsUntilNextDrop} and spawns {drop.enemyAmount} enemies");

            for (int i = 0; i < drop.enemyAmount; i++)
            {
                if (drop.isLastDrop) print("Last drop");
                var enemy = GameObject.Instantiate(
                    slasherPrefab,
                     spawnersObjects[UnityEngine.Random.Range(0, spawnersObjects.Count)].transform.position,
                      Quaternion.identity,
                         transform);

            }
            yield return new WaitForSeconds(drop.timeInSecondsUntilNextDrop);
        }
        print("Finished ! Switching to player preparation");
        GlobalStateManager.Instance.gameState = GlobalStateManager.GameState.PLAYER_PREPARATION;
        GlobalStateManager.Instance.IterateGameState();
    }
    void OnGUI()
    {


        if (GUI.Button(new Rect(10, 70, 50, 30), "Start Wave")) StartWave();
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
