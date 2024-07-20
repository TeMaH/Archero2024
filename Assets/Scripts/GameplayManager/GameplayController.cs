using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private CanvasController _canvas;

    [SerializeField] private HealthComponent _playerPrefab;

    [SerializeField] private EnemiesController _enemiesController;

    private HealthComponent _player;
    private Environment _currentLevel;

    private void Start()
    {
        _canvas.SetEnabledForButtons(true);
    }

    private void StartGame()
    {
        _canvas.SetEnabledForButtons(false);

        if (_player == null)
        {
            _player = Instantiate(_playerPrefab);
            _player.Init();
            _player.PlayerDied += PlayerLose;
        }

        _player.gameObject.SetActive(true);

        LevelManager.Instance.LoadNextLevel();
    }

    private void StartLevel(Environment level)
    {
        _currentLevel = level;

        _enemiesController.Init(level.EnemySpots);
    }

    private void PlayerWin()
    {
        //Logic for winning
        _currentLevel.ExitPortal.SetDoorIsActive(false);
    }

    private void PlayerLose(GameObject player)
    {
        //Logic for loosing
        _player.PlayerDied -= PlayerLose;
        //Destroy(player);
        StopGame();
    }
    private void StopGame()
    {
        _canvas.SetEnabledForButtons(true, "Restart");
        _enemiesController.Refresh();
        LevelManager.Instance.Refresh();
        _player.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _canvas.StartRestart.onClick.AddListener(StartGame);

        _enemiesController.AllEnemiesDied += PlayerWin;
        LevelManager.Instance.LevelLoaded += StartLevel;
        LevelManager.Instance.LevelsFinished += StopGame;
    }

    private void OnDisable()
    {
        _canvas.StartRestart.onClick.RemoveListener(StartGame);

        _enemiesController.AllEnemiesDied -= PlayerWin;
        LevelManager.Instance.LevelLoaded -= StartLevel;
        LevelManager.Instance.LevelsFinished -= StopGame;
    }
}
