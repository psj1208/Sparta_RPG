using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class ItemInfo : ScriptableObject
{
    public Sprite ItemImage;
    public string ItemName;

    [Header("Stacking")]
    public bool CanStacking;
    public int StackingNumber;

    [Header("Info")]
    [SerializeField] private StatType type;
    [SerializeField] private float amount;
    [SerializeField] private bool isPermanent;
    [SerializeField] float duration;

    public void Use()
    {
        GameManager.Instance.player.statHandler.ModifyStat(type, amount, isPermanent, duration);
    }
}
