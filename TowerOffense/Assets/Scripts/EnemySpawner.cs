using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using DG.Tweening;
public class EnemySpawner : MonoBehaviour
{
    public GameObject portalIdle;

    public float portalLifetime = 2.0f;

    public bool isActive = false;
    Vector3 smol = new Vector3(.1f, .1f, .1f);

    void Start()
    {
        portalIdle = transform.GetChild(0).gameObject;
        portalIdle.SetActive(false);
        portalIdle.transform.localScale = smol;
    }

    public void StartPortalSequence(GameObject enemyPrefab)
    {
        portalIdle.SetActive(true);
        portalIdle.transform.DOScale(Vector3.one * 3.5f, 1.0f);
        StartCoroutine("PortalLoop", enemyPrefab);
    }
    IEnumerator PortalLoop(GameObject enemyPrefab)
    {
        yield return new WaitForSeconds(0.8f);

        var enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        GameObject.Find("EnemySpawnSFX").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.5f);

        portalIdle.transform.DOScale(smol, .3f);
        yield return new WaitForSeconds(.31f);
        portalIdle.SetActive(false);
    }
}

