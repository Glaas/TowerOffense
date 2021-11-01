using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    public GameObject buildingPrefab;

    public int cost;
    public int maxHealth;
    public int currentHealth;

    private void Start()
    {
        InitHealth();
    }

    void InitHealth() => currentHealth = maxHealth;
    public void DecreaseHealth(int amount)
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            print($"{name} current health has reached zero and is destroyed now");
            Destroy(gameObject);
        }
    }
}
