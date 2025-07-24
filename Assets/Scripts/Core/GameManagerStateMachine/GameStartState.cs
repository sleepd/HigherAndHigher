using UnityEngine;

public class GameStartState : GameManagerState
{
    public GameStartState(GameManagerStateMachine gameManagerStateMachine) : base(gameManagerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.ChangeState(stateMachine.gamePlayingState);
    }
}
