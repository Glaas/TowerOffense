using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NaughtyAttributes;

public class WaveManager : MonoBehaviour
{
    public GameObject slasherPrefab;
    public List<EnemySpawner> spawnersObjects;
    public Wave myWave;

    private void Start()
    {

        for (int i = 0; i < GameObject.Find("--Spawners--").transform.childCount; i++)
        {
            var obj = GameObject.Find("--Spawners--").transform.GetChild(i).gameObject;
            obj.AddComponent<EnemySpawner>();
            spawnersObjects.Add(obj.GetComponent<EnemySpawner>());
            obj.name = "Spawner : " + i;

        }
    }

    [Button("StartWave")]
    public void StartWave()
    {
        StartCoroutine("StartWave", myWave);
    }

    IEnumerator StartWave(Wave wave)
    {
        print($"Starting wave {myWave.ToString()}");
        for (int i = 0; i < wave.Drops.Count; i++)
        {
            for (int j = 0; j < wave.Drops[j].enemyAmount; j++)
            {
                print($"Starting drop {wave.Drops[j]}, which has a TimeInSeconds of {wave.Drops[j].timeInSecondsUntilNextDrop} and spawns {wave.Drops[j].enemyAmount} enemies");
                if (wave.Drops[j].isLastDrop)
                {
                    print("This is the last drop");
                }
                spawnersObjects[UnityEngine.Random.Range(0, spawnersObjects.Count)].EnemySpawn(slasherPrefab);
                yield return new WaitForSeconds(wave.Drops[j].timeInSecondsUntilNextDrop);
            }
        }
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
