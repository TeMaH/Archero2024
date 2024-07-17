using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private List<GameObject> _levelPrefabs;
    [SerializeField] private Transform _playerStartPosition;

    private int _currentLevelIndex = -1;
    private GameObject _currentLevelInstance;
    private GameObject _player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadNextLevel();
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
        }
    }

    private IEnumerator LoadNextLevelCoroutine()
    {
        if (_currentLevelInstance != null)
        {
            Destroy(_currentLevelInstance);
        }

        _currentLevelInstance = Instantiate(_levelPrefabs[_currentLevelIndex]);

        ExitPortal exitPortal = FindObjectOfType<ExitPortal>();

        if (exitPortal != null)
        {
            exitPortal.SomeoneEntered += OnPlayerEnteredPortal;
        }
        else
        {
            Debug.LogWarning("ExitPortal не найден в сцене.");
        }

        MovePlayerToStartPosition();

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
