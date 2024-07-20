using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public UnityAction<Environment> LevelLoaded;
    public UnityAction LevelsFinished;

    [SerializeField] private List<Environment> _levelPrefabs;

    private Transform _playerStartPosition;
    private int _currentLevelIndex = -1;
    private Environment _currentLevelInstance;
    private GameObject _player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Refresh()
    {
        _currentLevelIndex = -1;
    }

    private void OnPlayerEnteredPortal()
    {
        _currentLevelIndex++;

        if (_currentLevelIndex < _levelPrefabs.Count)
        {
            StartCoroutine(LoadNextLevelCoroutine());
        }
        else
        {
            Debug.Log("Все уровни завершены!");
            LevelsFinished?.Invoke();
        }
    }

    private IEnumerator LoadNextLevelCoroutine()
    {
        if (_currentLevelInstance != null)
        {
            Destroy(_currentLevelInstance.gameObject);
        }

        _currentLevelInstance = Instantiate(_levelPrefabs[_currentLevelIndex]);

        ExitPortal exitPortal = _currentLevelInstance.ExitPortal;

        if (exitPortal != null)
        {
            exitPortal.SomeoneEntered += OnPlayerEnteredPortal;
        }
        else
        {
            Debug.LogWarning("ExitPortal не найден в сцене.");
        }

        MovePlayerToStartPosition();

        LevelLoaded?.Invoke(_currentLevelInstance);

        yield return null;
    }

    public void LoadNextLevel()
    {
        _currentLevelIndex++;

        if (_currentLevelIndex < _levelPrefabs.Count)
        {
            StartCoroutine(LoadNextLevelCoroutine());
        }
        else
        {
            Debug.Log("Все уровни завершены!");
        }
    }

    private void MovePlayerToStartPosition()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        _playerStartPosition = _currentLevelInstance.PlayerSpot;

        if (_playerStartPosition != null && _player != null)
        {
            CharacterController characterController = _player.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false;
                _player.transform.position = _playerStartPosition.position;
                _player.transform.rotation = _playerStartPosition.rotation;
                characterController.enabled = true;
            }
            else
            {
                _player.transform.position = _playerStartPosition.position;
                _player.transform.rotation = _playerStartPosition.rotation;
            }
        }
        else
        {
            Debug.LogWarning("Не установлена начальная позиция игрока или игрок не найден в сцене.");
        }
    }
}
