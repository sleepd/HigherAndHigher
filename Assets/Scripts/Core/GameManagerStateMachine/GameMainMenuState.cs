using UnityEngine;

public class GameMainMenuState : GameManagerState
{
    public GameMainMenuState(GameManagerStateMachine gameManagerStateMachine) : base(gameManagerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        GameManager.Instance.LoadScene("MainMenu");
    }

    public override void Exit()
    {
        base.Exit();
        GameManager.Instance.UnloadCurrentScene();
    }
}
