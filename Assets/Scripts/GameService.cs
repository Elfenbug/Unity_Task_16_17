using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private SpawnPoint[] _spawnPoints;

    private void Start()
    {
        Transform player = Instantiate(_playerPrefab).transform;

        foreach (var point in _spawnPoints)
        {
            if (point != null)
            {
                point.SpawnEnemy(player);
            }
        }
    }
}
