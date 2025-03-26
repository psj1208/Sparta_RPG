using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    UIStatus status;

    private void Awake()
    {
        status = GetComponentInChildren<UIStatus>();
    }

    private void Start()
    {
        GameManager.Instance.player.playerResource.AddHealthChangeEvent(status.ChangeHp);
    }
}
