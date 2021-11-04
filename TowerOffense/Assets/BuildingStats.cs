using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    void TakeDamage(int amount)
    {
        currentHealth--;
        if (currentHealth <= 0) { Destroy(gameObject); }
    }
}
