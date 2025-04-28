using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private IdleBehaviorType _idleType;
    [SerializeField] private ReactionBehaviorType _reactionType;
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private EnemyController _enemyPrefab;

    public EnemyController SpawnEnemy(Transform player)
    {
        EnemyController enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);

        enemy.Initialize(player, CreateIdleBehavior(_idleType), CreateReactionBehavior(_reactionType));

        return enemy;
    }

    private IEnemyBehavior CreateIdleBehavior(IdleBehaviorType type)
    {
        switch (type)
        {
            case IdleBehaviorType.Patrol:
                return new PatrolBehavior(_patrolPoints);

            case IdleBehaviorType.RandomMove:
                return new RandomMoveBehavior();

            default:
                return new StandBehavior();
        }
    }

    private IEnemyBehavior CreateReactionBehavior(ReactionBehaviorType type)
    {
        switch (type)
        {
            case ReactionBehaviorType.RunAway:
                return new RunAwayBehavior();

            case ReactionBehaviorType.Chase:
                return new ChaseBehavior();

            default:
                return new DieBehavior();
        }
    }
}
