using UnityEngine;
using static EnemyStates;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private IdleBehaviorType idleBehavior;
    [SerializeField] private ReactionBehaviorType reactionBehavior;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform[] patrolPoints;

    public void SpawnEnemy(GameObject player)
    {
        GameObject enemy = Instantiate(_enemyPrefab);
        enemy.transform.position = transform.position;

        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.Initialize(player, idleBehavior, reactionBehavior, patrolPoints);
    }
}
