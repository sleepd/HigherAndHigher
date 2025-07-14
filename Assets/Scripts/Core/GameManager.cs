using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerController Player { get; private set; }
    public LevelManager CurrentLevelManager { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Initialize scene except it's not a game scene
        if (IsGameScene(SceneManager.GetActiveScene()))
        {
            InitializeCurrentScene();
        }
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

    private bool IsGameScene(Scene scene)
    {
        return scene.name.StartsWith("Level");
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
