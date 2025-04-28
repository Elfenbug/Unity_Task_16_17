using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private SpawnPoint[] _spawnPoints;

    private void Start()
    {
        GameObject player = Instantiate(_playerPrefab);
        Transform playerTransform = player.transform;

        foreach (var point in _spawnPoints)
        {
            if (point != null)
            {
                point.SpawnEnemy(playerTransform);
            }
        }
    }
}
