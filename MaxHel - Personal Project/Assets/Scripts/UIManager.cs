using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private OptionMenu optionMenu;

    [SerializeField] private Camera dummyCamera;

    public Events.EventFadeComplete onMainMenuFadeComplete;
    public bool isOption;

    private void Start()
    {
        mainMenu.onMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
        GameManager.Instance.onGameStateChanged.AddListener(HandleGameStateChange);
    }

    private void HandleGameStateChange(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSED);
    }

    public void ToggleOption()
    {
        optionMenu.gameObject.SetActive(!isOption);
        isOption = !isOption;
    }

    private void HandleMainMenuFadeComplete(bool fadeOut)
    {
        onMainMenuFadeComplete.Invoke(fadeOut);
    }
    
    public void SetDummyCameraActive(bool active)
    {
        dummyCamera.gameObject.SetActive(active);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.StartGame();
        }
    }
}
