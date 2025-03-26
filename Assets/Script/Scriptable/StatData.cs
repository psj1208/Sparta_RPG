using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Health,
    Speed,
    Atk,
    ProjectileCount
}

[CreateAssetMenu(fileName = "New StatData", menuName = "Stats/Character Stat")]
public class StatData : ScriptableObject
{
    public string characterName;
    public List<StatEntry> stats;
}

[System.Serializable]
public class StatEntry
{
    public StatType statType;
    public float baseValue;
}
