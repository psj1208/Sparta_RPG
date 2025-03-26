using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    UIStatus status;
    UIInventory inventory;

    private void Awake()
    {
        status = GetComponentInChildren<UIStatus>();
        inventory = GetComponentInChildren<UIInventory>();
    }

    private void Start()
    {
        GameManager.Instance.player.playerResource.AddHealthChangeEvent(status.ChangeHp);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.Toggle();
        }
    }
}
