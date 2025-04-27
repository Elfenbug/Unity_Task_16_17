using UnityEngine;

public class RunAwayBehavior : IEnemyBehavior
{
    private const float MoveSpeed = 3f;
    private const float RotationSpeed = 5f;

    public void Execute(Transform enemy, Transform player)
    {
        Vector3 direction = (enemy.position - player.position).normalized;
        Move(enemy, direction);
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
