using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public PlayerController Player { get; private set; }
    public LevelManager CurrentLevelManager { get; private set; }

    private GameManagerStateMachine stateMachine;
    public string currentSceneName { get; private set; }

    public override void Awake()
    {
        base.Awake();
        stateMachine = new();

        // load mainMenu scene if game started from manager scene
        if (SceneManager.sceneCount == 1)
        {
            LoadScene(Level.mainMenu);
            stateMachine.ChangeState(stateMachine.gameMainMenuState);
        }
    }

    /// <summary>
    /// Start a new game, will load level01 by default
    /// </summary>
    public void LoadLevel(string levelName = Level.level01)
    {
        Debug.Log($"Load Level {levelName}");
        UnloadCurrentScene();
        LoadScene(levelName);
        stateMachine.ChangeState(stateMachine.gamePlayingState);
    }

    /// <summary>
    /// Load the latest saved game
    /// </summary>
    public void ContinueGame()
    {
        Debug.Log("Continue Game!");
    }

    public void LoadGame()
    {

    }

    /// <summary>
    /// Load and attach a scene to manager scene
    /// </summary>
    /// <param name="newScene">The name of scene to load</param>
    private void LoadScene(string newScene)
    {
        StartCoroutine(LoadSceneRoutine(newScene));
    }

    /// <summary>
    /// Unload a attached scene
    /// </summary>
    private void UnloadCurrentScene()
    {
        StartCoroutine(UnloadSceneRoutine());
    }
    

    private IEnumerator LoadSceneRoutine(string newScene)
    {
        currentSceneName = newScene;
        // todo: show a loading screen

        AsyncOperation op = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
        while (!op.isDone)
        {
            yield return null;
        }

        // Active new scene
        Scene loadedScene = SceneManager.GetSceneByName(newScene);
        SceneManager.SetActiveScene(loadedScene);

        
    }

    private IEnumerator UnloadSceneRoutine()
    {
        // todo: show a loading screen
        
        if (!string.IsNullOrEmpty(currentSceneName))
        {
            yield return SceneManager.UnloadSceneAsync(currentSceneName);
            currentSceneName = null;
        }
    }

    public void InitializeCurrentScene()
    {
        CurrentLevelManager = FindAnyObjectByType<LevelManager>();

        if (CurrentLevelManager != null)
        {
            Debug.Log($"Loaded Level: {SceneManager.GetActiveScene().name}");
        }
        else
        {
            Debug.LogError($"[GameManager] LevelManager not found in scene: {SceneManager.GetActiveScene().name}");
            ForceQuit();
            return;
        }

        Player = FindAnyObjectByType<PlayerController>();
        if (Player != null)
        {
            Debug.Log("[GameManager] Found Player in scene!");
        }
        else
        {
            Debug.LogError($"[GameManager] Player not found in scene: {SceneManager.GetActiveScene().name}");
            ForceQuit();
            return;
        }

        CurrentLevelManager.OnLevelStart();
    }

    void Update()
    {
        stateMachine.Update();
    }

    private void ForceQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
