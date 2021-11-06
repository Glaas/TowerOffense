using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave_Data", menuName = "WaveCreator/Create new wave", order = 1)]

public class WaveSO : ScriptableObject
{
    public List<Drop> drops;
}
