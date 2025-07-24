using UnityEngine;

public class GamePausedState : GameManagerState
{
    public GamePausedState(GameManagerStateMachine gameManagerStateMachine) : base(gameManagerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        Time.timeScale = 1;
    }
}
