using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    [Header("Stage About")]
    [SerializeField] Button StageSelectButton;
    [SerializeField] GameObject stageSelectPanel;

    [Header("Inven About")]
    [SerializeField] Button InvenButton;
    [SerializeField] UIInventory inven;

    [Header("Shop About")]
    [SerializeField] Button shopButton;
    [SerializeField] Shop shop;

    private void Awake()
    {
        inven = GetComponentInChildren<UIInventory>();
        shop = GetComponentInChildren<Shop>();
    }

    void Start()
    {
        StageSelectButton.onClick.AddListener(() => stageSelectPanel.SetActive(!stageSelectPanel.activeSelf));
        stageSelectPanel.SetActive(false);
        InvenButton.onClick.AddListener(() =>
        {
            inven.UpdateUI();
            inven.Toggle();
        });
        shopButton.onClick.AddListener(() => shop.Toggle());
    }
}
