using UnityEngine;

public interface IEnemyBehavior
{
    void Execute(Transform enemy, Transform player);
}
