using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
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
    [SerializeField] float detectRadius;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float AttackRange;

    [Header("Attribute")]
    public AnimationController controller;
    public StatHandler statHandler;
    public PlayerResource playerResource;

    private void Awake()
    {
        controller = GetComponent<AnimationController>();
        agent = GetComponent<NavMeshAgent>();
        statHandler = GetComponent<StatHandler>();
        playerResource = GetComponent<PlayerResource>();
        GameManager.Instance.player = this;
    }

    private void Update()
    {
        RenewState();
        switch(actionType)
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

    void Idle()
    {
        agent.velocity = Vector3.zero;
    }

    void RenewState()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectRadius, enemyLayer);
        if (hits.Length != 0) 
        {
            target = hits[0].transform;
            float minDistance = Vector3.Distance(transform.position, target.position);

            foreach (Collider col in hits)
            {
                float dist = Vector3.Distance(transform.position, col.transform.position);
                if (dist < minDistance)
                {
                    target = col.transform;
                    minDistance = dist;
                }
            }

            if (Vector3.Distance(transform.position, target.transform.position) <= AttackRange)
                actionType = ActionType.Attack;
            else
                actionType = ActionType.Chase;
        }
        else if (GameManager.Instance.EnemyManager.CurSpawnTransform != null)
        {
            actionType = ActionType.Chase;
            target = GameManager.Instance.EnemyManager.CurSpawnTransform;
        }
        else
        {
            target = null;
        }
    }
    void Move()
    {
        if (target == null || controller.IsAnimationing)
        {
            agent.ResetPath();
            agent.velocity = Vector3.zero;
            return;
        }
        agent.speed = statHandler.GetStat(StatType.Speed);
        agent.SetDestination(target.position);
    }

    void Attack()
    {
        agent.velocity = Vector3.zero;
        controller.setAnimation(AnimationType.Attack);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

    public void AttackMethod()
    {
        target.GetComponent<EnemyResource>().ChangeHealth(-statHandler.GetStat(StatType.Atk));
    }
}
