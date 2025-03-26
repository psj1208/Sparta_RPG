using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResource : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private StatHandler statHandler;

    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => statHandler.GetStat(StatType.Health);

    private Action<float, float> OnChangeHealth;

    private void Awake()
    {
        statHandler = GetComponent<StatHandler>();
    }
    private void Start()
    {
        CurrentHealth = statHandler.GetStat(StatType.Health);
    }

    private void Update()
    {
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);

        if (change < 0)
        {
            //데미지 받앗을 때 호출
        }

        if (CurrentHealth <= 0f)
        {
            //죽음
            Death();
        }

        return true;
    }

    private void Death()
    {
        
    }

    public void AddHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth += action;
    }

    public void RemoveHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth -= action;
    }
}
