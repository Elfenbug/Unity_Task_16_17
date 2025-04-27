using UnityEngine;

public class RandomMoveBehavior : IEnemyBehavior
{
    private const float MoveSpeed = 3f;
    private const float RotationSpeed = 5f;
    private const float ChangeTime = 1f;

    private Vector3 _direction;
    private float _timer;

    public RandomMoveBehavior()
    {
        _timer = ChangeTime;
        _direction = GetNewDirection();
    }

    public void Execute(Transform enemy, Transform player)
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _direction = GetNewDirection();
            _timer = ChangeTime;
        }

        Move(enemy, _direction);
    }

    private Vector3 GetNewDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
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
