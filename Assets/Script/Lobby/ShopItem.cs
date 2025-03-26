using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Shop shop;
    public ItemInfo itemInfo;
    public Button button;
    [SerializeField] Image icon;
    [SerializeField] Outline outline;

    public int index;

    public void Init(Shop shop, int index, ItemInfo info)
    {
        this.shop = shop;
        this.index = index;
        itemInfo = info;
        if (itemInfo != null)
            icon.sprite = itemInfo.ItemImage;
        else
            icon.sprite = null;
        button.onClick.AddListener(() =>
        {
            shop.selItem = this;
            shop.selIndex = this.index;
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
