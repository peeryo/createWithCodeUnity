using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // Enum representing the different states of the game
    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED
    }

    // Current Game State of the program
    public GameState CurrentGameState { get; private set; } = GameState.PREGAME;
    // A list of eventual other game systems like Audio System of UI System
    public GameObject[] systemPrefabs;
    // Reference to the event of Game State changing
    public Events.EventGameState onGameStateChanged;

    // List of actually instanced Game Object
    private List<GameObject> instancedSystemPrefabs;
    // List of Game Objects in process of instanciation
    private List<AsyncOperation> loadOperations;
    // Current player level 
    private string currentLevelName = string.Empty;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        instancedSystemPrefabs = new List<GameObject>();
        loadOperations = new List<AsyncOperation>();

        InstanciateSystemPrefabs();
        
        UIManager.Instance.onMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
    }

    private void Update()
    {
        if (CurrentGameState == GameState.PREGAME)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);

            if (loadOperations.Count == 0)
            {
                UpdateState(GameState.RUNNING);
            }
        }
        
        Debug.Log("Load Complete.");
    }

    private void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Complete.");
    }

    private void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.Log("[GameManager] Unable to load level " + levelName);
            return;
        }

        ao.completed += OnLoadOperationComplete;
        loadOperations.Add(ao);

        currentLevelName = levelName;
    }

    private void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.Log("[GameManager] Unable to unload level " + levelName);
            return;
        }

        ao.completed += OnUnloadOperationComplete;
    }

    private void InstanciateSystemPrefabs()
    {
        foreach (var obj in systemPrefabs)
        {
            var prefabInstance = Instantiate(obj);
            instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        foreach (var obj in instancedSystemPrefabs)
        {
            Destroy(obj);
        }
        instancedSystemPrefabs.Clear();
    }

    private void UpdateState(GameState state)
    {
        var previousGameState = CurrentGameState;
        CurrentGameState = state;

        switch (CurrentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1.0f;
                break;
            
            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                break;
            
            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;

            default:
                break;
        }
        
        onGameStateChanged.Invoke(CurrentGameState, previousGameState);
    }

    void HandleMainMenuFadeComplete(bool fadeOut)
    {
        if (!fadeOut)
        {
            UnloadLevel(currentLevelName);
        }
    }

    public void StartGame()
    {
        LoadLevel("Main");
    }

    public void TogglePause()
    {
        UpdateState(CurrentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    }

    public void RestartGame()
    {
        UpdateState(GameState.PREGAME);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
