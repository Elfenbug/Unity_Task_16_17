using UnityEngine;

public class DieBehavior : IEnemyBehavior
{
    public void Execute(Transform enemy, Transform player)
    {
        ParticleSystem particles = enemy.GetComponent<ParticleSystem>();

        if (particles != null)
        {
            particles.Play();
            GameObject.Destroy(enemy.gameObject, particles.main.duration);
        }
        else
        {
            GameObject.Destroy(enemy.gameObject);
        }
    }
}
