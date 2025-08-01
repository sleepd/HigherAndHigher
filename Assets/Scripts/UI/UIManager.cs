using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : Singleton<GameManager>
{

    VisualElement UI_LoadingScreen;
    public override void Awake()
    {
        base.Awake();
        var root = GetComponent<UIDocument>().rootVisualElement;
        UI_LoadingScreen = root.Q<VisualElement>("LoadingScreen");
    }

    public void ShowLoadingScreen()
    {
        UI_LoadingScreen.RemoveFromClassList("loading-hidden");
    }

    public void HideLoadingScreen()
    {
        UI_LoadingScreen.AddToClassList("loading-hidden");
    }
}
