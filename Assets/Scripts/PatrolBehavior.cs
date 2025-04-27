using System.Collections.Generic;
using UnityEngine;

public class PatrolBehavior : IEnemyBehavior
{
    private const float MoveSpeed = 3f;
    private const float RotationSpeed = 5f;
    private const float MinDistance = 0.5f;

    private Queue<Vector3> _points;
    private Vector3 _currentTarget;

    public PatrolBehavior(Transform[] patrolPoints)
    {
        _points = new Queue<Vector3>();

        foreach (var point in patrolPoints)
        {
            if (point != null)
            {
                _points.Enqueue(point.position);
            }
        }

        if (_points.Count > 0)
        {
            _currentTarget = _points.Dequeue();
        }
    }

    public void Execute(Transform enemy, Transform player)
    {
        if (_points == null || _points.Count == 0)
        {
            return;
        }

        Vector3 direction = _currentTarget - enemy.position;

        if (direction.magnitude <= MinDistance)
        {
            _points.Enqueue(_currentTarget);
            _currentTarget = _points.Dequeue();
        }

        Move(enemy, direction.normalized);
    }

    private void Move(Transform enemy, Vector3 direction)
    {
        enemy.Translate(direction * MoveSpeed * Time.deltaTime, Space.World);

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            enemy.rotation = Quaternion.Slerp(enemy.rotation, rotation, RotationSpeed * Time.deltaTime);
        }
    }
}
