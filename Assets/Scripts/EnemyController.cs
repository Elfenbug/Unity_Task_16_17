using System.Collections.Generic;
using UnityEngine;
using static EnemyStates;

public class EnemyController : MonoBehaviour
{
    private const float AggroRadius = 5f;
    private const float MoveSpeed = 3f;
    private const float RotationSpeed = 5f;
    private const float MinDistanceToTarget = 0.5f;
    private const float ChangeInterval = 1f;
    private const float AggroCheckInterval = 0.1f;

    private GameObject _player;
    private IdleBehaviorType _idleBehavior;
    private ReactionBehaviorType _reactionBehavior;
    private Transform[] _patrolPoints;

    private bool _isAggro = false;
    private Vector3 _randomDirection;
    private float _directionChangeTimer;
    private float _aggroCheckTimer;
    private Vector3 _currentTarget;
    private Queue<Vector3> _targetsQueue;

    public void Initialize(GameObject player, IdleBehaviorType idleBehavior, ReactionBehaviorType reactionBehavior, Transform[] patrolPoints)
    {
        _player = player;
        _idleBehavior = idleBehavior;
        _reactionBehavior = reactionBehavior;
        _patrolPoints = patrolPoints;

        _targetsQueue = new Queue<Vector3>();

        foreach (var point in _patrolPoints)
        {
            _targetsQueue.Enqueue(point.position);
        }

        _currentTarget = _targetsQueue.Dequeue();
        _targetsQueue.Enqueue(_currentTarget);

    }

    private void Update()
    {
        _aggroCheckTimer -= Time.deltaTime;
        if (_aggroCheckTimer <= 0)
        {
            CheckPlayerDistance();
            _aggroCheckTimer = AggroCheckInterval;
        }

        if (_isAggro)
        {
            ReactToPlayer();
        }
        else
        {
            IdleBehavior();
        }
    }

    private void CheckPlayerDistance()
    {
        if (_player == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance <= AggroRadius && !_isAggro)
        {
            _isAggro = true;
        }
        else if (distance > AggroRadius && _isAggro)
        {
            _isAggro = false;
        }
    }

    private void IdleBehavior()
    {
        switch (_idleBehavior)
        {
            case IdleBehaviorType.Stand:
                break;

            case IdleBehaviorType.Patrol:
                PatrolBetweenPoints();
                break;

            case IdleBehaviorType.RandomMovement:
                MoveRandom();
                break;
        }
    }

    private void ReactToPlayer()
    {
        switch (_reactionBehavior)
        {
            case ReactionBehaviorType.RunAway:
                RunAway();
                break;

            case ReactionBehaviorType.ChasePlayer:
                Chase();
                break;

            case ReactionBehaviorType.Die:
                Die();
                break;
        }
    }

    private void PatrolBetweenPoints()
    {
        Vector3 direction = _currentTarget - transform.position;

        if (direction.magnitude <= MinDistanceToTarget)
        {
            SwitchTarget();
        }

        Vector3 normalizedDirection = direction.normalized;

        ProcessMoveTo(normalizedDirection);
        ProcessRotateTo(normalizedDirection);
    }

    private void MoveRandom()
    {
        _directionChangeTimer -= Time.deltaTime;

        if (_directionChangeTimer <= 0)
        {
            _randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            _directionChangeTimer = ChangeInterval;
        }

        ProcessMoveTo(_randomDirection);
        ProcessRotateTo(_randomDirection);
    }

    private void RunAway()
    {
        Vector3 direction = transform.position - _player.transform.position;
        direction.y = 0;
        direction.Normalize();

        ProcessMoveTo(direction);
        ProcessRotateTo(direction);
    }

    private void Chase()
    {
        Vector3 direction = _player.transform.position - transform.position;
        direction.y = 0;
        direction.Normalize();

        ProcessMoveTo(direction);
        ProcessRotateTo(direction);
    }

    private void Die()
    {
        ParticleSystem particle = gameObject.GetComponent<ParticleSystem>();
        particle.Play();

        Destroy(gameObject, particle.main.duration);
    }

    private void ProcessMoveTo(Vector3 direction)
    {
        transform.Translate(direction * MoveSpeed * Time.deltaTime, Space.World);
    }

    private void ProcessRotateTo(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            float step = RotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, step);
        }
    }

    private void SwitchTarget()
    {
        _currentTarget = _targetsQueue.Dequeue();
        _targetsQueue.Enqueue(_currentTarget);
    }
}