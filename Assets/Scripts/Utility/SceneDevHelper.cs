using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// load manager scene when game started from level directly
/// </summary>
public class SceneDevHelper : MonoBehaviour
{

#if UNITY_EDITOR
    private void Awake()
    {
        if (SceneManager.GetSceneByName(Level.managerScene).isLoaded == false)
        {
            Debug.Log("[SceneDevHelper] Manager scene not loaded. Loading now...");
            StartCoroutine(LoadManagerAndInitialize());
        }
    }

    IEnumerator LoadManagerAndInitialize()
    {
        Scene oldScnee = gameObject.scene;
        SceneManager.LoadScene(Level.managerScene, LoadSceneMode.Additive);
        yield return null;
        GameManager.Instance.LoadLevel(SceneManager.GetActiveScene().name);
        SceneManager.UnloadSceneAsync(oldScnee);
    }
#endif
}