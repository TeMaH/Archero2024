using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private CanvasController _canvas;

    [SerializeField] private HealthComponent _playerPrefab;
    [SerializeField] private Transform _playerSpot;

    [SerializeField] private EnemiesController _enemiesController;

    private HealthComponent _player;
    private void Start()
    {
        _canvas.SetEnabledForButtons(true);
    }

    private void StartGame()
    {
        _canvas.SetEnabledForButtons(false);
        _enemiesController.Init();
        _player = Instantiate(_playerPrefab);
        _player.Init();
        _player.PlayerDied += PlayerLose;
        _player.transform.position = _playerSpot.position;
    }
    private void PlayerWin()
    {
        //Logic for winning
        StopGame();
    }

    private void PlayerLose(GameObject player)
    {
        //Logic for loosing
        _player.PlayerDied -= PlayerLose;
        Destroy(player);
        StopGame();
    }
    private void StopGame()
    {
        _canvas.SetEnabledForButtons(true, "Restart");
        _enemiesController.Refresh();
    }

    private void OnEnable()
    {
        _canvas.StartRestart.onClick.AddListener(StartGame);

        _enemiesController.AllEnemiesDied += PlayerWin;
    }

    private void OnDisable()
    {
        _canvas.StartRestart.onClick.RemoveListener(StartGame);

        _enemiesController.AllEnemiesDied -= PlayerWin;
    }
}
