using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory")]
public class InvenInfo : ScriptableObject
{
    List<ItemInfo> items;

    public void Additems(ItemInfo item)
    {
        items.Add(item);
    }

    public void Removeitems(ItemInfo item)
    {
        items.Remove(item);
    }

    public void RemoveitemWithIndex(int index)
    {
        items.RemoveAt(index);
    }
}
