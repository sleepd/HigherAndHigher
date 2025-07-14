using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] int maxHealth;
    [SerializeField] float detectRadius;
    [SerializeField] List<Vector3> waypoints;
    [SerializeField] float attackRange;

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



    void OnEnable()
    {
        _characterController = GetComponent<CharacterController>();
        _state = EnemyState.idle;
        _navMeshAgent = GetComponent<NavMeshAgent>();
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
        Debug.Log("Enemy state: Idle");

        if (waypoints.Count > 0)
        {
            _state = EnemyState.Patrol;
            _navMeshAgent.SetDestination(waypoints[_targetWaypointIndex]);
        }
    }

    void HandlePatrol()
    {
        if (IsPlayerInSight())
        {
            _state = EnemyState.Chase;
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
        Debug.Log("Enemy state: Chase");
        _navMeshAgent.SetDestination(GameManager.Instance.player.transform.position);

        if (IsPlayerInAttackRange())
        {
            _state = EnemyState.Attack;
        }

        // do a player detect check at here
    }

    void HandleAttack()
    {
        if (!IsPlayerInAttackRange() && !isAttackAnimationPlaying)
        {
            _state = EnemyState.Chase;
            return;
        }

        if (isAttackAnimationPlaying) return;

        Debug.Log("Enemey state: Attack");
        // tell animator play the attack animation
        isAttackAnimationPlaying = true;

        
    }

    void Die()
    {
        Debug.Log("Enemey dead!");
    }

    bool IsPlayerInSight()
    {
        if (Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) > detectRadius) return false;

        if (Vector3.Dot(transform.forward, GameManager.Instance.player.transform.position - transform.position) < -0.5f) return false;

        return true;
    }

    bool IsPlayerInAttackRange()
    {
        if (Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) < attackRange) return true;
        else return false;
    }

    void OnAttackAnimationFinished()
    {
        isAttackAnimationPlaying = false;
    }
}
