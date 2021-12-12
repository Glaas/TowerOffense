using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave_Data", menuName = "WaveCreator/Create new waves", order = 1)]

public class WavesSO : ScriptableObject
{
    public List<Wave> waves;
}
