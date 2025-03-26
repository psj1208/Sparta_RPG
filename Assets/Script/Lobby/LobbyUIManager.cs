using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] Button StageSelectButton;
    [SerializeField] GameObject stageSelectPanel;

    void Start()
    {
        StageSelectButton.onClick.AddListener(() => stageSelectPanel.SetActive(!stageSelectPanel.activeSelf));
        stageSelectPanel.SetActive(false);
    }
}
