using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float chaseSpeed;
    [SerializeField] int maxHealth;
    [SerializeField] float detectRadius;
    [SerializeField] List<Vector3> waypoints;
    [SerializeField] float attackRange;
    [SerializeField] Animator animator;

    private EnemyState _state;
    public EnemyState State { get => _state; }

    public enum EnemyState
    {
        idle,
        Patrol,
        Chase,
        Attack
    }

    private int _currentHealth;
    private CharacterController _characterController;
    private NavMeshAgent _navMeshAgent;
    private int _targetWaypointIndex = 1;
    private bool isAttackAnimationPlaying = false;
    // private PlayerController GameManager.Instance.Player;



    void OnEnable()
    {
        _characterController = GetComponent<CharacterController>();
        _state = EnemyState.idle;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        // GameManager.Instance.Player = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case EnemyState.idle:
                HandleIdle();
                break;
            case EnemyState.Patrol:
                HandlePatrol();
                break;
            case EnemyState.Chase:
                HandleChase();
                break;
            case EnemyState.Attack:
                HandleAttack();
                break;
        }
    }

    void HandleIdle()
    {
        if (waypoints.Count > 0)
        {
            EnterPatrol();
        }
    }

    void HandlePatrol()
    {
        if (IsPlayerInSight())
        {
            EnterChase();
            return;
        }

        float distance = Vector3.Distance(transform.position, waypoints[_targetWaypointIndex]);
        if (distance < 0.2f)
        {
            _targetWaypointIndex = (_targetWaypointIndex + 1) % waypoints.Count;
            Debug.Log("Next waypoint: " + _targetWaypointIndex);
            _navMeshAgent.SetDestination(waypoints[_targetWaypointIndex]);
        }

    }

    void HandleChase()
    {
        _navMeshAgent.SetDestination(GameManager.Instance.Player.transform.position);

        if (IsPlayerInAttackRange())
        {
            EnterAttack();
        }

        // do a player detect check at here
    }

    void HandleAttack()
    {
        if (!IsPlayerInAttackRange() && !isAttackAnimationPlaying)
        {
            EnterChase();
            return;
        }

        if (isAttackAnimationPlaying) return;

        Attack();
    }

    void Die()
    {
        Debug.Log("Enemey dead!");
    }

    bool IsPlayerInSight()
    {
        if (Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) > detectRadius) return false;

        if (Vector3.Dot(transform.forward, GameManager.Instance.Player.transform.position - transform.position) < -0.5f) return false;

        return true;
    }

    bool IsPlayerInAttackRange()
    {
        if (Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) < attackRange) return true;
        else return false;
    }

    void OnAttackAnimationFinished()
    {
        isAttackAnimationPlaying = false;
    }

    void EnterIdle()
    {

    }

    void EnterPatrol()
    {
        _state = EnemyState.Patrol;
        animator.SetFloat("Speed", 0.5f);
        _navMeshAgent.speed = moveSpeed;
        _navMeshAgent.SetDestination(waypoints[_targetWaypointIndex]);
    }

    void EnterChase()
    {
        Debug.Log("Enemy state: Chase");
        _navMeshAgent.isStopped = false;
        _state = EnemyState.Chase;
        animator.SetFloat("Speed", 1f);
        _navMeshAgent.speed = chaseSpeed;
    }

    void EnterAttack()
    {
        Debug.Log("Enemey state: Attack");
        _navMeshAgent.isStopped = true;
        _state = EnemyState.Attack;
        Attack();
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        isAttackAnimationPlaying = true;
    }
}
