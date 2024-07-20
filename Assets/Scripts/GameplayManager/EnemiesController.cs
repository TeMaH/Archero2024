using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemiesController : MonoBehaviour
{
    public UnityAction AllEnemiesDied;

    [SerializeField] private EnemyController _enemyPrefab;

    private List<EnemyController> _enemies = new List<EnemyController>();

    public void Init(List<Transform> enemySpots)
    {
        if (enemySpots.Count == 0)
        {
            AllEnemiesDied?.Invoke();
            return;
        }

        for (int i = 0; i < enemySpots.Count; i++)
        {
            var enemy = Instantiate(_enemyPrefab);

            var characterController = enemy.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false;
                enemy.transform.position = enemySpots[i].transform.position;
                enemy.transform.rotation = enemySpots[i].transform.rotation;
                characterController.enabled = true;
            }
            else
            {
                enemy.transform.position = enemySpots[i].transform.position;
                enemy.transform.rotation = enemySpots[i].transform.rotation;
            }
            
            _enemies.Add(enemy);

            if (enemy.TryGetComponent<HealthComponent>(out var hc))
            {
                hc.Init();
                hc.PlayerDied += EnemyDie;
            }

            if (enemySpots.Count == 1)
            {
                enemy.TimeToUpdateDirection = 10000000f;
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
