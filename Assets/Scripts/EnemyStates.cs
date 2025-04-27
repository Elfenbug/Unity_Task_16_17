using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    public enum IdleBehaviorType
    {
        Stand,
        Patrol,
        RandomMovement
    }

    public enum ReactionBehaviorType
    {
        RunAway,
        ChasePlayer,
        Die
    }
}
