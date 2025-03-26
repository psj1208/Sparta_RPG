using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    enum ActionType
    {
        Idle,
        Chase,
        Attack
    }

    [SerializeField] NavMeshAgent agent;
    [SerializeField] ActionType actionType;
    [SerializeField] Transform target;

    [Header("Combat")]
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float AttackRange;

    [Header("Attribute")]
    public AnimationController controller;
    public StatHandler statHandler;
    public EnemyResource enemyResource;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<AnimationController>();
        statHandler = GetComponent<StatHandler>();
        enemyResource = GetComponent<EnemyResource>();
    }

    private void Update()
    {
        RenewState();
        switch (actionType)
        {
            case ActionType.Idle:
                Idle();
                break;
            case ActionType.Chase:
                Move();
                break;
            case ActionType.Attack:
                Attack();
                break;
        }
        controller.ChangeMovementValue(agent.velocity.magnitude, agent.speed);
    }

    public void SetTarget(Transform trans)
    {
        target = trans;
        agent.SetDestination(target.position);
    }

    private void Idle()
    {
        agent.velocity = Vector3.zero;
    }
    private void Move()
    {
        if (target == null)
        {
            actionType = ActionType.Idle;
            agent.velocity = Vector3.zero;
            return;
        }
        agent.speed = statHandler.GetStat(StatType.Speed);
        agent.SetDestination(target.position);
    }

    private void Attack()
    {
        agent.velocity = Vector3.zero;
        controller.setAnimation(AnimationType.Attack);
    }

    void RenewState()
    {
        if (target != null && agent.hasPath && agent.remainingDistance < AttackRange)
        {
            actionType = ActionType.Attack;
        }
        else if (target != null) 
        {
            actionType = ActionType.Chase;
        }
        else
        {
            actionType = ActionType.Idle;
        }
    }

    public void AttackMethod()
    {
        target.GetComponent<PlayerResource>().ChangeHealth(-statHandler.GetStat(StatType.Atk));
    }
}
