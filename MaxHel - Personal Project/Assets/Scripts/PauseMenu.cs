using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        resumeButton.onClick.AddListener(HandleResumeClicked);
        optionButton.onClick.AddListener(HandleOptionClicked);
        restartButton.onClick.AddListener(HandleRestartClicked);
        quitButton.onClick.AddListener(HandleQuitClicked);
    }

    private void HandleResumeClicked()
    {
        GameManager.Instance.TogglePause();
    }

    private void HandleOptionClicked()
    {
        GameManager.Instance.ToggleOption();
    }

    private void HandleRestartClicked()
    {
        GameManager.Instance.RestartGame();
    }

    private void HandleQuitClicked()
    {
        GameManager.Instance.QuitGame();
    }
}
