using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int enemiesDestroyed = 0;
    Color healthy = Color.green;
    Color ded = Color.red;

    public void TakeDamage(int amount)
    {
        foreach (Image child in GetComponentsInChildren<Image>())
        {
            if (child.gameObject.CompareTag("Healthbar"))
            {
                child.gameObject.GetComponent<Image>().fillAmount = (float)currentHealth / (float)maxHealth;
                child.gameObject.GetComponent<Image>().color = Color.Lerp(ded, healthy, (float)currentHealth / (float)maxHealth);
            }
        }
        currentHealth--;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitHealth();
    }
    void InitHealth()
    {
        if (name == "TurretPrefab(Clone)")
        {
            maxHealth = GlobalDataRetriever.instance.turretMaxDurability;
        }
        else if (name == "Tower")
        {
            maxHealth = GlobalDataRetriever.instance.towerMaxHealthAmount;
        }
        else print("No health found for " + name);
        currentHealth = maxHealth;
    }
    float GetHealthPercentage()
    {
        return (float)currentHealth / (float)maxHealth;
    }
}
