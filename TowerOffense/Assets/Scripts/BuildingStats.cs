using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int enemiesDestroyed = 0;

    void TakeDamage(int amount)
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        } //TODO make this building
          //TODO keep track of amount of enemies killed
          //TODO keep track of durability of building

    }
    void InitHealth()
    {
        currentHealth = maxHealth;
    }
    float GetHealthPercentage()
    {
        return (float)currentHealth / (float)maxHealth;
    }
}
