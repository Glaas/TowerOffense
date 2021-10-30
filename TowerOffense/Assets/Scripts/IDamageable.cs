using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int currentHealth { get; set; }
    int maxHealth { get; set; }
    bool isAlive { get; set; }
}