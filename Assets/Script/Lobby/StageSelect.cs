using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] Button exitButton;

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int stageIndex = i;
            buttons[i].onClick.AddListener(() =>
            {
                SceneData.stage = stageIndex;
                SceneManager.LoadScene("GameScene");
            });
        }
        exitButton.onClick.AddListener(() => gameObject.SetActive(false));
    }
}
