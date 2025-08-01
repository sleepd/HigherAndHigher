using UnityEngine;
using UnityEngine.UIElements;

public class MainManuUIController : MonoBehaviour
{
    Button bt_Continue;
    Button bt_StartGame;
    Button bt_Options;
    Button bt_ExitGame;
    void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        bt_Continue = root.Q<Button>("ContinueButton");
        bt_StartGame = root.Q<Button>("StartGameButton");
        bt_Options = root.Q<Button>("OptionsButton");
        bt_ExitGame = root.Q<Button>("ExitGameButton");

        bt_Continue.clicked += OnContinueClicked;
        bt_StartGame.clicked += OnStartGameClicked;
        bt_Options.clicked += OnOptionsClicked;
        bt_ExitGame.clicked += OnExitClicked;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnContinueClicked()
    {
        GameManager.Instance.ContinueGame();
    }

    void OnStartGameClicked()
    {
        GameManager.Instance.LoadLevel();
    }

    void OnOptionsClicked()
    {

    }

    void OnExitClicked()
    {

    }

    void OnDisable()
    {
        bt_Continue.clicked -= OnContinueClicked;
        bt_StartGame.clicked -= OnStartGameClicked;
        bt_Options.clicked -= OnOptionsClicked;
        bt_ExitGame.clicked -= OnExitClicked;
    }
}
