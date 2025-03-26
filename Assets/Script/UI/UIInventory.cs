using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] InvenInfo invenInfo;
    public ItemSlot[] itemslots;

    [SerializeField] GameObject invenSlotPrefab;
    public GameObject inventoryWindow;
    public Transform invenParent;

    public ItemSlot selItem;
    public int selIndex;
    [SerializeField] Button useButton;
    [SerializeField] bool CanUse= true;
    // Start is called before the first frame update
    void Start()
    {
        if (!CanUse) 
            useButton.gameObject.SetActive(false);
        inventoryWindow.SetActive(false);
        useButton.onClick.AddListener(() => Use());
        itemslots = new ItemSlot[invenInfo.length];
        for (int i = 0; i < invenInfo.length; i++) 
        {
            itemslots[i] = Instantiate(invenSlotPrefab, invenParent).GetComponent<ItemSlot>();
        }
        UpdateUI();
    }

    public void Toggle()
    {
        if (inventoryWindow.activeSelf)
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < itemslots.Length; i++) 
        {
            itemslots[i].Init(this, i, invenInfo.ReturnItem(i));
        }
    }

    public void Use()
    {
        if (selItem.itemInfo == null)
            return;
        selItem.itemInfo.Use();
        RemoveSelectedItem();
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
