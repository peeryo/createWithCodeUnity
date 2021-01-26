using UnityEngine.Events;

/**
 * Class grouping different major events that could happened in the process of the game
 */
public class Events
{
    // Event triggered when a screen is fading away 
    [System.Serializable] public class EventFadeComplete : UnityEvent<bool> {}
    
    // Event triggered when a Game State change is happening
    [System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> {}
}
