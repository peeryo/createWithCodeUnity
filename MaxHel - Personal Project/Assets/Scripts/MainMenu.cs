using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animation mainMenuAnimation;
    [SerializeField] private AnimationClip fadeOutAnimationClip;
    [SerializeField] private AnimationClip fadeInAnimationClip;

    public Events.EventFadeComplete onMainMenuFadeComplete;

    private void Start()
    {
        GameManager.Instance.onGameStateChanged.AddListener(HandleGameStateChanged);
    }

    public void OnFadeOutComplete()
    {
        onMainMenuFadeComplete.Invoke(true);
    }

    public void OnFadeInComplete()
    {
        onMainMenuFadeComplete.Invoke(false);
        UIManager.Instance.SetDummyCameraActive(true);
    }

    private void FadeIn()
    {
        mainMenuAnimation.Stop();
        mainMenuAnimation.clip = fadeInAnimationClip;
        mainMenuAnimation.Play();
    }

    private void FadeOut()
    {
        UIManager.Instance.SetDummyCameraActive(false);
        
        mainMenuAnimation.Stop();
        mainMenuAnimation.clip = fadeOutAnimationClip;
        mainMenuAnimation.Play();
    }

    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (previousState == GameManager.GameState.PREGAME && currentState == GameManager.GameState.RUNNING)
        {
            FadeOut();
        }

        if (previousState != GameManager.GameState.PREGAME && currentState == GameManager.GameState.PREGAME)
        {
            FadeIn();
        }
    }
}
