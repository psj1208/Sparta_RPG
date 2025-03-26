using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory")]
public class InvenInfo : ScriptableObject
{
    [SerializeField] List<ItemInfo> items;
    public int length;

    public int ListLengthReturn()
    {
        return items.Count;
    }

    public ItemInfo ReturnItem(int index)
    {
        if (index < 0 || index >= items.Count)
            return null;
        return items[index];
    }

    public bool Additems(ItemInfo item)
    {
        if(items.Count >= length)
            return false;
        items.Add(item);
        return true;
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
