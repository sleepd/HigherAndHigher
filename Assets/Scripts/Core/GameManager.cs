using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public PlayerController Player { get; private set; }
    public LevelManager CurrentLevelManager { get; private set; }

    private GameManagerStateMachine stateMachine;

    public override void Awake()
    {
        base.Awake();
        stateMachine = new();
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeCurrentScene();
        SceneManager.sceneLoaded -= OnLevelLoaded;
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
