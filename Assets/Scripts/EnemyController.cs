using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private const float AggroRadius = 5f;

    private Transform _player;
    private IEnemyBehavior _idleBehavior;
    private IEnemyBehavior _reactionBehavior;
    private bool _isAggro;

    public void Initialize(Transform player, IEnemyBehavior idle, IEnemyBehavior reaction)
    {
        _player = player;
        _idleBehavior = idle;
        _reactionBehavior = reaction;
    }

    private void Update()
    {
        if (_player == null) return;

        CheckAggro();

        if (_isAggro)
        {
            if (_reactionBehavior != null)
            {
                _reactionBehavior.Execute(transform, _player);
            }
        }
        else
        {
            if (_idleBehavior != null)
            {
                _idleBehavior.Execute(transform, _player);
            }
        }
    }

    private void CheckAggro()
    {
        if (_player == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, _player.position);

        _isAggro = distance <= AggroRadius;
    }
}