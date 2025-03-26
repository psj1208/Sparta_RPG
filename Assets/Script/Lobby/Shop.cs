using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] InvenInfo invenInfo;
    [SerializeField] InvenInfo playerInven;
    public ShopItem[] itemslots;

    [SerializeField] GameObject shopSlotPrefab;
    public GameObject ShopWindow;
    public Transform ShopParent;

    public ShopItem selItem;
    public int selIndex;
    [SerializeField] Button useButton;
    // Start is called before the first frame update
    void Start()
    {
        ShopWindow.SetActive(false);
        useButton.onClick.AddListener(() => Buy());
        itemslots = new ShopItem[invenInfo.length];
        for (int i = 0; i < invenInfo.length; i++)
        {
            itemslots[i] = Instantiate(shopSlotPrefab, ShopParent).GetComponent<ShopItem>();
        }
        UpdateUI();
    }

    public void Toggle()
    {
        if (ShopWindow.activeSelf)
        {
            ShopWindow.SetActive(false);
        }
        else
        {
            ShopWindow.SetActive(true);
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < itemslots.Length; i++)
        {
            itemslots[i].Init(this, i, invenInfo.ReturnItem(i));
        }
    }

    public void Buy()
    {
        if (selItem.itemInfo == null)
            return;
        playerInven.Additems(selItem.itemInfo);
        UpdateUI();
    }

    public void AddItem(ItemInfo item)
    {
        invenInfo.Additems(item);
        UpdateUI();
    }

    void RemoveSelectedItem()
    {
        invenInfo.RemoveitemWithIndex(selIndex);
    }
}
