using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Button StartRestart;
    public Button MainMenu;

    [SerializeField] private TMP_Text _startButtonText;

    public void SetEnabledForButtons(bool value, string startRestartText = "Start")
    {
        StartRestart.gameObject.SetActive(value);
        _startButtonText.text = startRestartText;
        MainMenu.gameObject.SetActive(value);
    }
}
