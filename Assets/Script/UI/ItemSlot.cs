using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public UIInventory inventory;
    public ItemInfo itemInfo;
    public Button button;
    [SerializeField] Image icon;
    [SerializeField] Outline outline;

    public int index;

    public void Init(UIInventory inven,int index, ItemInfo info)
    {
        inventory = inven;
        this.index = index;
        itemInfo = info;
        if (itemInfo != null)
            icon.sprite = itemInfo.ItemImage;
        else
            icon.sprite = null;
        button.onClick.AddListener(() =>
        {
            inventory.selItem = this;
            inventory.selIndex = this.index;
        });
    }

    private void OnEnable()
    {
        outline.enabled = false;
    }

    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = itemInfo.ItemImage;
    }

    public void Clear()
    {
        itemInfo = null;
        icon.gameObject.SetActive(false);
    }
}
