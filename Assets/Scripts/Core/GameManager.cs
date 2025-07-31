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
        stateMachine.ChangeState(stateMachine.gameMainMenuState);
    }

    public void LoadScene(string newScene)
    {
        StartCoroutine(LoadSceneRoutine(newScene));
    }

    public void UnloadCurrentScene()
    {
        StartCoroutine(UnloadSceneRoutine());
    }
    

    private IEnumerator LoadSceneRoutine(string newScene)
    {
        // todo: show a loading screen

        AsyncOperation op = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
        while (!op.isDone)
        {
            yield return null;
        }

        // Active new scene
        Scene loadedScene = SceneManager.GetSceneByName(newScene);
        SceneManager.SetActiveScene(loadedScene);

        currentSceneName = newScene;
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

    private void InitializeCurrentScene()
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
