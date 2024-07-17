using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemiesController : MonoBehaviour
{
    public UnityAction AllEnemiesDied;

    [SerializeField] private List<Transform> _enemySpots;
    [SerializeField] private EnemyController _enemyPrefab;

    private List<EnemyController> _enemies = new List<EnemyController>();

    public void Init()
    {
        for (int i = 0; i < _enemySpots.Count; i++)
        {
            var enemy = Instantiate(_enemyPrefab);
            enemy.transform.position = _enemySpots[i].transform.position;
            _enemies.Add(enemy);

            if (enemy.TryGetComponent<HealthComponent>(out var hc))
            {
                hc.Init();
                hc.PlayerDied += EnemyDie;
            }
        }
    }

    public void Refresh()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].TryGetComponent<HealthComponent>(out var hc))
            {
                hc.PlayerDied -= EnemyDie;
            }

            Destroy(_enemies[i].gameObject);
        }

        _enemies.Clear();
    }

    private void EnemyDie(GameObject enemy)
    {
        if (enemy.TryGetComponent<HealthComponent>(out var hc))
        {
            hc.PlayerDied -= EnemyDie;
        }

        if (enemy.TryGetComponent<EnemyController>(out var ec))
        {
            _enemies.Remove(ec);            
        }

        Destroy(hc.gameObject);

        if (_enemies.Count == 0)
        {
            AllEnemiesDied.Invoke();
        }
    }
}
