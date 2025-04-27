using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private SpawnPoint[] spawnPoints;

    private void Start()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            spawnPoint.SpawnEnemy(_playerPrefab);
        }
    }
}
