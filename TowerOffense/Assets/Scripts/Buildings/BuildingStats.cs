using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BuildingStats : MonoBehaviour
{
    public GameObject explosionOnDeathParticleSystemPrefab;
    public int maxHealth;
    public int currentHealth;
    public int enemiesDestroyed = 0;
    Color healthy = Color.green;
    Color ded = Color.red;

    public void TakeDamage(int amount)
    {
        foreach (Image child in GetComponentsInChildren<Image>())
        {
            if (child.gameObject.CompareTag("Healthbar")) //Update healthbar color
            {
                child.gameObject.GetComponent<Image>().fillAmount = (float)currentHealth / (float)maxHealth;
                child.gameObject.GetComponent<Image>().color = Color.Lerp(ded, healthy, (float)currentHealth / (float)maxHealth);
            }
        }
        currentHealth--;
        if (currentHealth <= 0)
        {
            if (CompareTag("Turret"))
            {
                GlobalDataHandler.instance.turretsDestroyed++;
            }

            DOTween.Kill(this);
            var obj = Instantiate(explosionOnDeathParticleSystemPrefab, transform.position, Quaternion.identity);
            Destroy(obj, 2f);
            GameObject.Find("TurretDestroySFX").GetComponent<AudioSource>().Play();
            print($"{name} current health has reached zero and is destroyed now");
            //kill tween on all children
            foreach (Transform child in transform)
            {
                SendMessage("CancelInvoke", child);
                DOTween.Kill(child);
            }
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitHealth();
    }
    void InitHealth()
    {
        if (CompareTag("Tower"))
        {
            maxHealth = GlobalDataRetriever.instance.towerMaxHealthAmount;
        }
        else
        {
            maxHealth = GlobalDataRetriever.instance.turretMaxDurability;
        }
        currentHealth = maxHealth;
    }
    float GetHealthPercentage()
    {
        return (float)currentHealth / (float)maxHealth;
    }
}
